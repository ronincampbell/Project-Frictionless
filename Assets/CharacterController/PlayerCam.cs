using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// Code base Dave / Game Development on YouTube

public class PlayerCam : MonoBehaviour
{

    public Transform orientation;
    public Transform camHolder;
    float sense;
    public float sensX;
    public float sensY;
    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float sense = PlayerPrefs.GetFloat("SenseValue");
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX * sense;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY * sense;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-90f, 90f); // Cam cant look up or down more than 90 degrees 

        // rotate cam and orientation 
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }

}
