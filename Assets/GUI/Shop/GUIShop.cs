using System;
using UnityEngine;

public class GUIShop : MBSingleton<GUIShop>
{
	public GameObject shopItemPrefab;
	GameObject[] shopItems;

	void Start()
	{
		ReloadItems();
		//weapon table load
	}


	void ReloadItems()
	{
		Weapon[] weaponTable = WeaponManager.instance.GetWeaponTable ();
		shopItems = new GameObject[weaponTable.Length];
		foreach(Weapon item in weaponTable)
		{
			GameObject instance = GameObject.Instantiate<GameObject>(shopItemPrefab);
			instance.transform.SetParent(transform);
			UIShopItem shopItem = instance.GetComponent<UIShopItem>();
			shopItem.Initialize(item);
			shopItems[(int)item.type] = instance;
		}
	}

	public void OnItemSelected(WeaponManager.WeaponType weaponType)
	{
		//TODO
		//Weapon preview + upgrades
	}
}


