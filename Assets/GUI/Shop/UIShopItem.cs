using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Toggle))]

public class UIShopItem : MonoBehaviour {

	bool _bought = false;
	public int price;
	WeaponManager.WeaponType weaponType;

	public void Initialize(Weapon weapon)
	{
		gameObject.GetComponent<Toggle>().onValueChanged.AddListener(OnSelect);
		this.weaponType = weapon.type;
		this.price = weapon.price;
		GetComponent<Image>().sprite = weapon.icon;

		if(weapon.active)
		{
			//TODO
			//highlight or something
		}

		gameObject.AddComponent(typeof(BoxCollider2D));
	}

	void OnSelect(bool isSelect)
	{
		GUIShop.instance.OnItemSelected(weaponType);
	}

	void OnEquipClick()
	{
		WeaponManager.instance.SwitchWeapon(weaponType);
	}

	void OnBuyClick()
	{
		if(PlayerController.instance.SpendMoney(price))
		{
			WeaponManager.instance.AtivateWeapon(weaponType);
		}
	}

	
}
