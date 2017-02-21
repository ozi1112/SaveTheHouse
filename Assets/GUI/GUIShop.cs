using System;
using UnityEngine;

public class GUIShop : MBSingleton<GUIShop>
{
	void Start()
	{
		Weapon[] weaponTable = GUIWeapon.instance.GetWeaponTable ();

		//weapon table load
	}

	void BuyItem()
	{ }


}


