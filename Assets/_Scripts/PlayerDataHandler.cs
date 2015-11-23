using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDataHandler : MonoBehaviour {

	private float playerHp;
	public Slider slider;

	void playerHit(float amount)
	{
		CancelInvoke("playerAutoRegen");
		playerHp = playerHp - amount;
		updateSlider();
		InvokeRepeating("playerAutoRegen",5f,.5f);
	}
		
	void playerAutoRegen()
	{
		playerHp ++;
		updateSlider();
	}

	void updateSlider()
	{
		slider.value = playerHp;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			playerHit(15);
			Debug.Log("test");
		}
	}
}
