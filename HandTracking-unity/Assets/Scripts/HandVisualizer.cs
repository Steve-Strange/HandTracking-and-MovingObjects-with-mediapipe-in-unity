using System.Collections.Generic;
using UnityEngine;

public class HandVisualizer : MonoBehaviour
{

    public Transform leftHandParent;
    public Transform rightHandParent;
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;

    private Dictionary<string, Transform> handParents;

    private void Awake()
    {
        handParents = new Dictionary<string, Transform>();
        handParents.Add("Left", leftHandParent);
        handParents.Add("Right", rightHandParent);
    }

    public void VisualizeHands(string[] handData)
    {
        for (int i = 0; i < handData.Length; i += 5)
        {
            string hand = handData[i];
            if (handParents.ContainsKey(hand))
            {
                int x = int.Parse(handData[i + 1]);
                int y = int.Parse(handData[i + 2]);
                int z = int.Parse(handData[i + 3]);
                float depth = float.Parse(handData[i + 4]);

                Vector3 handPosition = new Vector3(x, y, z);

                // Instantiate sphere for hand joint
                GameObject sphere = Instantiate(spherePrefab, handPosition, Quaternion.identity, handParents[hand]);
                sphere.transform.localScale = Vector3.one * depth * 0.01f;

                // Instantiate cylinder for bone
                if (i > 0)
                {
                    Vector3 prevHandPosition = new Vector3(int.Parse(handData[i - 3]), int.Parse(handData[i - 2]), int.Parse(handData[i - 1]));
                    float distance = Vector3.Distance(handPosition, prevHandPosition);

                    GameObject cylinder = Instantiate(cylinderPrefab, (handPosition + prevHandPosition) / 2f, Quaternion.identity, handParents[hand]);
                    cylinder.transform.up = handPosition - prevHandPosition;
                    cylinder.transform.localScale = new Vector3(cylinder.transform.localScale.x, distance / 2f, cylinder.transform.localScale.z);
                }
            }
        }
    }
}
