using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public float MouseSensitivity = 300f;
    public Transform playerBody;
    float xRotation=0f;
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX=Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY=Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        xRotation-=MouseY;
        xRotation=Mathf.Clamp(xRotation,-90f,90f);
        transform.localRotation=Quaternion.Euler(xRotation,0f,0f);
        playerBody.Rotate(Vector3.up*MouseX);
    }
}
