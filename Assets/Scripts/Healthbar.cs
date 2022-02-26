using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbarImage;
    void Update()
    {
        healthbarImage.fillAmount = PlayerController.health / PlayerController.maxHealth;

    }
}
