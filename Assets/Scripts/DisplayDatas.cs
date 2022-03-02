using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDatas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bulletDisplay;
    [SerializeField] private Image healthbarImage;
    private int currentBulletinMagazine;
    private int nbOfBulletInTotal;
    
    void Update()
    {
        // HEALTHBAR
        healthbarImage.fillAmount = PlayerController.health / PlayerController.maxHealth;
        
        // MUNITIONS
        currentBulletinMagazine = WeaponController.currentBulletinMagazine;
        nbOfBulletInTotal = WeaponController.nbOfBulletInTotal;
        bulletDisplay.text = ""+ currentBulletinMagazine +"  /  "+ nbOfBulletInTotal;
    }
}
