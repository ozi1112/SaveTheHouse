using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]

public class EnemyController : MonoBehaviour, IHealth
{
    ///<summary>
    /// Maximum possible speed.
    ///</summary>
    public float speedMax = 1;

    ///<summary>
    /// Current speed.
    ///</summary>
    public float speed = 1;

    ///<summary>
    /// Specifies how much damage deals in sigle attack.
    ///</summary>
    public float attackPower = 1;

    ///<summary>
    /// Specifies attack speed.
    ///</summary>
    public float attackSpeed = 1;

    new Rigidbody rigidbody;

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
        StopMove();
        //TODO play anim + deal damage

    }

    #region MonoBehaviourImplementation

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.velocity = Vector3.right * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(0 == collision.collider.tag.CompareTo("Player"))
        {
            StartAttack();
        }
    }

    #endregion

    #region IHealth implementation
    public void Die()
    {
        StopMove();
        //TODO start die animation + destroyObject
    }
    #endregion
}
