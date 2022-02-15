using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float playerSpeed = 30;
    public static float sensiMouse = 1;
    private float yawn = 0f;
    private float pitch = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  //bloque le curseur sur l'ecran
    }

    // Update is called once per frame
    void Update()
    {
        Deplacer();
        Tourner();
    }

    private void Deplacer()
    {
        
        //changer translation
        Vector3 deltaPosition = (transform.right * Input.GetAxis("Horizontal") +   //diriger avec les fleches directionnelles
                                 transform.forward * Input.GetAxis("Vertical"))
                                *Time.deltaTime*playerSpeed;
        deltaPosition.y = 0f;  // pour ne pas monter ou descendre
        transform.position += deltaPosition;
    }

    private void Tourner()
    {
        //changer rotation
        yawn += Input.GetAxis("Mouse X")*sensiMouse;
        pitch += Input.GetAxis("Mouse Y")*sensiMouse;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.eulerAngles = new Vector3(-pitch, yawn, 0f);
    }
    

}
