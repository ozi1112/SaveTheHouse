using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ToggleGroup))]

[System.Serializable]
public class WeaponController : MonoBehaviour
{
	public enum eWeaponState
	{
		Idle,
		Shooting,
		Reloading
	}

	AudioClip m_ClipShoot, m_ClipReload;

    EventProperty<eWeaponState> m_WeaponState = new EventProperty<eWeaponState>(eWeaponState.Idle);
   
	public EventProperty<int> m_CurrentAmmo = new EventProperty<int>(100);
    
	public EventProperty<int> m_MaxAmmo = new EventProperty<int>(100);

    float m_ReloadTime = 1;
	/// <summary>
	/// Weapon power - can't be 0.
	/// </summary>
	int m_Power = 1;

	bool m_ContinousFire = false;
	float m_ShootPerSecond = 10;

	void FixedUpdate()
	{
		if(m_WeaponState.val != eWeaponState.Reloading)
		{
			//Single shoot weapon can shoot faster on click spam
			if(Input.GetButtonDown("Fire") && !m_ContinousFire)
			{
				Shoot();
			}
			else if(Input.GetButton("Fire"))
			{
				if(m_WeaponState.val != eWeaponState.Shooting)
				{
					Shoot();
				}
			}
			
			
			if(Input.GetButton("Reload"))
			{
				Reload();
			}
		}
	}


	public void SetWeaponParams(int maxAmmo, float reloadTime, int power, bool continousFire, float shootPerSecond)
	{
		Debug.Log(string.Format("{0} {1} {2} {3} {4}", maxAmmo, reloadTime, power, continousFire, shootPerSecond));
		this.m_MaxAmmo.val = maxAmmo;
		this.m_CurrentAmmo.val = maxAmmo;
		this.m_ReloadTime = reloadTime;
		this.m_Power = power;
		this.m_ContinousFire = continousFire;
		this.m_ShootPerSecond = shootPerSecond;
	}


	void Shoot()
	{
		StartCoroutine (CoShoot());
	}

	void Reload()
	{
		if(!IsFullAmmo())
		{
			StopAllCoroutines ();
			StartCoroutine (CoReload());
		}
	}

	void ShootTrigger()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] hitObjects;
		hitObjects = Physics.RaycastAll (ray, 100.0f);
		foreach (RaycastHit hit in hitObjects) 
		{
			Health hp = hit.collider.gameObject.GetComponent<Health> ();
			if (hp != null) 
			{
				hp.Loss (m_Power);
			}
		}
	}

	IEnumerator CoShoot()
	{
		m_WeaponState.val = eWeaponState.Shooting;
		if (m_CurrentAmmo.val > 0) 
		{
			Debug.Log (string.Format("Shoot {0} / {1}", m_CurrentAmmo.val, m_MaxAmmo.val));
			ShootTrigger();
			m_CurrentAmmo.val --;
		}
		
		if (m_CurrentAmmo.val <= 0) 
		{
			yield return CoReload ();
		}
		else
		{
			yield return new WaitForSeconds (1.0f/m_ShootPerSecond);
		}

		m_WeaponState.val = eWeaponState.Idle;
		
	}

	IEnumerator CoReload()
	{
			Debug.Log (string.Format( "Reload {0} / {1}", m_CurrentAmmo, m_MaxAmmo ));
			m_WeaponState.val = eWeaponState.Reloading;
			yield return new WaitForSeconds (m_ReloadTime);
			m_CurrentAmmo.val = m_MaxAmmo.val;
			m_WeaponState.val = eWeaponState.Idle;
	}

	bool IsFullAmmo()
	{
		return m_CurrentAmmo == m_MaxAmmo;
	}


}
