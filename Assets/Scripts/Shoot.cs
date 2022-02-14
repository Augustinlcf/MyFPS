using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject mp5;
    [SerializeField] private GameObject shotgun;
    private GameObject weaponSelected;

    // Update is called once per frame
    void Update()
    {
        Tirer();
    }
    
    private void Tirer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var bulletBody = Instantiate(bullet,
                weaponSelected.transform.position + weaponSelected.transform.forward*1.7f,
                weaponSelected.transform.rotation,
                bulletParent);
            bulletBody.GetComponent<Rigidbody>().AddForce(transform.forward*3000);
        }
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
