using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    private float yawn = 0f;
    private float pitch = 0f;
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject mp5;
    [SerializeField] private GameObject shotgun;
    private GameObject weaponSelected;
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
        Vector3 deltaPosition = (weaponSelected.transform.right * Input.GetAxis("Horizontal") +   //diriger avec les fleches directionnelles
                                 weaponSelected.transform.forward * Input.GetAxis("Vertical"))
                                *Time.deltaTime*30f;
        deltaPosition.y = 0f;  // pour ne pas monter ou descendre
        weaponSelected.transform.position += deltaPosition;
    }

    private void Tourner()
    {
        //changer rotation
        yawn += Input.GetAxis("Mouse X");
        pitch += Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        weaponSelected.transform.eulerAngles = new Vector3(-pitch, yawn, 0f);
    }
    
    public void Select_Sniper()
    {
        weaponSelected = sniper;
    }
    public void Select_Mp5()
    {
        weaponSelected = mp5;
    }
    public void Select_Shotgun()
    {
        weaponSelected = shotgun;
    }

}
