using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamControl : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && transform.position.z <= 2)
            transform.Translate(Vector3.left * 2 * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && transform.position.z >= -7.5)
            transform.Translate(Vector3.right * 2 * Time.deltaTime);
    }
}
