using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Use This Only Once
public class GUIProxy : MBSingleton<GUIProxy> {


	public enum Reason 
	{
		WaveEnd,
		Dead
	}

	// Use this for initialization
	void Start () 
	{ }
	
	// Update is called once per frame
	void Update () 
	{ }


	#region callbacks

	public void UpdateAmmo(int current, int max)
	{ }

	public void GameEnd(Reason reason)
	{ }

	public void HealthChange(int current, int max)
	{ }

	public void MoneyChange(int current)
	{ }

	public void UpdateWeaponState(WeaponManager.WeaponType weapon ) //change weapon
	{ }



	#endregion


	#region commands

	public void Reload()
	{
		PlayerController.instance.Reload();
	}

	public void ActivateWeapon(WeaponManager.WeaponType weapon)
	{
		WeaponManager.instance.AtivateWeapon (weapon);
	}

	public void SwitchWeapon(WeaponManager.WeaponType weapon)
	{
		WeaponManager.instance.SwitchWeapon (weapon);
	}

	#endregion


	#region getters

	public Weapon[] GetWeaponTable()
	{
		return WeaponManager.instance.GetWeaponTable ();
	}

	#endregion

}
