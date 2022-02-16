using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private Renderer rend;
    private int lives = 3;
    

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Downgrades();
        }

    }

    public void Downgrades()
    {
        MeshRenderer[] mfs = gameObject.GetComponentsInChildren<MeshRenderer>();
        
        switch (lives)
        {
            case 3:
                foreach (MeshRenderer mf in mfs)
                {
                    mf.material.SetColor(("_Color"),Color.yellow);
                }
                lives--;
                break;
            case 2:
                foreach (MeshRenderer mf in mfs)
                {
                    mf.material.SetColor(("_Color"),Color.red);
                }
                lives--;
                break;
            case 1:
                GameObject.Destroy(gameObject);
                break;
        }
        
    }
}
