using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    // OBJECTS TO BE DELETED (THE CHILDS)
    [SerializeField] private GameObject targetParent;
    [SerializeField] private GameObject bulletParent;
    
    // OBJECTS TO BE DELETED 
    private Transform player;
    
    // OBJECTS TO BE DISABLED/ENABLED
    [SerializeField] private Canvas canvasMenu;
    
    [SerializeField] private Canvas canvasOverlay;
    [SerializeField] private GameObject canvasOverlayObject;
    [SerializeField] private GameObject reticule;
    
    [SerializeField] private Canvas canvasSniper;
    [SerializeField] private GameObject canvasSniperObject;
    
    [SerializeField] private GameObject canvasMenuSelectSniper;
    [SerializeField] private GameObject canvasMenuSelectMp5;
    [SerializeField] private GameObject canvasMenuSelectShotgun;
    [SerializeField] private GameObject canvasMenuStart;
    [SerializeField] private GameObject canvasMenuBack;
    [SerializeField] private GameObject canvasMenuRestart;
    [SerializeField] private GameObject canvasDefeat;
    [SerializeField] private Camera cameraRotation;
    
    [SerializeField] private AudioSource audioSource;
    
    public void RestartGame()
    {
        WhatIsThePlayer();

        Cursor.lockState = CursorLockMode.None;
        
        // DESTROY
        player.GetComponent<WeaponController>().DestroyWeapon();
        foreach (Transform child in targetParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in bulletParent.transform)
        {
            Destroy(child.gameObject);
        }
        
        // ENABLE/DISABLE
        canvasMenu.enabled = true;
        
        canvasOverlay.enabled = false;
        canvasOverlayObject.SetActive(true);
        reticule.SetActive(true);

        canvasSniper.enabled = false;
        canvasSniperObject.SetActive(true);
        
        canvasMenuSelectSniper.SetActive(false);
        canvasMenuSelectMp5.SetActive(false);
        canvasMenuSelectShotgun.SetActive(false);
        canvasMenuStart.SetActive(false);
        canvasMenuBack.SetActive(false);
        canvasMenuRestart.SetActive(true);

        cameraRotation.enabled = true;
        
        // PLAY MUSIC MENU
        audioSource.Play();
    }
    private void WhatIsThePlayer()
    {
        if (StartGame.weaponData.weaponName == "Sniper")
        {
            player = GameObject.Find("Russian_sniper_prefab Variant(Clone)").transform;
        }
        else if (StartGame.weaponData.weaponName == "Mp5")
        {
            player = GameObject.Find("Submachine5_prefab Variant(Clone)").transform;
        }
        else if (StartGame.weaponData.weaponName == "Shotgun")
        {
            player = GameObject.Find("Shotgun3_prefab Variant(Clone)").transform;
        }
    }

    public void Death()
    {
        canvasDefeat.SetActive(true);
    }
}
