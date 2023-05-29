using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15;
    public float aimDuration = 0.3f;
    public Rig aimLayer;

    private Camera mainCamera;
    private RaycastWeapon weapon;

    private void Awake()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    private void LateUpdate()
    {
        AnimationUpdate();
        WeaponInputUpdate();
    }

    private void FixedUpdate()
    {
        TurnUpdate();
    }

    private void TurnUpdate()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void AnimationUpdate()
    {
        // 줌 애니메이션 실행
        if (Input.GetMouseButton(1))
        {
            aimLayer.weight += Time.deltaTime / aimDuration;
        }
        // 줌 애니메이션 헤제
        else
        {
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }
    }

    private void WeaponInputUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.StartFiring();
        }
        if (weapon.isFiring)
        {
            weapon.UpdateFiring(Time.deltaTime);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            weapon.StopFiring();
        }
    }
}