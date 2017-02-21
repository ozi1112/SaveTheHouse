using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIWeapon : MBSingleton<GUIWeapon>  {

    public Text ammoText;
    public Image weaponImage;

    public void UpdateWeaponState(WeaponController.WeaponState state)
	{
		
	}

	public void WeaponChange (WeaponManager.WeaponType activeWeapon)
	{
        //weaponImage.
        //Weapon[] table = GetWeaponTable()[(int)activeWeapon];
    }

    public void WeaponAmmoChange(int current, int capacity)
	{
        ammoText.text = string.Format("{0} / {1}", current, capacity);
    }

	public Weapon[] GetWeaponTable()
	{
		return WeaponManager.instance.GetWeaponTable ();
	}
}


