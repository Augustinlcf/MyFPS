using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMouvement : MonoBehaviour
{
    private int vitesse;
    
    // Start is called before the first frame update
    void Start()
    {
        vitesse = Random.Range(1, 10);
        
        transform.Rotate(0, Random.Range(0,360),0);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.left * Time.deltaTime * vitesse );
    }
}
