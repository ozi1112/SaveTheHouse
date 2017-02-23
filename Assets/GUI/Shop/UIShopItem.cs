using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class UIShopItem : MonoBehaviour {

	bool _bought = false;
	public int price;
	WeaponManager.WeaponType weaponType;

	public void Initialize(Weapon weapon)
	{
		this.weaponType = weapon.type;
		this.price = weapon.price;
		GetComponent<Image>().sprite = weapon.icon;

		if(weapon.active)
		{
			//TODO
			//highlight or something
		}
	}

	void OnMouseDown()
	{
		GUIShop.instance.OnItemSelected(weaponType);
	}

	void OnEquipClick()
	{

	}

	void OnBuyClick()
	{
		if(PlayerController.instance.SpendMoney(price))
		{
			WeaponManager.instance.AtivateWeapon(weaponType);
		}
	}

	
}
