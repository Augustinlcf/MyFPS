using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletParent;
    public GameObject currentWeapon;
    private Transform currentCameraPosition;
    private Transform aimCameraPosition;
    private Transform notAimCameraPosition;
    private float aimingSpeed;
    private float bulletSpeed;
    private float aimSensitivity;
    private float aimMovementSpeed;
    private float normalSensitivity;
    private float normalMovementSpeed;
    private GameObject reticule;

    private void Start()
    {
        aimingSpeed = StartGame.weaponData.aimingSpeed;
        bulletSpeed = StartGame.weaponData.bulletSpeed;
        aimSensitivity = StartGame.weaponData.aimSensitivity;
        aimMovementSpeed = StartGame.weaponData.aimMovementSpeed;

        normalSensitivity = PlayerController.sensiMouse;
        normalMovementSpeed = PlayerController.playerSpeed;
        
        reticule = GameObject.Find("Reticule");
    }

    void Update()
    {
        Shoot();
        Aim(Input.GetMouseButton(1));
    }
    
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var bulletBody = Instantiate(bullet,
                transform.position + transform.forward*1.7f,
                transform.rotation,
                bulletParent);
            bulletBody.GetComponent<Rigidbody>().AddForce(transform.forward*bulletSpeed);
        }
    }

    private void Aim(bool isAiming)
    {
        ;
        switch (StartGame.weaponData.weaponName)
        {
            case "Shotgun":
            
                currentCameraPosition = currentWeapon.transform.Find("Main Camera");
                aimCameraPosition = currentWeapon.transform.Find("AimTransform");
                notAimCameraPosition = currentWeapon.transform.Find("StopAimTransform");
                
                if (isAiming)
                {
                    PlayerController.sensiMouse = aimSensitivity;
                    PlayerController.playerSpeed = aimMovementSpeed;
                    
                    reticule.SetActive(false);
                    
                    currentCameraPosition.position = Vector3.Lerp(currentCameraPosition.position,
                        aimCameraPosition.position,
                        Time.deltaTime * aimingSpeed);
                }
                else
                {
                    PlayerController.sensiMouse = normalSensitivity;
                    PlayerController.playerSpeed = normalMovementSpeed;
                    
                    reticule.SetActive(true);
                    
                    currentCameraPosition.position = Vector3.Lerp(currentCameraPosition.position,
                        notAimCameraPosition.position,
                        Time.deltaTime * aimingSpeed);
                }
                break;
            
            
            case "Sniper":
                
                SkinnedMeshRenderer[] smrs = currentWeapon.GetComponentsInChildren<SkinnedMeshRenderer>();
                GameObject cameraMain = currentWeapon.transform.Find("Main Camera").gameObject;
                currentCameraPosition = cameraMain.transform;
                aimCameraPosition = currentWeapon.transform.Find("AimTransform");
                notAimCameraPosition = currentWeapon.transform.Find("StopAimTransform");
                
                if (isAiming)
                {
                    PlayerController.sensiMouse = aimSensitivity;
                    PlayerController.playerSpeed = aimMovementSpeed;
                    
                    reticule.SetActive(false);
                    
                    currentCameraPosition.position = Vector3.Lerp(currentCameraPosition.position,
                        aimCameraPosition.position,
                        Time.deltaTime * StartGame.weaponData.aimingSpeed);
                    
                    cameraMain.GetComponent<Camera>().fieldOfView = 10;
                    
                    foreach (SkinnedMeshRenderer smr in smrs)
                    {
                        smr.enabled = false;
                    }
                    
                }
                else
                {
                    
                    PlayerController.sensiMouse = normalSensitivity;
                    PlayerController.playerSpeed = normalMovementSpeed;
                    
                    reticule.SetActive(true);
                    
                    currentCameraPosition.position = Vector3.Lerp(currentCameraPosition.position,
                        notAimCameraPosition.position,
                        Time.deltaTime * StartGame.weaponData.aimingSpeed);
                    cameraMain.GetComponent<Camera>().fieldOfView = 60;
                    foreach (SkinnedMeshRenderer smr in smrs)
                    {
                        smr.enabled = true;
                    }
                }
                break;
        }

       
        
        
    }

  
}
