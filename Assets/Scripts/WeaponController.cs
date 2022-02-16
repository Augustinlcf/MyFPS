using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private Animator animator;
    public GameObject currentWeapon;
    private Transform currentCameraPosition;
    private Transform aimCameraPosition;
    private Transform notAimCameraPosition;
    private float aimingSpeed;
    private float bulletSpeed;
    private float aimSensitivity;
    private float aimMovementSpeed;
    private int fieldOfViewAim;
    private float normalSensitivity;
    private float normalMovementSpeed;
    private int hitForce;
    private float rateOfFire;
    private float nextFire;
    private LayerMask layerMask;
    private Camera fpsCam;
    private GameObject reticule;
    private GameObject scopeOverlay;

    private GameObject cameraMain;
    private SkinnedMeshRenderer[] smrs;

    private void Start()
    {
        aimingSpeed = StartGame.weaponData.aimingSpeed;
        bulletSpeed = StartGame.weaponData.bulletSpeed;
        aimSensitivity = StartGame.weaponData.aimSensitivity;
        aimMovementSpeed = StartGame.weaponData.aimMovementSpeed;
        fieldOfViewAim = StartGame.weaponData.fieldOfViewAim;
        rateOfFire = StartGame.weaponData.rateOfFire;
        hitForce = StartGame.weaponData.hitForce;
        fpsCam = GetComponentInChildren<Camera>();
        layerMask = LayerMask.GetMask("Target");

        normalSensitivity = PlayerController.sensiMouse;
        normalMovementSpeed = PlayerController.playerSpeed;
        
        reticule = GameObject.Find("Reticule");
        scopeOverlay = GameObject.Find("CanvasSniperOverlay");
        
        smrs = currentWeapon.GetComponentsInChildren<SkinnedMeshRenderer>();
        cameraMain = currentWeapon.transform.Find("Main Camera").gameObject;
        currentCameraPosition = currentWeapon.transform.Find("Main Camera");
        aimCameraPosition = currentWeapon.transform.Find("AimTransform");
        notAimCameraPosition = currentWeapon.transform.Find("StopAimTransform");

    }

    void Update()
    {
        Shoot();
        Aim(Input.GetMouseButton(1));
    }
    
    private void Shoot()
    {
        switch (StartGame.weaponData.weaponName)
        {
                
            case "Sniper":
                animator.SetBool("isFire",false);
                animator.SetBool("isFireWhenScoping",false);
                if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
                {
                    animator.SetBool("isFire",true);
                    animator.SetBool("isFireWhenScoping",true);

                    nextFire = Time.time + rateOfFire;
                    Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(
                            rayOrigin,
                            fpsCam.transform.forward,
                            out hit,
                            Mathf.Infinity,
                            layerMask))
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitForce);
                        hit.collider.gameObject.GetComponent<Damage>().Downgrades();
                    }
                    
                    
                }
                break;
            case "Mp5":
                animator.SetBool("isFire",false);
                if (Input.GetMouseButton(0) && Time.time > nextFire)
                {
                    animator.SetBool("isFire",true);

                    nextFire = Time.time + rateOfFire;
                    Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(
                            rayOrigin,
                            fpsCam.transform.forward,
                            out hit,
                            Mathf.Infinity,
                            layerMask))
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitForce);
                        hit.collider.gameObject.GetComponent<Damage>().Downgrades();
                    }
                    
                }
                break;
            case "Shotgun":
                animator.SetBool("isFire",false);
                if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
                {
                    animator.SetBool("isFire",true);
                    nextFire = Time.time + rateOfFire;
                    var bulletBody = Instantiate(
                        bullet,
                        transform.position + transform.forward*1.7f,
                        transform.rotation,
                        bulletParent);
                    bulletBody.GetComponent<Rigidbody>().AddForce(transform.forward*bulletSpeed);
                }
                
                break;
            }
            
        
    }

    private void Aim(bool isAiming)
    {
        
        switch (StartGame.weaponData.weaponName)
        {
            case "Shotgun":
                if (isAiming)
                {
                    PlayerController.sensiMouse = aimSensitivity;
                    PlayerController.playerSpeed = aimMovementSpeed;
                    
                    animator.SetBool("isScope",true);
                    
                    reticule.SetActive(false);
                    
                    currentCameraPosition.position = Vector3.Lerp(currentCameraPosition.position,
                        aimCameraPosition.position,
                        Time.deltaTime * aimingSpeed);
                }
                else
                {
                    PlayerController.sensiMouse = normalSensitivity;
                    PlayerController.playerSpeed = normalMovementSpeed;
                    
                    animator.SetBool("isScope",false);
                    
                    reticule.SetActive(true);
                    scopeOverlay.SetActive(false);
                    
                    currentCameraPosition.position = Vector3.Lerp(currentCameraPosition.position,
                        notAimCameraPosition.position,
                        Time.deltaTime * aimingSpeed);
                }
                break;
            
            
            case "Sniper":
                if (isAiming)
                {
                    PlayerController.sensiMouse = aimSensitivity;
                    PlayerController.playerSpeed = aimMovementSpeed;
                    
                    animator.SetBool("isScope",true);
                    
                    reticule.SetActive(false);
                    scopeOverlay.SetActive(true);

                    currentCameraPosition.position = Vector3.Lerp(currentCameraPosition.position,
                        aimCameraPosition.position,
                        Time.deltaTime * aimingSpeed);
                    
                    cameraMain.GetComponent<Camera>().fieldOfView = fieldOfViewAim;
                    
                    foreach (SkinnedMeshRenderer smr in smrs)
                    {
                        smr.enabled = false;
                    }
                    
                }
                else
                {
                    PlayerController.sensiMouse = normalSensitivity;
                    PlayerController.playerSpeed = normalMovementSpeed;
                    
                    animator.SetBool("isScope",false);
                    
                    reticule.SetActive(true);
                    scopeOverlay.SetActive(false);
                    
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
            
            case "Mp5":
                if (isAiming)
                {
                    PlayerController.sensiMouse = aimSensitivity;
                    PlayerController.playerSpeed = aimMovementSpeed;
                    
                    animator.SetBool("isScope",true);
                    
                    reticule.SetActive(false);

                    currentCameraPosition.position = Vector3.Lerp(currentCameraPosition.position,
                        aimCameraPosition.position,
                        Time.deltaTime * aimingSpeed);
                    
                    cameraMain.GetComponent<Camera>().fieldOfView = fieldOfViewAim;
                }
                
                else
                {
                    PlayerController.sensiMouse = normalSensitivity;
                    PlayerController.playerSpeed = normalMovementSpeed;
                    
                    animator.SetBool("isScope",false);
                    
                    reticule.SetActive(true);
                    scopeOverlay.SetActive(false);
                    
                    currentCameraPosition.position = Vector3.Lerp(currentCameraPosition.position,
                        notAimCameraPosition.position,
                        Time.deltaTime * StartGame.weaponData.aimingSpeed);
                    cameraMain.GetComponent<Camera>().fieldOfView = 60;
                }
                break;
        }
        
    }
    

  
}
