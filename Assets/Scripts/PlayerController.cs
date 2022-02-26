using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static float playerSpeed = 30;
    public static float sensiMouse = 1;
    private float yawn = 0f;
    private float pitch = 0f;

    private GameObject manager;
    
    // HEALTHBAR
    public static float health;
    public static float maxHealth = 300;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager");
        health = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;  //bloque le curseur sur l'ecran
        playerSpeed = 30;
        sensiMouse = 1;
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
    public void GetDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
        }

        if (health <= 0)
        {
            manager.GetComponent<Restart>().RestartGame();
        }
    }
    
}
