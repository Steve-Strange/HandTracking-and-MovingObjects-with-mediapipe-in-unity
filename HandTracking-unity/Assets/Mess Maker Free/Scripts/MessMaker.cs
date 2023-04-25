using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessMaker : MonoBehaviour
{
    public MessMakerPoole messMakerPoole;
    public int Number;
    public int Range;

    List<GameObject> Objects = new List<GameObject>();
    bool active = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == messMakerPoole.Player && !active)
        {
            active = true;
            Objects = new List<GameObject>();

            for (int i = 0; i < Number; i++)
            {
                int which = UnityEngine.Random.Range(0, messMakerPoole.PooledObjects.Count);
                
                float x = transform.position.x + UnityEngine.Random.Range(-Range, Range + 1);
                float z = transform.position.z + UnityEngine.Random.Range(-Range, Range + 1);
                Objects.Add(messMakerPoole.TakeFromPoole(which, new Vector3(x, transform.position.y, z)));

                Quaternion rotatation = new Quaternion();

                if (messMakerPoole.RandomiseXRotation)
                    rotatation.x = UnityEngine.Random.Range(0, 360);
                if (messMakerPoole.RandomiseYRotation)
                    rotatation.y = UnityEngine.Random.Range(0, 360);
                if (messMakerPoole.RandomiseZRotation)
                    rotatation.z = UnityEngine.Random.Range(0, 360);

                Objects[i].transform.rotation = rotatation;

                if (messMakerPoole.RandomiseScale)
                {
                    float scale = UnityEngine.Random.Range(messMakerPoole.MinimumScale, messMakerPoole.MaximumScale);
                    Objects[i].transform.localScale = new Vector3(scale, scale, scale);
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == messMakerPoole.Player && active)
        {
            active = false;

            foreach (GameObject thing in Objects)
            {
                thing.transform.position = Vector3.zero;
                thing.SetActive(false);
            }

        }
    }

}
