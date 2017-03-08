using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ToggleGroup))]

[System.Serializable]
public class WeaponController : MonoBehaviour
{
	AudioClip clipShoot, clipReload;

	public enum WeaponState
	{
		Idle,
		Shooting,
		Reloading
	}

	
    EventProperty<WeaponState> weaponState = new EventProperty<WeaponState>(WeaponState.Idle);
   
	public EventProperty<int> currentAmmo = new EventProperty<int>(100);
    
	public EventProperty<int> maxAmmo = new EventProperty<int>(100);

    bool stopShootingRequest = false;

    float reloadTime = 1;
	/// <summary>
	/// Weapon power - can't be 0.
	/// </summary>
	int power = 1;

	bool continousFire = false;
	float shootPerSecond = 10;

	void FixedUpdate()
	{
		if(Input.GetButtonDown("Fire"))
		{
			StartShooting();
		}
		else if(Input.GetButtonUp("Fire"))
		{
			StopShooting();
		}
		else if(Input.GetButtonDown("Reload"))
		{
			Reload();
		}
	}

	public void StartShooting()
	{
		stopShootingRequest = false;
		if(weaponState.val == WeaponState.Idle)
		{
			if (continousFire) {
				StartCoroutine (CoContinousFire());
			} else {
				StartCoroutine (CoShoot());
			}
		}
	}

	public void SetWeaponParams(int maxAmmo, float reloadTime, int power, bool continousFire, float shootPerSecond)
	{
		this.maxAmmo.val = maxAmmo;
		this.currentAmmo.val = maxAmmo;
		this.reloadTime = reloadTime;
		this.power = power;
		this.continousFire = continousFire;
		this.shootPerSecond = shootPerSecond;
	}

	public void StopShooting()
	{
		//Request to break shooting loop
		stopShootingRequest = true;
	}

	public void Reload()
	{
		if(!IsFullAmmo())
		{
			StopAllCoroutines ();
			StartCoroutine (CoReload());
			stopShootingRequest = false;
		}
	}

	public void ShootTrigger()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] hitObjects;
		hitObjects = Physics.RaycastAll (ray, 100.0f);
		foreach (RaycastHit hit in hitObjects) 
		{
			Health hp = hit.collider.gameObject.GetComponent<Health> ();
			if (hp != null) 
			{
				hp.Loss (power);
			}
		}
	}

	IEnumerator CoShoot()
	{
		weaponState.val = WeaponState.Shooting;
		if (currentAmmo.val > 0) 
		{
			Debug.Log (string.Format("Shoot {0} / {1}", currentAmmo.val, maxAmmo.val));
			ShootTrigger();
		}

		currentAmmo.val --;
		if (currentAmmo.val == 0) 
		{
			yield return CoReload ();
		}
		else if (continousFire) 
		{
			yield return new WaitForSeconds (1.0f/shootPerSecond);
		} else 
		{
			weaponState.val = WeaponState.Idle;
		}
	}

	IEnumerator CoContinousFire()
	{
		Debug.Log ("ContinousFire");
		weaponState.val = WeaponState.Shooting;
		while (!stopShootingRequest) 
		{
			yield return CoShoot ();
		}

		weaponState.val = WeaponState.Idle;
		stopShootingRequest = false;

		yield return 1;
	}


	IEnumerator CoReload()
	{
		Debug.Log (string.Format( "Reload {0} / {1}", currentAmmo, maxAmmo ));
		weaponState.val = WeaponState.Reloading;
		yield return new WaitForSeconds (reloadTime);
		currentAmmo = maxAmmo;
		weaponState.val = WeaponState.Idle;
	}

	bool IsFullAmmo()
	{
		return currentAmmo == maxAmmo;
	}


}
