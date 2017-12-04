﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// VR 컨트롤러의 인풋을 받아 Gun 을 제어하는 스크립트
public class GunController : MonoBehaviour {
	
	/* VR 입력을 받아 처리해야 하는 클래스는 VRInput의 두 함수만 체크 하면 된다 */
	// 단 두개의 함수 GetGripButton 와 GetTriggerButton

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
}
