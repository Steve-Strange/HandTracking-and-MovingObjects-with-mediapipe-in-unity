using System.Runtime.InteropServices.ComTypes;
using System;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHands : MonoBehaviour
{
    public UDPReceive udpReceive;
    public FirstPersonController person;

    private LineRenderer[] lineL = new LineRenderer[6];
    private LineRenderer[] lineR = new LineRenderer[6];

    GameObject[] pointL = new GameObject[21];
    GameObject[] pointR = new GameObject[21];
    Vector3[] pointPositionL = new Vector3[21];
    Vector3[] pointPositionR = new Vector3[21];
    Vector3[] pointPositionL_f = new Vector3[21];
    Vector3[] pointPositionR_f = new Vector3[21];

    public void Start()
    {

        PhysicMaterial handMaterial = new PhysicMaterial();
        handMaterial.dynamicFriction = 1000;
        handMaterial.staticFriction = 1000;
        handMaterial.bounciness = 0.7f;

        //Rigidbody pointRigidbody = new Rigidbody();
        //pointRigidbody.mass = 30;

        for (int i = 0; i < 21; i++)
        {
            pointL[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointL[i].SetActive(true);
            pointL[i].name = "pointL"+i;
            pointL[i].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            pointL[i].transform.parent = GameObject.Find("FirstPersonController").transform;
            pointL[i].GetComponent<SphereCollider>().material = handMaterial;
            pointL[i].AddComponent<Rigidbody>();
            pointL[i].GetComponent<Rigidbody>().mass = 1f;
            pointL[i].GetComponent<Rigidbody>().drag = 50f;
            pointL[i].GetComponent<Rigidbody>().useGravity = false;
            pointL[i].GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;


            Renderer renderL = pointL[i].GetComponent<Renderer>();
            renderL.material.color = new Color(0.58f, 0.0f, 0.827f, 0.6f);

            pointR[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointR[i].SetActive(true);
            pointR[i].name = "pointR"+i;
            pointR[i].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            pointR[i].transform.parent = GameObject.Find("FirstPersonController").transform;
            pointR[i].GetComponent<SphereCollider>().material = handMaterial;
            pointR[i].AddComponent<Rigidbody>();
            pointR[i].GetComponent<Rigidbody>().mass = 1f;
            pointL[i].GetComponent<Rigidbody>().drag = 50f;
            pointR[i].GetComponent<Rigidbody>().useGravity = false;
            pointR[i].GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;

            Renderer renderR = pointR[i].GetComponent<Renderer>();
            renderR.material.color = new Color(0.86f, 0.08f, 0.235f, 0.6f);
        }

        for (int j = 0; j < 6; j++)
        {
            lineL[j] = GameObject.Find("Line/LineL" + j).AddComponent<LineRenderer>();
            lineR[j] = GameObject.Find("Line/LineR" + j).AddComponent<LineRenderer>();
            lineL[j].positionCount = 5;
            lineR[j].positionCount = 5;

            lineL[j].material.color = new Color(0.5f, 0.26f, 0f, 0.7f);
            lineR[j].material.color = new Color(0.5f, 0.26f, 0f, 0.7f);

            lineL[j].startWidth = 0.02f;
            lineL[j].endWidth = 0.02f;
            lineR[j].startWidth = 0.02f;
            lineR[j].endWidth = 0.02f;
        }
    }

    float x_L = -999;
    float y_L = -999;
    float z_L = -999;
    float x_R = -999;
    float y_R = -999;
    float z_R = -999;
    public void Update()
    {
        string data = udpReceive.data;
        // Vector3 humanRotation = person.transform.rotation;

        if (data.Length == 0) return;
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        print(data);
        string[] points = data.Split(',');

        print(points.Length);

        for (int i = 0; i < 21; i++)
        {
            if (points.Length == 85)
            {
                if (points[0] == "'Left'")
                {
                    pointL[i].SetActive(true);
                    float x = float.Parse(points[1 + i * 4]) / 500;
                    float y = float.Parse(points[1 + i * 4 + 1]) / 400;
                    float z = float.Parse(points[1 + i * 4 + 2]) / 500;
                    float z_distance = float.Parse(points[1 + i * 4 + 3]) / 20;

                    if (Math.Abs(x_L - x) > 0.001) x_L = x;
                    if (Math.Abs(y_L - y) > 0.001) y_L = y;
                    if (Math.Abs(z_L - (z + z_distance)) > 0.001) z_L = z + z_distance;

                    pointPositionL[i] = new Vector3(1.3f + -x_L, -0.7f + y_L, 5.5f + -z_L);
                    pointL[i].transform.localPosition = pointPositionL[i];

                    pointR[i].SetActive(false);

                }
                else if (points[0] == "'Right'")
                {
                    pointR[i].SetActive(true);
                    float x = float.Parse(points[1 + i * 4]) / 500;
                    float y = float.Parse(points[1 + i * 4 + 1]) / 400;
                    float z = float.Parse(points[1 + i * 4 + 2]) / 500;
                    float z_distance = 0 + float.Parse(points[1 + i * 4 + 3]) / 20;

                    if (Math.Abs(x_R - x) > 0.001) x_R = x;
                    if (Math.Abs(y_R - y) > 0.001) y_R = y;
                    if (Math.Abs(z_R - (z + z_distance)) > 0.001) z_R = z + z_distance;

                    pointPositionR[i] = new Vector3(1.3f + -x_R, -0.7f + y_R, 5.5f + -z_R);
                    pointR[i].transform.localPosition = pointPositionR[i];

                    pointL[i].SetActive(false);

                }
            }
            else if (points.Length == 170)
            {
                pointR[i].SetActive(true);
                pointL[i].SetActive(true);
                float x = float.Parse(points[1 + i * 4]) / 500;
                float y = float.Parse(points[1 + i * 4 + 1]) / 400;
                float z = float.Parse(points[1 + i * 4 + 2]) / 500;
                float z_distance = 0 + float.Parse(points[1 + i * 4 + 3]) / 20;

                if (Math.Abs(x_L - x) > 0.001) x_L = x;
                if (Math.Abs(y_L - y) > 0.001) y_L = y;
                if (Math.Abs(z_L - (z + z_distance)) > 0.001) z_L = z + z_distance;

                pointPositionL[i] = new Vector3(1.3f + -x_L, -0.7f + y_L, 5.5f + -z_L);
                pointL[i].transform.localPosition = pointPositionL[i];

                x = float.Parse(points[85 + 1 + i * 4]) / 500;
                y = float.Parse(points[85 + 1 + i * 4 + 1]) / 400;
                z = float.Parse(points[85 + 1 + i * 4 + 2]) / 500;
                z_distance = 0 + float.Parse(points[85 + 1 + i * 4 + 3]) / 20;

                if (Math.Abs(x_R - x) > 0.001) x_R = x;
                if (Math.Abs(y_R - y) > 0.001) y_R = y;
                if (Math.Abs(z_R - (z + z_distance)) > 0.001) z_R = z + z_distance;

                pointPositionR[i] = new Vector3(1.3f + -x_R, -0.7f + y_R, 5.5f + -z_R);
                pointR[i].transform.localPosition = pointPositionR[i];
            }
            else
            {
                print("none！！！！");
                for (int j = 0; j < 21; j++)
                {
                    pointR[i].SetActive(false);
                    pointL[i].SetActive(false);

                }
            }

            pointPositionL_f[i] = pointL[i].transform.position;
            pointPositionR_f[i] = pointR[i].transform.position;
        }

        if (pointL[0].active == true)
        {
            lineL[0].SetPosition(0, pointPositionL_f[0]);
            lineL[0].SetPosition(1, pointPositionL_f[1]);
            lineL[0].SetPosition(2, pointPositionL_f[2]);
            lineL[0].SetPosition(3, pointPositionL_f[3]);
            lineL[0].SetPosition(4, pointPositionL_f[4]);
            lineL[1].SetPosition(0, pointPositionL_f[0]);
            lineL[1].SetPosition(1, pointPositionL_f[5]);
            lineL[1].SetPosition(2, pointPositionL_f[6]);
            lineL[1].SetPosition(3, pointPositionL_f[7]);
            lineL[1].SetPosition(4, pointPositionL_f[8]);
            lineL[2].SetPosition(0, pointPositionL_f[0]);
            lineL[2].SetPosition(1, pointPositionL_f[17]);
            lineL[2].SetPosition(2, pointPositionL_f[18]);
            lineL[2].SetPosition(3, pointPositionL_f[19]);
            lineL[2].SetPosition(4, pointPositionL_f[20]);
            lineL[3].SetPosition(0, pointPositionL_f[16]);
            lineL[3].SetPosition(1, pointPositionL_f[15]);
            lineL[3].SetPosition(2, pointPositionL_f[14]);
            lineL[3].SetPosition(3, pointPositionL_f[13]);
            lineL[3].SetPosition(4, pointPositionL_f[17]);
            lineL[4].SetPosition(0, pointPositionL_f[5]);
            lineL[4].SetPosition(1, pointPositionL_f[9]);
            lineL[4].SetPosition(2, pointPositionL_f[10]);
            lineL[4].SetPosition(3, pointPositionL_f[11]);
            lineL[4].SetPosition(4, pointPositionL_f[12]);
            lineL[5].SetPosition(0, pointPositionL_f[6]);
            lineL[5].SetPosition(1, pointPositionL_f[5]);
            lineL[5].SetPosition(2, pointPositionL_f[9]);
            lineL[5].SetPosition(3, pointPositionL_f[13]);
            lineL[5].SetPosition(4, pointPositionL_f[17]);
        }
        else
        {
            for(int k = 0; k < 6; k++)
            {
                for(int m= 0; m < 5; m++)
                {
                    lineL[k].SetPosition(m, new Vector3(-1,-1,-1));
                }
            }
        }

        if (pointR[0].active == true)
        {
            lineR[0].SetPosition(0, pointPositionR_f[0]);
            lineR[0].SetPosition(1, pointPositionR_f[1]);
            lineR[0].SetPosition(2, pointPositionR_f[2]);
            lineR[0].SetPosition(3, pointPositionR_f[3]);
            lineR[0].SetPosition(4, pointPositionR_f[4]);
            lineR[1].SetPosition(0, pointPositionR_f[0]);
            lineR[1].SetPosition(1, pointPositionR_f[5]);
            lineR[1].SetPosition(2, pointPositionR_f[6]);
            lineR[1].SetPosition(3, pointPositionR_f[7]);
            lineR[1].SetPosition(4, pointPositionR_f[8]);
            lineR[2].SetPosition(0, pointPositionR_f[0]);
            lineR[2].SetPosition(1, pointPositionR_f[17]);
            lineR[2].SetPosition(2, pointPositionR_f[18]);
            lineR[2].SetPosition(3, pointPositionR_f[19]);
            lineR[2].SetPosition(4, pointPositionR_f[20]);
            lineR[3].SetPosition(0, pointPositionR_f[16]);
            lineR[3].SetPosition(1, pointPositionR_f[15]);
            lineR[3].SetPosition(2, pointPositionR_f[14]);
            lineR[3].SetPosition(3, pointPositionR_f[13]);
            lineR[3].SetPosition(4, pointPositionR_f[17]);
            lineR[4].SetPosition(0, pointPositionR_f[5]);
            lineR[4].SetPosition(1, pointPositionR_f[9]);
            lineR[4].SetPosition(2, pointPositionR_f[10]);
            lineR[4].SetPosition(3, pointPositionR_f[11]);
            lineR[4].SetPosition(4, pointPositionR_f[12]);
            lineR[5].SetPosition(0, pointPositionR_f[6]);
            lineR[5].SetPosition(1, pointPositionR_f[5]);
            lineR[5].SetPosition(2, pointPositionR_f[9]);
            lineR[5].SetPosition(3, pointPositionR_f[13]);
            lineR[5].SetPosition(4, pointPositionR_f[17]);
        }
        else
        {
            for (int k = 0; k < 6; k++)
            {
                for (int m = 0; m < 5; m++)
                {
                    lineR[k].SetPosition(m, new Vector3(-1, -1, -1));
                }
            }
        }
    }

}