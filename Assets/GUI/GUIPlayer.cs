using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPlayer : MBSingleton<GUIPlayer> 
{

    public Text txtMoney;
    public Text txtHealth;
    public Health health;
    public Wallet wallet;

    private void Start()
    {
        wallet.currentMoney.OnChange += OnMoneyChange;
        OnMoneyChange();
        health.current.OnChange += OnMoneyChange;
        OnHealthChange();
        
    }

    //CALLBACKS
    public void OnHealthChange()
	{
        txtMoney.text = health.current.val.ToString();
    }
	public void OnMoneyChange()
	{
        txtMoney.text = wallet.currentMoney.val.ToString();
    }

	//COMMANDS
	public bool SpendMoney(int value)
	{
		return wallet.SpendMoney (value);
	}
}
