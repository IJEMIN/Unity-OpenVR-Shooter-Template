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
- 간결한 코드와 높은 확장성

### VR 인터렉션과 VR UI
- VR 카메라가 응시하는 사물과 상호작용 가능
- 상호작용 이벤트를 제공
- 상호작용 UI 예제 첨부

### 건 슈터 (FPS Shooter) 기능
- 단발/연사
- 피탄 이펙트
- 발사 이펙트
- 재장전
- 증강현실 탄약 UI
- 간결한 외부 함수
    - Gun.Reload - 장전
    - Gun.Fire - 발사



## 선행조건 ##

유니티 Input Manager 에 OpenVR에 대응되는 Axes 이름이 지정되어 있어야 합니다.<br>
각 스틱과 버튼에 대한 식별자는 [유니티 OpenVR 입력 테이블 문서](https://docs.unity3d.com/Manual/OpenVRControllers.html) 에서 찾을 수 있습니다.


이 템플릿 프로젝트에는 해당 설정이 미리 지정되어 있으므로 별다른 설정을 요구하지는 않습니다.<br>미리 설정된 VR Input 테이블은 Edit > Proejct Settings > Input Manager 에서 확인할 수 있습니다.

VR 컨트롤러에 대응하는 자세한 원리는 VRInput 스크립트의 주석에 있습니다.


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

![트래킹2](https://imgur.com/jgH8PFD.png)
부착한 다음, 원하는 추적 부위를 지정해주세요.

위치와 회전 동기화는 로컬 좌표계를 기준으로 합니다.


### VR 컨트롤러 입력 감지

VRInput 를 통해 왼손과 오른손 VR 컨트롤러의 현재 입력을 확인할 수 있습니다.

두가지 정적 함수를 제공합니다.

- bool GetVRButtonDown(Button button)
    - VR 컨트롤러의 방아쇠 버튼을 막 누른 그 한 순간에만 true 를 반환
	- 클릭이나 단발 사격을 구현할때 사용할 수 있습니다

- bool GetVRButton(Button button)
	- VR 컨트롤러의 방아쇠 버튼을 누르는 동안 지속적으로 true 를 반환


두 함수 모두 입력으로 방아쇠 버튼의 종류를 받습니다.
- LeftIndex: 왼손 VR 컨트롤러의 검지 방아쇠
- RightIndex: 오른손 VR 컨트롤러의 검지 방아쇠
- LeftGrip: 왼손 VR 컨트롤러의 그립(쥐는) 방아쇠
- RightGrip: 오른손 VR 컨트롤러의 그립(쥐는) 방아쇠


#### VR 입력 대응 예시 코드

예제의 GunController 스크립트는 VR 입력에 따라 Gun 을 제어하는 스크립트 입니다.<br>
별다른 코드 없이, GetVRButtonDown 와 GetVRButton 으로 입력을 체크하여 총을 제어합니다.

검지 방아쇠를 당기고 있는 동안 계속 총을 발사합니다.<br>그립 방아쇠는 Down 함수를 사용하여, 쥐는 그 한 순간에만 재장전을 합니다.


~~~
	public Gun gun;

	void Update()
	{
		if(VRInput.GetVRButton(VRInput.Button.RightIndex))
		{
			gun.Fire();
		}

		if(VRInput.GetVRButtonDown(VRInput.Button.RightGrip))
		{
			gun.Reload();
		}
	}
~~~

### VR 인터렉션과 VR UI ###

VR에서 UI와 상호작용하는 예제가 데모에 있습니다.<br>상호작용 자체는 UI가 아니더라도, 콜라이더를 가진 다른 모든 오브젝트와도 가능합니다.

데모의 VR UI Canvas 오브젝트에 있는, Interatable UI Image 를 플레이어가 응시하면 색이 바뀝니다.<br>다시 다른 곳을 바라보면 원래 색으로 돌아갑니다. 플레이어가 해당 오브젝트를 바라보는 상태에서 오른손 검지 방아쇠를 당기면 색이 무작위로 지정됩니다.


#### 사용법 ####

선행조건
- 레이캐스팅을 통해 감지하므로, 상호작용할 물체는 3D 콜라이더 컴포넌트를 가지고 있어야 합니다.
- UI 오브젝트도 예외없이 3D 콜라이더를 가져야 합니다.

절차

- 카메라 VREyeRaycaster 스크립트를 부착합니다.<br>![VRRaycaster](https://imgur.com/NAdS3z9.png)
	- 해당 스크립트는 응시하고 있는 오브젝트가 가진 함수를 발동시킬 수 있습니다.
- 인터렉션이 필요한 오브젝트에게 콜라이더를 부착합니다.<br>![VRUICollider](https://imgur.com/1ZIStu5.png)
- 인터렉션할 오브젝트에게 VRInteractable 스크립트를 부착합니다.<br>![VRInteractable](https://imgur.com/RMaaOWp.png)
	- 이 스크립트는 세가지 유니티 이벤트를 제공합니다. 이들은 VREyeRaycaster에 의해 자동으로 발동됩니다.
		- OnClick: VR 오른손 컨트롤러의 검지 방아쇠를 클릭하면 발동됩니다.
		- OnVREyeEnter: VREyeRaycaster 를 가진 카메라의 시선 중심에 오브젝트가 들어오는 순간 발동됩니다.
		- OnVREyeExit: VREyeRaycaster 를 가진 카메라의 시선이 오브젝트를 벗어나는 순간 발동됩니다.
- 원하는 유니티 이벤트의 슬롯을 추가하고, 연동할 함수를 등록합니다.


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



# 크레딧과 연락처
I_Jemin
- 메일: i_jemin@hotmail.com
- 블로그: ijemin.com

# 기타
유니티 애셋 스토어에서 제공되는 무료 애셋을 사용하였습니다.<br>
이 프로젝트는 교육용입니다.

상업적 용도로 해당 애셋들을 사용하지 않았으며, 라이센스 문제가 있는 모델과 음원은 추후 삭제하거나 대체할 것입니다.







# Unity-OpenVR-Shooter-Template
Easy to use VR Shooter template for Unity OpenVR Input System. Works for Oculus Rift and Vive.

Only use Unity built-in VR codes.

# Credit
I_Jemin (i_jemin@hotmail.com, ijemin.com)