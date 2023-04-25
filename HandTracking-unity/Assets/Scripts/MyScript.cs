using UnityEngine;
using System.Collections;
public class MyScript:MonoBehaviour
{

    void Start()
    {
        GameObject flag = GameObject.Find("trash_can");
        this.transform.LookAt(flag.transform);
    }
    void Update() {
        //Debug.Log(Time.time + "asdf" + Time.deltaTime + "asdf");
        //Vector3 pos = this.transform.position;

        //speed = (float)0.5 + accelerateRate * Time.time;
        //float Xdelta = speed * Time.deltaTime;
        //this.transform.Translate(Xdelta, 0, 0);


    }
}   