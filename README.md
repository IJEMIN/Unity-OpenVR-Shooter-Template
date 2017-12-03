![대문](https://imgur.com/wocxoKL.png)


# Unity-OpenVR-Shooter-Template<br>유니티 OpenVR 슈터 템플릿
사용하기 쉽게 템플릿화 되어 완성된 유니티 VR 슈터 게임 제작 템플릿입니다.

Oculus VR 과 HTC Vive (Steam VR) 에 모두 대응합니다.

총 모듈만 따로 때어 FPS 게임을 제작할때 사용할 수 있습니다.

상세한 설명 주석 달아놨음.
이것 기반으로 만듬<br>https://github.com/IJEMIN/Unity-OpenVR-Power-Blade-Example

----------
## 다운로드
최신 버전의 패키지를 [releases](//github.com/IJEMIN/Unity-OpenVR-Shooter-Template/releases) 에서 받아주세요.<br>저장소에는 현재 작성중인 미완성의 코드가 포함되 있을 수 있습니다.


## 제공하는 기능
코드 동작이 내부에서 간결하게 완결되어 있어, 드래그-드롭으로 쓸 수 있습니다.

### VR 트래킹 기능
- 현실 부위 1:1 트래킹
- VR 컨트롤러 입력 대응 함수 제공
- 스테이셔너리 자동 키 설정
- 간결한 코드와 높은 확장성

### 건 슈터 (FPS Shooter) 기능
- 단발/연사
- 피탄 이펙트
- 발사 이펙트
- 재장전
- 증강현실 탄약 UI
- 간결한 외부 함수
    - Gun.Reload - 장전
    - Gun.Fire - 발사

## 동작과 사용 방법
미리 만들어진 예제 씬을 참고하면 좋습니다.

### 프리팹 Prefab

미리 만들어진 예제 프리팹을 제공합니다.
- VR Player: VR 카메라와 왼손과 오른손 트래킹, (사람) 키 대응이 미리 완성되어 있는 프리팹입니다.
- Gun: 사격할수 있는, 미리 완성된 총 입니다.
- Cube: Gun 에 의해 데미지를 받아 파괴될 수 있는 큐브 입니다.

### VR 컨트롤러 트래킹
![트래킹1](https://imgur.com/NKPpcAc.png)
어떤 오브젝트든 VRControllerTracking 만 붙여주면, 현실의 VR 컨트롤러와 위치와 동기화됩니다.

![트래킹1](https://imgur.com/jgH8PFD.png)
부착한 다음, 원하는 추적 부위를 지정해주세요.

위치와 회전 동기화는 로컬 좌표계를 기준으로 합니다.


### VR 컨트롤러 입력 감지

어떤 스크립트든 VRInputController 를 상속하면, VR 컨트롤러의 입력을 받을 수 있습니다.

두가지 오버라이드 가능한 함수를 제공합니다. 이들은 VR 컨트롤러 입력이 감지되면 자동으로 호출됩니다.
- void OnIndexTriggerButton() - VR 컨트롤러 검지 방아쇠를 누르는 동안 발동
- void OnGripTriggerButton() - VR 컨트롤러 쥐는 방아쇠를 누르는 동안 발동

1. VRInputController 스크립트를 상속받습니다.
2. 원하는 입력에 따라 위의 함수 중 하나를 오버라이드 합니다.
3. 함수 내부에 입력에 대응할 동작을 구현합니다.

#### VR 입력 대응 예시 코드

예제의 GunController 스크립트는 VR 입력에 따라 Gun 을 제어하는 스크립트 입니다.
별다른 코드 없이, OnIndexTriggerButton 와 OnGripTriggerButton 를 오버라이드 하여 입력에 대응하고 있습니다.


~~~
public class GunController : VRInputController {
	
	public Gun gun;

	public override void OnIndexTriggerButtonDown()
	{
		gun.Fire();
	}

	public override void OnGripTriggerButtonDown()
	{
		gun.Reload();
	}
}
~~~

### 건 슈터 Gun Shooter
캡슐화 되어있습니다. 그냥 프리팹을 가져다 쓰면 됩니다.
![총프리팹](https://imgur.com/T9ZJiT3.png)
외부 함수로 Gun.Fire 와 Gun.Reload 를 제공하여, 총을 쏘고 재장전 할 수 있습니다.

### 총알 데미지 처리
IDamageable 인터페이스를 상속받는 오브젝트는 총에 의해 데미지를 입을 수 있습니다.

예시 코드
~~~

public class HitCube : MonoBehaviour,IDamageable {
	public float hp = 100f;	
	public void OnDamage(float damage)
	{
		Debug.Log("큐브가 맞았다!");
		hp -= damage;

		if(hp <= 0)
		{
			Destroy(gameObject);
		}
	}
}
~~~

### 이외의 기능들 ###

작성중

## 기타 ##

### VR 컨트롤러 입력 설정

VR 컨트롤러의 입력을 InputManager 에서 제어하는 방법은 VRInputController 와 https://github.com/IJEMIN/Unity-OpenVR-Power-Blade-Example 의 Readme 문서를 참고.



# 크레딧
I_Jemin (i_jemin@hotmail.com, ijemin.com)

# 기타
유니티 애셋 스토어에서 제공되는 무료 애셋을 사용하였습니다.<br>
이 프로젝트는 교육용입니다.

상업적 용도로 해당 애셋들을 사용하지 않았으며, 라이센스 문제가 있는 모델과 음원은 추후 삭제하거나 대체할 것입니다.







# Unity-OpenVR-Shooter-Template
Easy to use VR Shooter template for Unity OpenVR Input System. Works for Oculus Rift and Vive.

Only use Unity built-in VR codes.

# Credit
I_Jemin (i_jemin@hotmail.com, ijemin.com)