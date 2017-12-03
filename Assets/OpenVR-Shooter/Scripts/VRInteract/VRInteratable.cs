using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRInteratable : MonoBehaviour {

	[SerializeField]
	private UnityEvent onClick;

	[SerializeField]
	private UnityEvent onVREyeEnter;

	[SerializeField]
	private UnityEvent onVREyeExit;

	public void OnClick()
	{
		onClick.Invoke();
	}

	public void OnVREyeEnter()
	{
		onVREyeEnter.Invoke();
	}

	public void OnVREyeExit()
	{
		onVREyeExit.Invoke();
	}
}
