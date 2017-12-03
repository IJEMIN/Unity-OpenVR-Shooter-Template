using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// VR 컨트롤러의 입력 감지를 GetButton 함수로 제공하는 클래스
public class VRInput: MonoBehaviour {

	private static VRInput m_instance;

	public static VRInput instance
	{ 
		get
		{
			if (!m_instance)
			{
				m_instance = new GameObject("VRInput").AddComponent<VRInput>();
			}

			return m_instance;
		}
	}

	private void Start()
	{
		if (m_instance != null && m_instance != this)
		{
			Debug.LogError("There are more than one VRInput instance");
			DestroyImmediate(this);
		}
	}


	public enum Button {LeftIndex,RightIndex,LeftGrip,RightGrip};

	/*
	유니티는 OpenVR API 를 내장하고 있다.
	이것으로 VR 입력을 InputManager 에서 조이스틱 입력으로서 받을 수 있다.

	1. VR 인풋 테이블 참고하기.
	
	VR 인풋 테이블을 참고하기 위해 링크로 이동한다. (https://docs.unity3d.com/Manual/OpenVRControllers.html) InputManager 에서 어떤 조이스틱 입력 번호가 VR 컨트롤러의 버튼에 대응된느지 체크할 수 있다.

	2. InputManager 에서 VR 용 입력 설정을 만든다.
	Project Settings > InputManager 로 이동한다.
	
	(미리 만들어진 InputManager 값들을 확인 가능하다.)

	이름은 마음대로 지정가능하다. 이 예제의 경우 HTC Vive 컨트롤러의 왼쪽 검지 방아쇠(트리거) 버튼과 Oculus Rift 터치 컨트롤러의 왼쪽 검지 방아쇠(트리거) 버튼에 연동되는 입력 세팅 이름을 "LeftIndexTrigger" 로 지었다.

	인풋 테이블에서 Vive 왼손 검지 방아쇠와 Ocluls Rift 왼손 검지 방아쇠는 Joystick 9 th Axis 로 표시되어있다.
	
	따라서 LeftIndexTrigger 에 대응되는 조이스틱 Axis 는 9 th Axis 로 지정되어 있다.
	
	위의 내용은 미리 세팅된 값에서 확인 가능하다.

	2. 일반 게임에서 사용하듯이 Input.GetAxis 를 사용하면 된다.

	0.1f(10%) 이상 트리거가 눌려질시 해당 버튼을 누른것으로 보았다.

	GetAxis 는 입력값을 부드럽게 꺾어준다. 별 상관은 없지만 입력을 즉시 받기 위해, 보간이 없는 GetAxisRaw 를 사용했다.

	 */


	//왼손
	// 검지 손가락 트리거에 대응되는 입력 세팅 이름
	public static string leftIndexTriggerName = "LeftIndexTrigger";
	// 쥐는 트리거에 대응되는 입력 세팅 이름
	public static string leftGripTriggerName = "LeftGripTrigger";
	

	// 오른손
	// 검지 손가락 트리거에 대응되는 입력 세팅 이름
	public string rightIndexTriggerName = "RightIndexTrigger";
	// 쥐는 트리거에 대응되는 입력 세팅 이름
	public string rightGripTriggerName = "RightGripTrigger";
	

	private bool m_isLeftIndexTriggerDown;
	private bool m_isLeftIndexTriggerStay;


	private bool m_isRightIndexTriggerDown;
	private bool m_isRightIndexTriggerStay;


	private bool m_isLeftGripTriggerDown;
	private bool m_isLeftGripTriggerStay;


	private bool m_isRightGripTriggerDown;
	private bool m_isRightGripTriggerStay;


	void Awake()
	{
		m_isRightIndexTriggerDown = false;
		m_isRightIndexTriggerStay = false;

		m_isRightGripTriggerDown = false;
		m_isRightGripTriggerStay = false;

		m_isLeftIndexTriggerDown = false;
		m_isLeftIndexTriggerStay = false;

		m_isLeftGripTriggerDown = false;
		m_isLeftGripTriggerStay = false;
	}


	public static bool GetVRButtonDown(Button button)
	{
		switch (button)
		{
			case Button.LeftIndex:
				return instance.m_isLeftIndexTriggerDown;
			
			case Button.LeftGrip:
				return instance.m_isLeftGripTriggerDown;
			
			case Button.RightIndex:
				return instance.m_isRightIndexTriggerDown;

			case Button.RightGrip:
				return instance.m_isRightGripTriggerDown;
		}

		return false;
	}


	public static bool GetVRButton(Button button)
	{
		switch (button)
		{
			case Button.LeftIndex:
				return instance.m_isLeftIndexTriggerStay;
			
			case Button.LeftGrip:
				return instance.m_isLeftGripTriggerStay;
			
			case Button.RightIndex:
				return instance.m_isRightIndexTriggerStay;

			case Button.RightGrip:
				return instance.m_isRightGripTriggerStay;
		}

		return false;
	}


	void Update()
	{
		UpdateIndexState();
		UpdateGripState();

	}

	void UpdateIndexState()
	{

		if(Input.GetAxisRaw(rightIndexTriggerName) >= 0.1f)
		{
			if(m_isRightIndexTriggerDown || m_isRightIndexTriggerStay)
			{
				m_isRightIndexTriggerDown = false;
			}
			else
			{
				m_isRightIndexTriggerDown = true;
			}

			m_isRightIndexTriggerStay = true;
		}
		else
		{
			m_isRightIndexTriggerDown = false;
			m_isRightIndexTriggerStay = false;
		}


		if(Input.GetAxisRaw(leftIndexTriggerName) >= 0.1f)
		{
			if(m_isLeftIndexTriggerDown || m_isLeftIndexTriggerStay)
			{
				m_isLeftIndexTriggerDown = false;
			}
			else
			{
				m_isLeftIndexTriggerDown = true;
			}
			
			m_isLeftIndexTriggerStay = true;
		}
		else
		{
			m_isLeftIndexTriggerDown = false;
			m_isLeftIndexTriggerStay = false;
		}
	}

	void UpdateGripState()
	{

				
		if(Input.GetAxisRaw(rightGripTriggerName) >= 0.1f)
		{
			if(m_isRightGripTriggerDown || m_isRightGripTriggerStay)
			{
				m_isRightGripTriggerDown = false;
			}
			else
			{
				m_isRightGripTriggerDown = true;
			}

			m_isRightGripTriggerStay = true;
		}
		else
		{
			m_isRightGripTriggerDown = false;
			m_isRightGripTriggerStay = false;
		}


		if(Input.GetAxisRaw(leftGripTriggerName) >= 0.1f)
		{
			if(m_isLeftGripTriggerDown || m_isLeftGripTriggerStay)
			{
				m_isLeftGripTriggerDown = false;
			}
			else
			{
				m_isLeftGripTriggerDown = true;
			}
			
			m_isLeftGripTriggerStay = true;
		}
		else
		{
			m_isLeftGripTriggerDown = false;
			m_isLeftGripTriggerStay = false;
		}
	}


}
