using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootRocket : MonoBehaviour
{
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float radius;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = .75f;
    [SerializeField] private float MaxDistance = 10f;
    [SerializeField] private float MinDistance = 1f;
    [SerializeField] private float MaxImpactForce = 3000f;
    [SerializeField] private float MinImpactForce = 2000f;

    private float nextTimeToFire = 0f;

    private Camera fpsCam;
    void Start()
    {
        fpsCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.distance < MaxDistance)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }

            applyRocketJump(giveForce(hit.distance));
            onhit(hit);
        }
        else
        {
            applyRocketJump(MinImpactForce);
        }
    }

    float giveForce(float distance)
    {
        float dist = Mathf.Clamp(MaxDistance - (Mathf.Max(distance, MinDistance)), 0, MaxDistance-MinDistance);
        float percentage = dist / (MaxDistance-MinDistance);
        return ((MaxImpactForce - MinImpactForce) *percentage) + MinImpactForce;
    }

    //Calculate rocket Jump
    void applyRocketJump(float force)
    {
        rb.AddForce(-transform.forward* force, ForceMode.Impulse);
    }

    void onhit(RaycastHit hit)
    {
        Collider[] hitColliders = Physics.OverlapSphere(hit.point, radius);
        foreach (var hitCollider in hitColliders)
        {
            //save this for later
        }

        Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
