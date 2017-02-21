using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPlayer : MBSingleton<GUIPlayer> {


	//CALLBACKS
	public void HealthChange(int current, int max)
	{ }
	public void MoneyChange(int current)
	{ }

	//COMMANDS
	public bool SpendMoney(int value)
	{
		return PlayerController.instance.SpendMoney (value);
	}
}
