using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIWeapon : MBSingleton<GUIWeapon>  {

    public Text ammoText;
    public Image weaponImage;

    void Start()
    {
        PlayerController.instance.weaponController.m_CurrentAmmo.OnChange += UpdateWeaponAmmo;
        PlayerController.instance.weaponController.m_MaxAmmo.OnChange += UpdateWeaponAmmo;
        PlayerController.instance.weaponManager.currentWeapon.OnChange += WeaponChange;
        UpdateWeaponAmmo();
    }

    void UpdateWeaponAmmo()
    {
        ammoText.text = string.Format(
        "{0} / {1}", 
        PlayerController.instance.weaponController.m_CurrentAmmo.val,  
        PlayerController.instance.weaponController.m_MaxAmmo.val);
    }

    public void UpdateWeaponState(WeaponController.eWeaponState state)
	{
		switch(state)
        {
            case WeaponController.eWeaponState.Idle:
            {
                break;
            }
             case WeaponController.eWeaponState.Reloading:
            {
                //Animate reload
                break;
            }
             case WeaponController.eWeaponState.Shooting:
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


