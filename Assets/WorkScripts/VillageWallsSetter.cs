using UnityEngine;
using System.Collections;

public class VillageWallsSetter : MonoBehaviour {

    public GameObject Prefab;
    public float AngleRate = 4.0f;

    [ContextMenu("Generate wall")]
	void Generate()
    {
        Vector3 radiusVector = new Vector3(8.0f, 0.0f, 0.0f);
        int startingI = 3;
        radiusVector = Quaternion.AngleAxis(-AngleRate * (startingI - 1), Vector3.up) * radiusVector;
        for (int i = startingI; i * AngleRate <= 80.0f; ++i)
        {
            radiusVector = Quaternion.AngleAxis(-AngleRate, Vector3.up) * radiusVector;
            GameObject go = Instantiate(Prefab, radiusVector, Quaternion.Euler(Prefab.transform.eulerAngles + new Vector3(0.0f, AngleRate * i, 0.0f))) as GameObject;
            go.transform.parent = Prefab.transform.parent;
        }
    }
}
