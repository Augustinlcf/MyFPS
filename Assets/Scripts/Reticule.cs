using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticule : MonoBehaviour
{
    [SerializeField] private RectTransform reticule;
    [SerializeField] private float restingSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float speed;
    private float currentSize;
    
    void Update()
    {
        if (IsMoving())
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed*0.6f);

        }

        reticule.sizeDelta = new Vector2(currentSize, currentSize);


    }
    
    // Appeler à chaque tir
    public void ActiveDynamicCrosshair()
    {
        currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed*8f);
        StartCoroutine(StopDynamicCrosshair());
    }

    IEnumerator StopDynamicCrosshair()
    {
        yield return new WaitForSeconds(0.2f);
        currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed*0.2f);
    }

    bool IsMoving()
    {
        if (
            Input.GetAxis("Horizontal") != 0 ||
            Input.GetAxis("Vertical") != 0
        )
        {
            return true;
        }

        return false;
    }
}
