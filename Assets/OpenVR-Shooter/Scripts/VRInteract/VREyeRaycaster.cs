using System;
using UnityEngine;


public class VREyeRaycaster : MonoBehaviour
{
	public event Action<RaycastHit> OnRaycasthit;                   // This event is called every frame that the user's gaze is over a collider.


	[SerializeField] private Transform m_Camera;
	[SerializeField] public LayerMask m_ExclusionLayers;           // Layers to exclude from the raycast.
    [SerializeField] private float m_RayLength = 500f; 
	[SerializeField] private bool m_ShowDebugRay;                   // Optionally show the debug ray.	[SerializeField] private float m_RayLength = 500f;              // How far into the scene the ray is cast.


	private VRInteratable m_CurrentInteractible;                //The current interactive item
	private VRInteratable m_LastInteractible;                   //The last interactive item


	void Update()
	{
		EyeRaycast();

		if(VRInput.GetVRButtonDown(VRInput.Button.LeftIndex))
		{
			if(m_CurrentInteractible)
			{
				m_CurrentInteractible.OnClick();
			}
		}
	}


	
	private void EyeRaycast()
	{
		// 디버그용 레이
		if (m_ShowDebugRay)
		{
			Debug.DrawRay(m_Camera.position, m_Camera.forward * 5, Color.blue, 1f);
		}

		// 카메라 앞쪽으로 광선
		Ray ray = new Ray(m_Camera.position, m_Camera.forward);
		RaycastHit hit;
		
		// Do the raycast forweards to see if we hit an interactive item
		if (Physics.Raycast(ray, out hit, m_RayLength, ~m_ExclusionLayers))
		{
			VRInteratable interactible = hit.collider.GetComponent<VRInteratable>(); //attempt to get the VRInteractiveItem on the hit object
			m_CurrentInteractible = interactible;

			// If we hit an interactive item and it's not the same as the last interactive item, then call Over
			if (interactible && interactible != m_LastInteractible)
			{
				interactible.OnVREyeEnter(); 
				m_LastInteractible = interactible;
			}

			if (OnRaycasthit != null)
				OnRaycasthit(hit);
		}
		else
		{
			if(m_LastInteractible && m_CurrentInteractible)
			{
				m_LastInteractible.OnVREyeExit();
			}

			m_LastInteractible = null;
			m_CurrentInteractible = null;
		}
	}
}
