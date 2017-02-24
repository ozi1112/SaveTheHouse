using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIWeapon : MBSingleton<GUIWeapon>  {

    public Text ammoText;
    public Image weaponImage;

    void Start()
    {
        UpdateWeaponAmmo();
    }

    void UpdateWeaponAmmo()
    {
        ammoText.text = string.Format(
        "{0} / {1}", 
        WeaponController.instance.currentAmmo,  
        WeaponController.instance.maxAmmo);
    }

    public void UpdateWeaponState(WeaponController.WeaponState state)
	{
		switch(state)
        {
            case WeaponController.WeaponState.Idle:
            {
                break;
            }
             case WeaponController.WeaponState.Reloading:
            {
                //Animate reload
                break;
            }
             case WeaponController.WeaponState.Shooting:
            {
                break;
            }
        }

	}

	public void WeaponChange (WeaponManager.WeaponType activeWeapon)
	{
        Debug.Log(string.Format("Change weapon {0}", activeWeapon.ToString()));
        Weapon weapon = GetWeaponTable()[(int)activeWeapon];
        weaponImage.sprite = weapon.icon;
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


