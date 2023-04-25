using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    public GameObject prompt;

    public void Show(GameObject prompt)
    {
        //������ĳ�����弤����ǽ��ã�������prompt��Ҳ�����Ǹ�ͼ��
        //����ʱ���������������嶼����ã���������Ľű���������ܷ���
        prompt.SetActive(true);
    }
    public void Hide(GameObject prompt)
    {
        prompt.SetActive(false);
    }
    void OnTriggerEnter(Collider other)     //�Ӵ�ʱ�������������
    {
        Debug.Log(Time.time + ":����ô������Ķ����ǣ�" + other.gameObject.name);
        Show(prompt);
        prompt.GetComponent<MoveObject>().enabled = true;
    }
    void OnTriggerStay(Collider other)    //ÿ֡����һ��OnTriggerStay()����
    {
        Debug.Log(Time.time + "���ڴ������Ķ����ǣ�" + other.gameObject.name);
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log(Time.time + "�뿪�������Ķ����ǣ�" + other.gameObject.name);
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