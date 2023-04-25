using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    int state=0;
    GameObject cam;
    Vector3 v;
    float mousev,sensitivity=10.0f;
    void updatestate()
    {
        switch(state)
        {
            case 0:
                if(Input.GetKeyDown(KeyCode.Mouse0)) state=1;
                break;
            case 1:
                if(Input.GetKeyUp(KeyCode.Mouse0))
                {
                    state=2;
                    v=cam.transform.rotation.eulerAngles;
                    v.x=45.0f;
                    v=(Quaternion.Euler(v)*Vector3.forward).normalized*mousev;
                }
                break;
        }
    }
    void action()
    {
        switch(state)
        {
            case 1:
                mousev=Input.GetAxis("Mouse Y")*sensitivity;
                break;
            case 2:
                transform.position=transform.position+v;
                break;
        }
    }
    void Start()
    {
        cam=GameObject.Find("PlayerCamera");
    }
    void Update()
    {
        updatestate();
        action();
    }
}
