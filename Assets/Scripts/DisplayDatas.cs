using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayDatas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bulletDisplay;
    private int currentBulletinMagazine;
    private int nbOfBulletInTotal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentBulletinMagazine = WeaponController.currentBulletinMagazine;
        nbOfBulletInTotal = WeaponController.nbOfBulletInTotal;
        bulletDisplay.text = ""+ currentBulletinMagazine +"  /  "+ nbOfBulletInTotal;
    }
}
