using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // TRANSFORM
    [SerializeField] private Transform playerSpawn;
    
    // GAME OBJECTS
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject mp5;
    [SerializeField] private GameObject shotgun;
    private GameObject weaponSelected;
    
    // SCRIPTABLE OBJECTS WEAPON DATA
    [SerializeField] private WeaponData sniperData;
    [SerializeField] private WeaponData mp5Data;
    [SerializeField] private WeaponData shotgunData;
    public static WeaponData weaponData;
    
    // CANVAS
    [SerializeField] private Canvas canvasMenu;
    [SerializeField] private Canvas canvasOverlay;
    [SerializeField] private Canvas canvasSniper;
    
    // MUSIC
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        weaponData = sniperData;
    }

    private void Start()
    {
        audioSource.Play();
        canvasMenu.enabled = true;
        canvasOverlay.enabled = false;
        canvasSniper.enabled = false;
    }
    
    // Appeler lors de l'appui sur le bouton Start
    public void Instantiate_weapon()
    {
        audioSource.Stop();
        canvasMenu.enabled = false;
        canvasSniper.enabled = true;
        canvasOverlay.enabled = true;
        Instantiate(weaponSelected,playerSpawn.position,transform.rotation);
    }
    
    // Appeler lors de l'appui sur le bouton Sniper
    public void Select_Sniper()
    {
        weaponSelected = sniper;
        weaponData = sniperData;
    }
    // Appeler lors de l'appui sur le bouton Mp5
    public void Select_Mp5()
    {
        weaponSelected = mp5;
        weaponData = mp5Data;
    }
    // Appeler lors de l'appui sur le bouton Shotgun
    public void Select_Shotgun()
    {
        weaponSelected = shotgun;
        weaponData = shotgunData;
    }
}
