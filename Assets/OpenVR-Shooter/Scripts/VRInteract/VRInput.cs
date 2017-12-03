using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// VR 컨트롤러의 입력을 GetVRTriggerButton 과 GetVRGripButton 로 제공하는 클래스
public static class VRInput {

	public enum Hand {Left,Right};


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
	static string leftIndexTriggerName = "LeftIndexTrigger";
	// 쥐는 트리거에 대응되는 입력 세팅 이름
	static string leftGripTriggerName = "LeftGripTrigger";
	


	// 오른손
	// 검지 손가락 트리거에 대응되는 입력 세팅 이름
	static string rightIndexTriggerName = "RightIndexTrigger";
	// 쥐는 트리거에 대응되는 입력 세팅 이름
	static string rightGripTriggerName = "RightGripTrigger";
	

	// 편의를 위해 정적 함수로 입력을 제공
	public static bool GetTriggerButton(Hand hand)
	{
		if(hand == Hand.Right)
		{
			if(Input.GetAxisRaw(rightIndexTriggerName) >= 0.1f)
			{
				return true;
			}
		}
		else if(hand == Hand.Left)
		{
			if(Input.GetAxisRaw(leftIndexTriggerName) >= 0.1f)
			{
				return true;
			}
		}

		return false;
	}


	public static bool GetGripButton(Hand hand)
	{
		if(hand == Hand.Right)
		{
			if(Input.GetAxisRaw(rightGripTriggerName) >= 0.1f)
			{
				return true;
			}
		}
		else if(hand == Hand.Left)
		{
			if(Input.GetAxisRaw(leftGripTriggerName) >= 0.1f)
			{
				return true;
			}
		}
		
		return false;
	}

	//TODO GetTriggerDown 과 GetTriggerUp 만들기
}
