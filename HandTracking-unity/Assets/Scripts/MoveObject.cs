using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    //鼠标经过时改变物体颜色
    private Color mouseOverColor = Color.yellow;  //声明变量为蓝色
    private Color originalColor;    //声明变量来存储本来颜色

    //旋转速度
    public float xSpeed = 0.00001f;//左右旋转速度
    public float ySpeed = 0.00005f;//上下旋转速度
    //旋转角度
    private float x = 0.0f;
    private float y = 0.0f;


    void Start()
    {
        originalColor = GetComponent<Renderer>().sharedMaterial.color;//开始时得到物体着色
    }
    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = mouseOverColor;//当鼠标滑过时高亮
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = originalColor;//当鼠标滑出时恢复物体本来颜色
    }
    IEnumerator OnMouseDown()
    {
        Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);//三维物体坐标转屏幕坐标
        //将鼠标屏幕坐标转为三维坐标，再计算物体位置与鼠标之间的距离
        var offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

        while (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButton(1))
            {
                //Input.GetAxis("MouseX")获取鼠标移动的X轴的距离
                x += Input.GetAxis("Mouse X") * xSpeed * 0.1f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.1f;
                //欧拉角转化为四元数
                Quaternion rotation = Quaternion.Euler(y, x, 0);
                transform.rotation = rotation;
            }


            //将鼠标位置二维坐标转为三维坐标
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            //将鼠标转换的三维坐标再转换成世界坐标+物体与鼠标位置的偏移量
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            transform.position = curPosition;
            yield return new WaitForFixedUpdate();//循环执行
        }
    }

    //void Update()
    //{
    //    if (Input.GetMouseButton(1))
    //    {
    //        //Input.GetAxis("MouseX")获取鼠标移动的X轴的距离
    //        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
    //        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
    //        y = ClampAngle(y, yMinLimit, yMaxLimit);
    //        //欧拉角转化为四元数
    //        Quaternion rotation = Quaternion.Euler(y, x, 0);
    //        transform.rotation = rotation;
    //    }
    //}

    ////角度范围值限定
    //static float ClampAngle(float angle, float min, float max)
    //{
    //    if (angle < -360)
    //        angle += 360;
    //    if (angle > 360)
    //        angle -= 360;
    //    return Mathf.Clamp(angle, min, max);
    //}
}