using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPlayer : MBSingleton<GUIPlayer> {

    public Text money;

    private void Start()
    {
        MoneyChange(PlayerController.instance.wallet.currentMoney.val);
    }

    //CALLBACKS
    public void HealthChange(int current, int max)
	{

    }
	public void MoneyChange(int current)
	{
        money.text = current.ToString();
    }

	//COMMANDS
	public bool SpendMoney(int value)
	{
		return PlayerController.instance.wallet.SpendMoney (value);
	}
}
