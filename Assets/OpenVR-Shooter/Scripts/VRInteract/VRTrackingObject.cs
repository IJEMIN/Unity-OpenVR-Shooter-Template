using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 유니티 2017.2 버전부터는 XR, 2017.1 버전까지는 VR 명칭으로 사용
// UnityEngine.VR;
using UnityEngine.XR;

// 현실의 디바이스를 트래킹하는 스크립트
public class VRTrackingObject : MonoBehaviour {

	// 트래킹 부위 식별자
	public XRNode trackingNode;

	void Start()
	{
		// 게임이 시작되었을때 XR(VR) 옵션이 안켜져 있으면 스스로를 끄기
		if(XRSettings.enabled == false)
		{
			Debug.LogWarning("NO XR EXIST!");
			enabled = false;
		}
	}

	void Update()
	{
		// InputTracking 은 디바이스 식별자를 주면 자동으로 추적해주는 함수가 있음

		// 예) InputTracking.GetLocalPosition(XRNode.LeftEye) 는 왼쪽 눈을 트래킹
		// 나의 위치와 회전을 추적 하는 부위(기기)의 위치와 회전으로 설정
		transform.localPosition = InputTracking.GetLocalPosition(trackingNode);

		transform.localRotation =
		InputTracking.GetLocalRotation(trackingNode);
	}
}
