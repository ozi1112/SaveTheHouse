using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIWeapon : MBSingleton<GUIWeapon>  {

    public Text ammoText;
    public Image weaponImage;

    void Start()
    {
        PlayerController.instance.weaponController.currentAmmo.OnChange += UpdateWeaponAmmo;
        PlayerController.instance.weaponController.maxAmmo.OnChange += UpdateWeaponAmmo;
        PlayerController.instance.weaponManager.currentWeapon.OnChange += WeaponChange;
        UpdateWeaponAmmo();
    }

    void UpdateWeaponAmmo()
    {
        ammoText.text = string.Format(
        "{0} / {1}", 
        PlayerController.instance.weaponController.currentAmmo.val,  
        PlayerController.instance.weaponController.maxAmmo.val);
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

	public void WeaponChange ()
	{
        WeaponType activeWeapon = PlayerController.instance.weaponManager.currentWeapon.val;
        Debug.Log(string.Format("Change weapon {0}", activeWeapon.ToString()));
        Weapon weapon = GetWeaponTable()[(int)activeWeapon];
        weaponImage.sprite = weapon.icon;
    }

	public Weapon[] GetWeaponTable()
	{
		return WeaponManager.instance.GetWeaponTable ();
	}

}


