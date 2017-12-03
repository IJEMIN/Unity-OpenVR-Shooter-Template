using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanage : MonoBehaviour {

	public Image target;

	public void SetWhite()
	{
		target.color = Color.white;
	}

	public void SetRed()
	{
		target.color = Color.red;
	}

	public void SetRandomColor()
	{
		target.color = Random.ColorHSV();
	}
}
