using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootGun : MonoBehaviour
{

    [SerializeField] private PhotonView myPV;

    [SerializeField] private GameObject impactEffect;

    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = .75f;
    [SerializeField] float gunDamage;

    private float nextTimeToFire = 0f;

    private Camera fpsCam;

    void Start()
    {
        fpsCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (myPV.IsMine && Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Debug.Log("fire");
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.collider.gameObject.name);
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(gunDamage);
        }

    }



}


