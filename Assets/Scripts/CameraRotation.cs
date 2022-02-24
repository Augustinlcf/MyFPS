using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Camera cameraRotation;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    
    void Update()
    {
        cameraRotation.transform.LookAt(target);
        cameraRotation.transform.Translate(Vector3.left * Time.deltaTime*speed );
    }

    public void DestroyCamera()
    {
        cameraRotation.enabled = false;
    }
}
