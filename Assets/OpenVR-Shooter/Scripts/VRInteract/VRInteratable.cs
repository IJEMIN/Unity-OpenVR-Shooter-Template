using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRInteratable : MonoBehaviour {

	[SerializeField]
	private UnityEvent onVRTriggerClick;

	[SerializeField]
	private UnityEvent onVREyeEnter;

	public void OnTriggerClick()
	{
		onVRTriggerClick.Invoke();
	}

	public void OnVREyeEnter()
	{
		onVREyeEnter.Invoke();
	}
}
