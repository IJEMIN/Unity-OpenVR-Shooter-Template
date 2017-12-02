using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
