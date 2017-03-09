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
	WeaponType weaponType;

	public void Initialize(Weapon weapon)
	{
		gameObject.GetComponent<Toggle>().onValueChanged.AddListener(OnSelect);
		this.weaponType = weapon.type;
		this.price = weapon.m_Price;
		GetComponent<Image>().sprite = weapon.icon;

		if(weapon.m_Active)
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
		WeaponManager.instance.UpdateWeapon(weaponType);
	}

	void OnBuyClick()
	{
		if(PlayerController.instance.wallet.SpendMoney(price))
		{
			WeaponManager.instance.AtivateWeapon(weaponType);
		}
	}

	void OnUpgradeClick()
	{
		if(PlayerController.instance.wallet.SpendMoney(price))
		{
			WeaponManager.instance.GetWeapon(weaponType).UpgradeWeapon();
		}
		//refresh gui
		GUIShop.instance.OnItemSelected(weaponType);
	}

	
}
