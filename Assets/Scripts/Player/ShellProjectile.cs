using System.Runtime.CompilerServices;
using UnityEngine;

public class ShellProjectile : MonoBehaviour
{
    #region Variables

    [SerializeField] private float speed = 40f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private AudioClip impactSound;

    #endregion

    #region Game Cycle Methods

    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    #endregion

    #region Game Mechanic Methods

    void OnCollisionEnter(Collision collision)
    {
        if (impactEffect)
            Instantiate(impactEffect, transform.position, Quaternion.identity);

        if (impactSound)
            AudioSource.PlayClipAtPoint(impactSound, transform.position);

        Destroy(gameObject);
    }

    #endregion
}
