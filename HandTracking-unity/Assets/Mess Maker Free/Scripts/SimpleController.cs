using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    Camera MainCamera;
    public float Speed;
    Rigidbody RB;

    private void Start()
    {
        MainCamera = Camera.main;
        RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float speed = Input.GetAxis("Vertical") * Speed;
        
        if (Input.GetAxis("Vertical") < 0) speed /= 2;

        if (Input.GetKey(KeyCode.LeftShift)) speed *= 2;

        RB.velocity = transform.forward * speed;

        float direction = Input.GetAxis("Horizontal") / 2;

        RB.velocity += transform.right * direction * Speed/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0)
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 150 * Time.deltaTime);

        if (Input.GetAxis("Mouse Y") != 0)
            if (MainCamera.transform.localRotation.x >= -0.35f
            && MainCamera.transform.localRotation.x <= 0.35f)
            {
                Quaternion camRote = MainCamera.transform.localRotation;
                camRote.x += Input.GetAxis("Mouse Y") * -3 * Time.deltaTime;

                if (camRote.x < -0.35f)
                    camRote.x = -0.35f;
                else if (camRote.x > 0.35f)
                    camRote.x = 0.35f;

                Debug.Log("CamRote = " + camRote);

                MainCamera.transform.localRotation = camRote;
                Debug.Log("MainCamera.transform.localRotation = " + MainCamera.transform.localRotation);
            }

    }
}
