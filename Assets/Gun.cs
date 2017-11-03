using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	// 스팀 VR 관련 레퍼런스들
	public GameObject controllerRight;

	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device device;

	// 총의 탄약 UI
	public Text ammoText;

	// 현재 탄창의 남은 총알
	public int magAmmo;

	// 탄창의 총알 용량
	public int magAmmoMax = 30;

	// 총의 가능한 상태 - 잠금, 준비, 탄창빔, 재장전중
	public enum GUN_STATE { LOCK, READY, EMPTY, RELOADING };

	// 총의 현재 상태
	private GUN_STATE state = GUN_STATE.LOCK;

	// 연사 간격
	public float timeBetFire = 0.1f;

	// 마지막으로 총알을 발사한 시간대
	private float lastTimeFire;

	// 총의 애니메이터
	public Animator gunAnimator;

	// 총격 소리 재생기
	private AudioSource fireAudio;

	// 총구화염 재생기
	public ParticleSystem muzzleFlash;

	// 탄피 배출 효과 재생기
	public ParticleSystem caseEjectEffect;

	// 총알 궤적
	public LineRenderer shotLineRenderer;

	// 총알 발사 위치
	public Transform firePos;

	// 총알 궤적이 남아 있는 시간
	private WaitForSeconds effectDuration = new WaitForSeconds(0.07f);

	// 사정거리
	public float fireDistance = 100f;

	// 재장전 시간
	public float reloadTime = 1.7f;

	// 공격력
	public float damage = 25;

	// 피탄 효과 및 데칼
	public GameObject impactPrefab;

	private void Start()
	{
		trackedObj = controllerRight.GetComponent<SteamVR_TrackedObject>();

		// 총의 상태를 준비로
		state = GUN_STATE.READY;

		// 현재 탄창의 탄약을 최대로
		magAmmo = magAmmoMax;
		// 마지막 발사 시간을 초기화
		lastTimeFire = 0;

		// 총소리 재생용 오디오소스를 가져오기
		fireAudio = GetComponent<AudioSource>();

		// 총알 궤적의 꼭지점을 두개로
		shotLineRenderer.positionCount = 2;
		// 총알 궤적 랜더러를 꺼놓기
		shotLineRenderer.enabled = false;

		// 탄약 UI를 갱신
		UpdateGunUI();

		device = SteamVR_Controller.Input((int)trackedObj.index);
	}

	private void Fire()
	{
		// AND 현재 시간 >= 마지막으로 총을 쏜 과거의 시간 + 연사 간격
		// AND 총의 상태 == 준비됨
		if (Time.time >= lastTimeFire + timeBetFire &&
			state == GUN_STATE.READY)
		{
			// 마지막으로 총을 쏜 시간을, 당장 현재의 시간으로 갱신
			lastTimeFire = Time.time;

			/*실제 총쏘는 부분은 Shot() 에게 맡김*/
			Shot();
			// UI 갱신
			UpdateGunUI();

			device.TriggerHapticPulse(750);
		}
	}

	private void Update()
	{
		if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
		{
			Fire();
		}
		else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			Reload();
		}
	}

	// 총을 쏘는 실제 처리 함수
	private void Shot()
	{
		// 레이캐스트 충돌 정보를 담는 컨테이너
		RaycastHit hit;
		// hitPosition 총알궤적의 도착 지점
		// 총알이 맞는 곳에 지정해야 하나, 일단은 총구 앞쪽으로 머나먼 곳으로 지정
		// (총구 위치 + 총구 앞쪽x사정거리)
		Vector3 hitPosition = firePos.position + firePos.forward * fireDistance;

		// 만약-광선날리기(발사위치, 방향, 충돌 정보를 담아갈 그릇, 사정거리)-이
		// 충돌을 검사한다면...
		if (Physics.Raycast(firePos.position, firePos.forward, out hit, fireDistance))
		{
			// 총알궤적의 도착 지점 = 실제 광선의 충돌 지점
			hitPosition = hit.point;

			// 피탄 효과 찍어내기
			// 찍어내기(찍어낼 피탄효과 원본, 위치, 회전)
			// Quaternion.LookRotation(방향) => 해당 방향을 보는 쪽으로 회전을 만들어줌
			// hit.normal = 충돌각 (정확하게는 충돌한 표면이 바라보는 방향)
			GameObject impactInstance
				= Instantiate(impactPrefab, hit.point, Quaternion.LookRotation(hit.normal));

			// 만약 상대방이 IDamageable 로서 가져와진다면...
			// 상대방이 무조건 IDamageable 이 강제하는
			// Damage 함수는 가지고 있다는 것을 확신가능
			IDamageable target = hit.collider.GetComponent<IDamageable>();
			if (target != null)
			{
				// 상대방이 진짜 무엇인지는 모르지만 상관없이 Damage 함수를 쓰면 된다
				target.Damage(damage);
			}
		}

		// 이펙트 재생(광선 충돌위치)
		StartCoroutine(ShotEffect(hitPosition));

		//탄약 1 소모
		magAmmo--;

		// 탄약을 다 소모했다면 상태=탄창빔
		if (magAmmo <= 0)
		{
			magAmmo = 0;
			state = GUN_STATE.EMPTY;
		}
	}

	// 총구 화염과 피탄효과를 재생
	// 코루틴의 "대기시간"을 주는 기능을 사용하여 잠시 총알 궤적을 켠 상태로 대기하다가 끔
	private IEnumerator ShotEffect(Vector3 hitPosition)
	{
		//이펙트들을 재생
		muzzleFlash.Play();
		caseEjectEffect.Play();

		// 발사음 재생
		fireAudio.Play();

		// 총의 Fire 트리거 발동 (애니메이터에서 알아서 함)
		gunAnimator.SetTrigger("Fire");

		// 탄환의 직선 광선 효과를 켜기
		shotLineRenderer.enabled = true;
		// 꼭지점 지정하기
		// 0번째 지점 - 발사 위치
		shotLineRenderer.SetPosition(0, firePos.position);
		// 1번째 지점 - (레이캐스트 광선이 닿은) 충돌 지점
		shotLineRenderer.SetPosition(1, hitPosition);

		// 잠시 대기
		yield return effectDuration;

		// 탄환의 직선 광선 효과를 끄기
		// (켰다가 잠시후 바로 꺼서 "번쩍"하는 효과를 내는 것)
		shotLineRenderer.enabled = false;
	}

	// UI 갱신 코드를 묶어서 하나의 함수로 만든것
	private void UpdateGunUI()
	{
		ammoText.text = magAmmo + "/" + magAmmoMax;
	}

	// 재장전 함수
	public void Reload()
	{
		// 만약 재장전 중이라면, 재장전 중에 또 재장전 불가
		if (state == GUN_STATE.RELOADING)
		{
			return;
		}

		// 실제 재장전 처리를 순서대로
		StartCoroutine("Reloading");
	}

	// 실제 재장전 처리
	// 코루틴으로 쓴 이유는 탄약이 실제 들어가는데 지연시간을 주기 위해
	private IEnumerator Reloading()
	{
		// 현재 상태를 재장전중으로 지정
		// Update 함수에서 if문에 걸려 Shot 발사가 못되게 막음
		// Reload 함수에서 if문에 걸려 Reload 함수가 중단되게 함
		state = GUN_STATE.RELOADING;

		// 잠시 장전소요 시간만큼 대기
		yield return new WaitForSeconds(reloadTime);

		// 현재 탄창 총알 수를 최대 탄창 총알수로
		magAmmo = magAmmoMax;

		// 이제 REALODING => READY로 바꾸어 총의 락을 풀어줌
		state = GUN_STATE.READY;

		// UI 갱신
		UpdateGunUI();
	}
}