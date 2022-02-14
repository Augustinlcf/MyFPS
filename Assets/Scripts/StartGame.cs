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
    [SerializeField] private Canvas canvas;
    private GameObject weaponSelected;

    private void Start()
    {
        
    }
    
    public void Instantiate_weapon()
    {
        canvas.enabled = false;
        Instantiate(weaponSelected,spawn.position,transform.rotation);
    }
    
    public void Select_Sniper()
    {
        weaponSelected = sniper;
    }
    public void Select_Mp5()
    {
        weaponSelected = mp5;
    }
    public void Select_Shotgun()
    {
        weaponSelected = shotgun;
    }
}
