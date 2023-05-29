using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public bool isFiring = false;

    [Space(10)]
    [Header("무기 스펙 관련")]
    public float fireRate = 25;

    [Space(10)]
    [Header("무기 이펙트")]
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;

    [Space(10)]
    [Header("레이 케스트")]
    public Transform raycastOrigin;
    public Transform raycastDestination;

    [Space(10)]
    [Header("레이어 마스크")]
    public LayerMask environmentLayer;

    private float accumulatedTime;

    private Ray ray;
    private RaycastHit hitInfo;

    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0.0f;
        FireBullet();
    }

    public void UpdateFiring(float daltaTime)
    {
        accumulatedTime += daltaTime;

        float fireInterval = 1f / fireRate;

        while (accumulatedTime >= 0)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    private void FireBullet()
    {
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);

        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, environmentLayer))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);

            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}