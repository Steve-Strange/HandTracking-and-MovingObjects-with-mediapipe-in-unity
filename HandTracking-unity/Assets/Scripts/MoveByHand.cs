using UnityEngine;

public class MoveByHand : MonoBehaviour
{
    private bool activated = false;
    GameObject a;
    GameObject b;
    public GameObject c;
    Rigidbody rig_a;
    Rigidbody rig_b;

    static int a_t = 0;
    static int b_t = 0;

    private void Start()
    {
        rig_a = a.GetComponent<Rigidbody>();
        rig_b = b.GetComponent<Rigidbody>();
    }

    void Update()
    {
        a = GameObject.Find("pointL10");
        b = GameObject.Find("pointR10");
        if (activated)
        {
            print("666");
            Vector3 midPoint = (a.transform.position + b.transform.position) / 2f;
            c.transform.position = midPoint;
            rig_a.useGravity = false;
            rig_b.useGravity = false;
        }
        else
        {
            rig_a.useGravity = true;
            rig_b.useGravity = true;
        }
        print(a_t);
        print(b_t);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == a || a_t == 1)
        {
            a_t = 1;
            if (other.gameObject == b || b_t == 1)
            {
                b_t = 1;
                activated = true;
                return;
            }
            return;
        }
        if (other.gameObject == b || b_t == 1)
        {
            b_t = 1;
            if (other.gameObject == a || a_t == 1)
            {
                a_t = 1;
                activated = true;
                return;
            }
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == a)
        {
            a_t = 0;
            activated = false;
        }
        if (other.gameObject == b)
        {
            b_t = 0;
            activated = false;
        }
    }
}