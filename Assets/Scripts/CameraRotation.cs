using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Camera cameraRotation;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraRotation.transform.LookAt(target);
        cameraRotation.transform.Translate(Vector3.left * Time.deltaTime*150f );
    }

    public void DestroyCamera()
    {
        cameraRotation.enabled = false;
    }
}
