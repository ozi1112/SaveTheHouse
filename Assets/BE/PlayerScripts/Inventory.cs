using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory {

	int currentMoney = 100;

	void EarnMoney(int value)
	{
		currentMoney += value;
	}

	bool GetMoney(int value)
	{
		bool retVal = (currentMoney - value) >= 0;
		if (retVal) {
			currentMoney -= value;
		}
		return retVal;
	}
}
