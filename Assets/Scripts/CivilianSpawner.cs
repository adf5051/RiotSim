using UnityEngine;
using System.Collections;

public class CivilianSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject charPrefab = null;

    [SerializeField]
    [Range(2, 20)]
    private int numCivies = 10;

    private Bounds bounds;

    // Use this for initialization
    void Start () {
        bounds = gameObject.GetComponent<MeshFilter>().mesh.bounds;

        GameObject temp;
        //Civilian script;
        Vector3 pos;
        for (int i = 0; i < numCivies; i++) {
            temp = GameObject.Instantiate(charPrefab);
            //script = temp.AddComponent<Civilian>();
            float x = Random.Range(-bounds.extents.x * transform.localScale.x, bounds.extents.x * transform.localScale.x) + transform.position.x;
            float z = Random.Range(-bounds.extents.z * transform.localScale.z, bounds.extents.z * transform.localScale.z) + transform.position.z;
            pos = new Vector3(x, 2, z);
            temp.transform.position = pos;
            temp.GetComponentInChildren<MeshRenderer>().material.color = Color.green;

            temp.name = "Civilian";
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
