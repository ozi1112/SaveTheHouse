using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wallet : MonoBehaviour
{
    public EventProperty <int> currentMoney = new EventProperty <int> (100);

	public void EarnMoney(int value)
	{
		currentMoney.val += value;
	}

	public bool SpendMoney(int value)
	{
		bool retVal = (currentMoney.val - value) >= 0;
		if (retVal) 
		{
			currentMoney.val -= value;
		}
		return retVal;
	}
}
