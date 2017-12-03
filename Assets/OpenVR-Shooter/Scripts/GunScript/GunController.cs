using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// VR 컨트롤러의 인풋을 받아 Gun 을 제어하는 스크립트
public class GunController : VRInputController {
	
	/* VR 입력을 받아 처리해야 하는 클래스는 VRInputController 만 상속받아서 두 함수만 오버라이드 하면 된다! */
	// 단 두개의 함수 OnGripTriggerButtonDown 와 OnIndexTriggerButtonDown

	public Gun gun;

	protected override void OnIndexTriggerButtonDown()
	{
		gun.Fire();
	}

	protected override void OnGripTriggerButtonDown()
	{
		gun.Reload();
	}
}
