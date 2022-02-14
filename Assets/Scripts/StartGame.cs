using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Transform spawn;
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject mp5;
    [SerializeField] private GameObject shotgun;
    private GameObject weaponSelected;
    
    public void Instantiate_weapon()
    {
        Instantiate(weaponSelected,spawn.transform);
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
