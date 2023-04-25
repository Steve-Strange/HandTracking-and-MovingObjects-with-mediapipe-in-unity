using UnityEngine;

public class DrawLine : MonoBehaviour
{

    private LineRenderer[] line = new LineRenderer[2];
    Vector3[] pos = new Vector3[4];
    void Start()
    {
        //四个球体的位置
        pos[0] = new Vector3(0, 0, 1);
        pos[1] = new Vector3(0, 1, 0);
        pos[2] = new Vector3(1, 0, 0);
        pos[3] = new Vector3(2, 1, 1);

        //***************** 画第一条线段 *******************************
        line[0] = GameObject.Find("Line/Line0").AddComponent<LineRenderer>();
        //line[0].material = new Material(Shader.Find("Particles/Additive"));
        line[0].positionCount = 3;
        line[0].startColor = Color.blue;
        line[0].endColor = Color.red;
        line[0].startWidth = 0.006f;
        line[0].endWidth = 0.006f;

        line[0].SetPosition(0, pos[3]);
        line[0].SetPosition(1, pos[2]);
        line[0].SetPosition(2, pos[1]);

        //*************** 画第二条线段 *********************************
        line[1] = GameObject.Find("Line/Line1").AddComponent<LineRenderer>();
        //line[1].material = new Material(Shader.Find("Particles/Additive"));
        line[1].positionCount = 2;
        line[1].startColor = Color.blue;
        line[1].endColor = Color.red;
        line[1].startWidth = 0.006f;
        line[1].endWidth = 0.006f;

        line[1].SetPosition(0, pos[2]);
        line[1].SetPosition(1, pos[0]);

    }
}