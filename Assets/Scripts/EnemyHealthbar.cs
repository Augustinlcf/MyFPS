using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    private Image healthbarImage;
    void Start()
    {
        healthbarImage = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        healthbarImage.fillAmount = GetComponentInParent<EnemyAI>().health / GetComponentInParent<EnemyAI>().maxhealth;
    }
}
