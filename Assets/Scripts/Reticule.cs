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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving())
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);

        }

        reticule.sizeDelta = new Vector2(currentSize, currentSize);


    }

    bool IsMoving()
    {

        if (
            Input.GetAxis("Horizontal") != 0 ||
            Input.GetAxis("Vertical") != 0 ||
            Input.GetMouseButtonDown(0) == true
            )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
