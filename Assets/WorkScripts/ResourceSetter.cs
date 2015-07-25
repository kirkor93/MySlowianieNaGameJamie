using UnityEngine;
using System.Collections;

public class ResourceSetter : MonoBehaviour {

    public Transform Corner1;
    public Transform Corner2;
    public GameObject FoodResource;
    public GameObject WoodResource;
    public GameObject IronResource;
    public GameObject StoneResource;
    public int MaxResources = 20;

    [ContextMenu("Generate resources")]
    void Generate()
    {
        if(Corner1 == null || Corner2 == null)
        {
            Debug.LogError("How can I create null or random between nulls");
            return;
        }
        GameObject resourcePrefab = null;
        for(int i = 0; i < MaxResources; ++i)
        {
            int rnd = Random.Range(1, 5);
            switch (rnd)
            {
                case 1:
                    resourcePrefab = FoodResource;
                    break;
                case 2:
                    resourcePrefab = WoodResource;
                    break;
                case 3:
                    resourcePrefab = IronResource;
                    break;
                case 4:
                    resourcePrefab = StoneResource;
                    break;
            }
            float x = Random.Range(Corner1.position.x, Corner2.position.x);
            float z = Random.Range(Corner1.position.z, Corner2.position.z);
            Vector3 newPos = new Vector3(x, resourcePrefab.transform.position.y, z);
            Quaternion newRot = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
            GameObject go = Instantiate(resourcePrefab, newPos, newRot) as GameObject;
            go.name = resourcePrefab.name + " (" + (i + 1) + ")";
            go.transform.parent = resourcePrefab.transform.parent;
        }
    }
}
