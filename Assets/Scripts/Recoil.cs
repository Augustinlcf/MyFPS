using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached on camera weapon
public class Recoil : MonoBehaviour
{
    //Rotations
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    
    //HipFire Recoil
    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;
    
    //Settings
    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    private GameObject weaponHolder;
    void Start()
    {
        weaponHolder = GameObject.Find("WeaponHolder");
    }

  
    void Update()
    {
        // L'arme cherche à se restabiliser à chaque frame
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        
        // L'arme et la camera subissent le recul
        transform.localRotation = Quaternion.Euler(currentRotation);
        weaponHolder.transform.localRotation = Quaternion.Euler(currentRotation);
    }

    // Appeler à chaque tir
    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX,
            Random.Range(-recoilY, recoilY),
            Random.Range(-recoilZ, recoilZ));
    }
}
