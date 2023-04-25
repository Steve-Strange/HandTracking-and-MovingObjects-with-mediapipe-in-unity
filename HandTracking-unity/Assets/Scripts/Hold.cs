using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    public GameObject prompt;

    public void Show(GameObject prompt)
    {
        //用来将某个物体激活或是禁用（这里是prompt，也就是那个图标
        //禁用时这个物体和其子物体都会禁用，包括上面的脚本，在这里很方便
        prompt.SetActive(true);
    }
    public void Hide(GameObject prompt)
    {
        prompt.SetActive(false);
    }
    void OnTriggerEnter(Collider other)     //接触时触发，无需调用
    {
        Debug.Log(Time.time + ":进入该触发器的对象是：" + other.gameObject.name);
        Show(prompt);
        prompt.GetComponent<MoveObject>().enabled = true;
    }
    void OnTriggerStay(Collider other)    //每帧调用一次OnTriggerStay()函数
    {
        Debug.Log(Time.time + "留在触发器的对象是：" + other.gameObject.name);
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log(Time.time + "离开触发器的对象是：" + other.gameObject.name);
        Hide(prompt);
        prompt.GetComponent<MoveObject>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Hide(prompt);
    }

    // Update is called once per frame
    void Update()
    {

    }
}