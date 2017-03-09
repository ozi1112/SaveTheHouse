using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wallet : MonoBehaviour
{
    public EventProperty <int> currentMoney = new EventProperty <int> (1000);

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
		else
		{
			Debug.Log(string.Format("SpendMoney not enough cur:{0} val:{1}", currentMoney.val, value));
		}
		return retVal;
	}
}