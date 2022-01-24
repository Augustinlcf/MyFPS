using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMouvement : MonoBehaviour
{
    private int vitesse;
    //private int _speed = Random.Range(2, 30);
    // Start is called before the first frame update
    void Start()
    {
        vitesse = Random.Range(0, 10);
        
        transform.Rotate(0, Random.Range(0,360),0);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.left * Time.deltaTime * vitesse );
    }
}
