using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Transform spawn;
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject mp5;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private WeaponData sniperData;
    [SerializeField] private WeaponData mp5Data;
    [SerializeField] private WeaponData shotgunData;
    [SerializeField] private Canvas canvasMenu;
    [SerializeField] private Canvas canvasOverlay;
    private GameObject weaponSelected;
    public static WeaponData weaponData;

    private void Start()
    {
        canvasMenu.enabled = true;
        canvasOverlay.enabled = false;
    }
    
    public void Instantiate_weapon()
    {
        canvasMenu.enabled = false;
        canvasOverlay.enabled = true;
        Instantiate(weaponSelected,spawn.position,transform.rotation);
    }
    
    public void Select_Sniper()
    {
        weaponSelected = sniper;
        weaponData = sniperData;
    }
    public void Select_Mp5()
    {
        weaponSelected = mp5;
        weaponData = mp5Data;
    }
    public void Select_Shotgun()
    {
        weaponSelected = shotgun;
        weaponData = shotgunData;
    }
}
