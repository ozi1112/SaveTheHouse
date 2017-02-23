using System;
using UnityEngine;
using UnityEngine.UI;

public class GUIShop : MBSingleton<GUIShop>
{
	/// <summary>
	/// Item prefab.
	/// </summary>
	public GameObject shopItemPrefab;
	/// <summary>
	/// Button buy.
	/// </summary>
	public Button btnBuy;
	/// <summary>
	/// Button equip.
	/// </summary>
	public Button btnEquip;
	/// <summary>
	/// Image weapon .
	/// </summary>
	public Image imgWeapon;
	/// <summary>
	/// Text with weapon description.
	/// </summary>
	public Text txtWpnDesc;
	/// <summary>
	/// Shop items table.
	/// </summary>
	UIShopItem[] shopItems;


	void Start()
	{
		ReloadItems();
	}


	void ReloadItems()
	{
		Weapon[] weaponTable = WeaponManager.instance.GetWeaponTable ();
		shopItems = new UIShopItem[weaponTable.Length];
		foreach(Weapon item in weaponTable)
		{
			GameObject instance = GameObject.Instantiate<GameObject>(shopItemPrefab);
			instance.transform.SetParent(transform);
			UIShopItem shopItem = instance.GetComponent<UIShopItem>();
			shopItem.Initialize(item);
			shopItems[(int)item.type] = shopItem;
		}
	}

	public void OnItemSelected(WeaponManager.WeaponType weaponType)
	{
		Weapon[] weaponTable = WeaponManager.instance.GetWeaponTable ();

		imgWeapon.sprite = weaponTable[(int)weaponType].icon;
		txtWpnDesc.text = string.Format(
			@"
			Power: 	{0} \n
			Speed:	{1} \n
			Ammo:	{2} \n
			",
			weaponTable[(int)weaponType].power,
			weaponTable[(int)weaponType].shootPerSecond,
			weaponTable[(int)weaponType].maxAmmo);

		btnBuy.onClick.RemoveAllListeners();
		btnEquip.onClick.RemoveAllListeners();
		//Not bought 
		if(!weaponTable[(int)weaponType].active)
		{
			btnBuy.onClick.AddListener( () => { WeaponManager.instance.AtivateWeapon(weaponType); } );
			btnEquip.gameObject.SetActive(false);
		}
		//Bought
		else
		{
			btnEquip.onClick.AddListener( () => { WeaponManager.instance.SwitchWeapon(weaponType); } );
			btnBuy.gameObject.SetActive(false);
		}
	}


}


