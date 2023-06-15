using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootGun : MonoBehaviour
{

    [SerializeField] private PhotonView myPV;

    [SerializeField] private GameObject impactEffect;
    [SerializeField] private ParticleSystem muzzleFlash;

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
            muzzleFlash.Play();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(gunDamage);
            GameObject pro = PhotonNetwork.Instantiate(impactEffect.name, hit.point, Quaternion.LookRotation(hit.normal));
        }

    }



}


