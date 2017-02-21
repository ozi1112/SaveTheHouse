using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IHealth
{
	public float speedMax = 1;
	public float speed = 1;

	public float attackPower = 1;
	public float attackSpeed = 1;

	new Rigidbody rigidbody;

	// Use this for initialization
	void Start ()
	{
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		rigidbody.velocity = Vector3.right * speed;
	}

	public void StartMove()
	{
		speed = speedMax;
	}

	public void StopMove()
	{
		speed = 0;
	}

	public void StartAttack()
	{
		StopMove ();
		//TODO
	}

	void OnCollisionEnter(Collision collision) 
	{
		StartAttack ();
	}

	#region IHealth implementation
	public void Die ()
	{
		StopMove ();
		//throw new System.NotImplementedException ();
	}
	#endregion
}
