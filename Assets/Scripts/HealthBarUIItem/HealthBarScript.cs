using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

	public Slider slider;

	public void SetMaxhealth (float health)
	{
		slider.maxValue = health;
		SetHealth (health);
	}

	public void SetHealth (float health)
	{
		slider.value = health;
	}

}
