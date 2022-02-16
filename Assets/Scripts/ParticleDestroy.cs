using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    [SerializeField]private float timeBeforeDestroy;
    void Start()
    {
        Destroy(this.gameObject,timeBeforeDestroy);
    }
    
}
