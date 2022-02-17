using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject impactBullet;
    [SerializeField] private float recoilCoefficient;
    private float recoilValue;
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
    private float recoilSpeed;
    public static int currentBulletinMagazine=0;
    public static int nbOfBulletInTotal=0;
    private int hitForce;
    private float rateOfFire;
    private float nextFire;
    private LayerMask layerMask;
    private Camera fpsCam;
    private GameObject reticule;
    private GameObject scopeOverlay;
    private GameObject cameraMain;
    private SkinnedMeshRenderer[] smrs;
    private Reticule crosshair;
    private GameObject _gameObject;
    private Recoil recoilScript;

    private void Start()
    {
        aimingSpeed = StartGame.weaponData.aimingSpeed;
        bulletSpeed = StartGame.weaponData.bulletSpeed;
        aimSensitivity = StartGame.weaponData.aimSensitivity;
        aimMovementSpeed = StartGame.weaponData.aimMovementSpeed;
        recoilSpeed = StartGame.weaponData.recoilSpeed;
        fieldOfViewAim = StartGame.weaponData.fieldOfViewAim;
        rateOfFire = StartGame.weaponData.rateOfFire;
        hitForce = StartGame.weaponData.hitForce;
        currentBulletinMagazine = StartGame.weaponData.nbOfBulletsInMagazine;
        nbOfBulletInTotal = StartGame.weaponData.nbOfBulletsInTotal;

        recoilValue = 0;
        recoilScript = transform.Find("Main Camera").GetComponent<Recoil>();
        
        fpsCam = GetComponentInChildren<Camera>();
        layerMask = LayerMask.GetMask("Target","Default");

        normalSensitivity = PlayerController.sensiMouse;
        normalMovementSpeed = PlayerController.playerSpeed;

        _gameObject = GameObject.Find("GameObject");
        crosshair = _gameObject.GetComponent<Reticule>();
        
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
        Recharger(Input.GetKeyDown(KeyCode.R));
    }
    
    private void Shoot()
    {
        switch (StartGame.weaponData.weaponName)
        {
                
            case "Sniper":
                animator.SetBool("isFire",false);
                animator.SetBool("isFireWhenScoping",false);
                if (Input.GetMouseButtonDown(0) && Time.time > nextFire && currentBulletinMagazine>0)
                {
                    animator.SetBool("isFire",true);
                    animator.SetBool("isFireWhenScoping",true);

                    currentBulletinMagazine--;
                    nextFire = Time.time + rateOfFire;
                    
                    crosshair.ActiveDynamicCrosshair();
                    
                    Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(
                            rayOrigin,
                            fpsCam.transform.forward,
                            out hit,
                            Mathf.Infinity,
                            layerMask))
                    {
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Default"))
                        {
                            Instantiate(
                                impactBullet, 
                                hit.point + hit.normal * 0.01f,
                                Quaternion.FromToRotation(Vector3.up, hit.normal));
                        }

                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
                        {
                            hit.rigidbody.AddForce(-hit.normal * hitForce);
                            hit.collider.gameObject.GetComponent<Damage>().Downgrades();
                        }
                      
                    }
                }
                break;
            
            case "Mp5":
                animator.SetBool("isFire",false);
                animator.SetBool("isFireWhenScoping",false);
                if (Input.GetMouseButton(0) && Time.time > nextFire && currentBulletinMagazine>0)
                {
                    animator.SetBool("isFire",true);
                    animator.SetBool("isFireWhenScoping",true);
                    
                    recoilScript.RecoilFire();

                    currentBulletinMagazine--;
                    nextFire = Time.time + rateOfFire;
                    
                    crosshair.ActiveDynamicCrosshair();
                    
                    Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(
                            rayOrigin,
                            fpsCam.transform.forward + new Vector3(0,recoilValue,0),
                            out hit,
                            Mathf.Infinity,
                            layerMask))
                    {
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Default"))
                        {
                            Instantiate(
                                impactBullet,
                                hit.point + hit.normal * 0.01f,
                                Quaternion.FromToRotation(Vector3.up, hit.normal));
                        }

                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
                        {
                            hit.rigidbody.AddForce(-hit.normal * hitForce);
                            hit.collider.gameObject.GetComponent<Damage>().Downgrades();
                        }

                        //recoilValue += recoilCoefficient;
                        //recoilValue -= Mathf.Lerp(recoilValue, recoilValue-recoilCoefficient, recoilSpeed);
                        //StartCoroutine(recoilCancel());
                    }
                }
                break;
            
            case "Shotgun":
                animator.SetBool("isFire",false);
                
                if (Input.GetMouseButtonDown(0) && Time.time > nextFire && currentBulletinMagazine>0)
                {
                    animator.SetBool("isFire",true);
                    
                    currentBulletinMagazine--;
                    nextFire = Time.time + rateOfFire;
                    
                    crosshair.ActiveDynamicCrosshair();
                    
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

    private void Recharger(bool isReloading)
    {
        if (isReloading)
        {
            int nbOfBulletToReload = StartGame.weaponData.nbOfBulletsInMagazine - currentBulletinMagazine;
            nbOfBulletInTotal -= nbOfBulletToReload;
            currentBulletinMagazine = StartGame.weaponData.nbOfBulletsInMagazine;
        }
    }

    IEnumerator recoilCancel()
    {
        yield return new WaitForSeconds(recoilSpeed);
        recoilValue = Mathf.Lerp(recoilValue,0,recoilSpeed);
    }
    

  
}
