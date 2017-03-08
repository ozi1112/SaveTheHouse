using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponManager))]
[RequireComponent(typeof(WeaponController))]
[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(Health))]

public class PlayerController : MBSingleton<PlayerController>, IHealth
{
    public WeaponManager weaponManager;
    public WeaponController weaponController;
    public Wallet wallet;

    public event DelVoid OnDie;
    

	void Start()
    {
        wallet = GetComponent<Wallet>();
        weaponController = GetComponent<WeaponController>();
        weaponManager = GetComponent<WeaponManager>();
    }

    public void Die()
    {
        OnDie.Invoke();
    }
}
