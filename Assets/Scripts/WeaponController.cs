using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

//using Random = System.Random;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject impactBullet;
    [SerializeField] private ParticleSystem muzzleFlash;
    
    public GameObject currentWeapon;
    private Transform currentCameraPosition;
    private Transform aimCameraPosition;
    private Transform notAimCameraPosition;
    private Transform weaponHolder;
    
    // WEAPON STATS
    private float aimingSpeed;
    private float bulletSpeed;
    private float aimSensitivity;
    private float aimMovementSpeed;
    private int fieldOfViewAim;
    private float normalSensitivity;
    private float normalMovementSpeed;
    public static int currentBulletinMagazine=0;
    public static int nbOfBulletInTotal=0;
    private int nbOfBulletPerShot;
    private float bulletDispersion;
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
    private GameObject bulletParent;

    private void Start()
    {
        // INITIALISATION
        aimingSpeed = StartGame.weaponData.aimingSpeed;
        bulletSpeed = StartGame.weaponData.bulletSpeed;
        aimSensitivity = StartGame.weaponData.aimSensitivity;
        aimMovementSpeed = StartGame.weaponData.aimMovementSpeed;
        nbOfBulletPerShot = StartGame.weaponData.bulletPerShot;
        bulletDispersion = StartGame.weaponData.bulletDispersion;
        fieldOfViewAim = StartGame.weaponData.fieldOfViewAim;
        rateOfFire = StartGame.weaponData.rateOfFire;
        hitForce = StartGame.weaponData.hitForce;
        currentBulletinMagazine = StartGame.weaponData.nbOfBulletsInMagazine;
        nbOfBulletInTotal = StartGame.weaponData.nbOfBulletsInTotal;
        
        // RECOIL
        recoilScript = transform.Find("Main Camera").GetComponent<Recoil>();
        
        // FIELD OF VIEW
        fpsCam = GetComponentInChildren<Camera>();
        
        // MASK
        layerMask = LayerMask.GetMask("Target","Default");
        
        // SENSITIVITY
        normalSensitivity = PlayerController.sensiMouse;
        normalMovementSpeed = PlayerController.playerSpeed;
        
        // DYNAMIC CROSSHAIR 
        _gameObject = GameObject.Find("GameObject");
        crosshair = _gameObject.GetComponent<Reticule>();
        
        // CROSSHAIR / SCOPE
        reticule = GameObject.Find("Reticule");
        scopeOverlay = GameObject.Find("CanvasSniperOverlay");

        bulletParent = GameObject.Find("BulletParent");
        
        // CAMERA POSITION
        smrs = currentWeapon.GetComponentsInChildren<SkinnedMeshRenderer>();
        cameraMain = currentWeapon.transform.Find("Main Camera").gameObject;
        currentCameraPosition = currentWeapon.transform.Find("Main Camera");
        aimCameraPosition = currentWeapon.transform.Find("WeaponHolder/AimTransform");
        notAimCameraPosition = currentWeapon.transform.Find("WeaponHolder/StopAimTransform");
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
        
                if (Input.GetMouseButtonDown(0) && Time.time > nextFire && currentBulletinMagazine>0)
                {
                    nextFire = Time.time + rateOfFire;
                    
                    recoilScript.RecoilFire();
                    muzzleFlash.Play();
                    currentBulletinMagazine--;
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
                                Quaternion.FromToRotation(Vector3.up, hit.normal),
                                bulletParent.transform);
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
                if (Input.GetMouseButton(0) && Time.time > nextFire && currentBulletinMagazine>0)
                {
                    nextFire = Time.time + rateOfFire;
                    
                    animator.SetBool("isFire",true);
                    recoilScript.RecoilFire();
                    muzzleFlash.Play();
                    currentBulletinMagazine--;
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
                                Quaternion.FromToRotation(Vector3.up, hit.normal),
                                bulletParent.transform);
                        }

                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
                        {
                            //hit.rigidbody.AddForce(-hit.normal * hitForce);
                            hit.collider.gameObject.GetComponent<EnemyAI>().TakeDamage(30);
                        }
                    }
                }
                break;
            
            case "Shotgun":
                animator.SetBool("isFire",false);
                
                if (Input.GetMouseButtonDown(0) && Time.time > nextFire && currentBulletinMagazine>0)
                {
                    nextFire = Time.time + rateOfFire;
                    
                    animator.SetBool("isFire",true);
                    recoilScript.RecoilFire();
                    muzzleFlash.Play();
                    currentBulletinMagazine--;
                    crosshair.ActiveDynamicCrosshair();
                   
                    weaponHolder = currentWeapon.transform.Find("WeaponHolder");
                    for (int i=0; i<nbOfBulletPerShot; i++)
                    {
                        Vector3 random = new Vector3(Random.Range(-bulletDispersion,bulletDispersion),Random.Range(-bulletDispersion,bulletDispersion),Random.Range(-bulletDispersion,bulletDispersion));
                        var bulletBody = Instantiate(
                            bullet,
                            weaponHolder.position + weaponHolder.forward*1.4f + weaponHolder.up*0.5f,
                            weaponHolder.rotation,
                            bulletParent.transform);
                        bulletBody.GetComponent<Rigidbody>().AddForce((weaponHolder.forward+random)*bulletSpeed); 
                    }
                    
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
                    muzzleFlash.Stop();
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
    

  
}
