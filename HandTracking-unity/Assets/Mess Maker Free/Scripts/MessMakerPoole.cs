using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessMakerPoole : MonoBehaviour
{
    public GameObject Player;
    public int DrawDistance;
    public LayerMask MessMakerLayer;
    int Layer;

    public int StartSize;
    public List<GameObject> PooledObjects = new List<GameObject>();

    List<List<GameObject>> Poole = new List<List<GameObject>>();

    public bool RandomiseScale;
    public float MinimumScale;
    public float MaximumScale;

    public bool RandomiseXRotation;
    public bool RandomiseYRotation;
    public bool RandomiseZRotation;

    public GameObject TakeFromPoole(int ObjNumber,Vector3 Pos)
    {
        var relevantPool = Poole[ObjNumber];
        foreach (var gameObject in relevantPool)
        {
            if (gameObject.activeSelf == false)
            {
                gameObject.transform.position = Pos;
                gameObject.SetActive(true);
                return gameObject;
            }
        }

        //didn't find available one
        var newInstance = Instantiate(PooledObjects[ObjNumber],this.transform);
        relevantPool.Add(newInstance);
        newInstance.layer = Layer;
        newInstance.transform.position = Pos;
        newInstance.SetActive(true);
        return newInstance;
    }// end TakeFromPoole


    // Start is called before the first frame update
    void Start()
    {
        Layer = Convert.ToInt32(Mathf.Log(MessMakerLayer.value,2));

        float[] distances = new float[32];

        bool cullingActive = false;
        Camera mainCamera = Camera.main;

        for (int i = 0; i < 32; i++)
            if (mainCamera.layerCullDistances[i]>0) cullingActive = true;

        if (!cullingActive)
        {        
            distances[Layer] = DrawDistance;
            mainCamera.layerCullDistances = distances;
        }
        else 
        {
            distances = mainCamera.layerCullDistances;
            distances[Layer] = DrawDistance;
            mainCamera.layerCullDistances = distances;
        }

        for (int i = 0; i < PooledObjects.Count; i++)
        {
            Poole.Add(new List<GameObject>());
            for (int j = 0; j < StartSize; j++)
            {
                var newInstance = Instantiate(PooledObjects[i],this.transform);
                newInstance.layer = Layer;

                foreach (Transform child in newInstance.transform)
                    child.gameObject.layer = Layer;

                newInstance.SetActive(false);
                Poole[i].Add(newInstance);
            } // end for j
        }// end for i
    } // end Start
} // end Class