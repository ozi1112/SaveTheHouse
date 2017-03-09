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
	/// 
	/// </summary>
	public Button btnUpgrade;
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

		//Select Pistol
		var toggle = shopItems[(int)WeaponType.Pistol].GetComponent<Toggle>();
		toggle.onValueChanged.Invoke(true);
		toggle.Select();
	}

	/// <summary>
	/// Use for refresh weapon list.
	/// </summary>
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

	/// <summary>
	/// Select an item and show details in 'details' panel.
	/// </summary>
	public void OnItemSelected(WeaponType weaponType)
	{
		Weapon[] weaponTable = WeaponManager.instance.GetWeaponTable ();

		imgWeapon.sprite = weaponTable[(int)weaponType].icon;
		txtWpnDesc.text = string.Format(
		   "Power: 	{0}" + Environment.NewLine +
			"Speed:	{1}" + Environment.NewLine +
			"Ammo:	{2}" + Environment.NewLine,
			weaponTable[(int)weaponType].GetWeaponParams().power,
			weaponTable[(int)weaponType].GetWeaponParams().shootPerSecond,
			weaponTable[(int)weaponType].GetWeaponParams().maxAmmo);

		btnBuy.onClick.RemoveAllListeners();
		btnEquip.onClick.RemoveAllListeners();
		btnUpgrade.onClick.RemoveAllListeners();
		//Not bought 
		if(!weaponTable[(int)weaponType].m_Active)
		{
			btnBuy.onClick.AddListener( () => { 
				if(PlayerController.instance.wallet.SpendMoney(weaponTable[(int)weaponType].m_Price))
				{
					btnEquip.gameObject.SetActive(true);
					btnUpgrade.gameObject.SetActive(true);
					btnBuy.gameObject.SetActive(false);
					WeaponManager.instance.AtivateWeapon(weaponType);
				}
			} );
			
			btnBuy.gameObject.SetActive(true);
			btnEquip.gameObject.SetActive(false);
			btnUpgrade.gameObject.SetActive(false);
		}
		//Bought
		else
		{
			btnEquip.onClick.AddListener( () => { WeaponManager.instance.UpdateWeapon(weaponType); } );
			btnEquip.gameObject.SetActive(true);

			btnUpgrade.onClick.AddListener(
			()=>{ 
				if(PlayerController.instance.wallet.SpendMoney(weaponTable[(int)weaponType].m_Price))
				{
					WeaponManager.instance.GetWeapon(weaponType).UpgradeWeapon(); 
					OnItemSelected(weaponType); 
				}
				
			} );
			btnUpgrade.gameObject.SetActive(true);

			btnBuy.gameObject.SetActive(false);
			
		}
	}
}


