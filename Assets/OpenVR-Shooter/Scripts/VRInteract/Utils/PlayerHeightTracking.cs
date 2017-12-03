using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerHeightTracking : MonoBehaviour {


	// 룸스케일 모드에서는 머리의 위치를 추적하므로 문제가 없다.
	// 센서를 사용하지 않는 Stationary Mode 에서는
	// HMD 의 기울기는 내부 자이로스코프로 측정이되나, 위치가 측정이 안되므로
	// 플레이어가 일어서 있어도 측정이 안되니 게임의 바닥에 꽂혀 있다.
	
	// 만약 Stationary Mode 라면,
	// 플레이어의 키(예상치) 만큼 플레이어의 머리의 위치를 올려줌

	// 플레이어의 키
	public float playerHeight = 1.7f;
	// Update is called once per frame
	void Start () {

		if(XRSettings.enabled == false)
		{
			enabled = false;
		}

		if(XRDevice.GetTrackingSpaceType() == TrackingSpaceType.RoomScale)
		{
			Debug.Log("룸 스케일 트래킹");
		}
		else
		{
			Debug.Log("스테이셔너리 트레킹");
			//현재 위치에서 플레이어의 키만큼 올리기
			transform.localPosition += Vector3.up * playerHeight;
		}	


	}
}
