using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletParent;
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
        //faireRayCast();
        Tirer();
    }

    private void Deplacer()
    {
        
        //changer translation
        Vector3 deltaPosition = (transform.right * Input.GetAxis("Horizontal") +   //diriger avec les fleches directionnelles
                                 transform.forward * Input.GetAxis("Vertical"))
                                *Time.deltaTime*30f;
        deltaPosition.y = 0f;  // pour ne pas monter ou descendre
        transform.position += deltaPosition;
    }

    private void Tourner()
    {
        //changer rotation
        yawn += Input.GetAxis("Mouse X");
        pitch += Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.eulerAngles = new Vector3(-pitch, yawn, 0f);
    }

    private void faireRayCast()
    {
        RaycastHit hit;  //parametre distance de l'obstacle
        bool foundAwall = Physics.Raycast(
            new Ray(
                transform.position, 
                transform.forward),
            out hit,
            Mathf.Infinity,
            LayerMask.GetMask("Default"));
    
        
        Debug.DrawRay( // dessine un rayon vers la direction indiquée
            transform.position,
            transform.forward * 1000f,
            foundAwall ? Color.green : Color.red);

        
    }

    private void Tirer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var bulletBody = Instantiate(bullet,
            transform.position + transform.forward*1.7f,
            transform.rotation,
            bulletParent);
            bulletBody.GetComponent<Rigidbody>().AddForce(transform.forward*3000);
        }
    }
}
