using UnityEngine;
using System.Collections;

public class BarrierStorage : MonoBehaviour {

    public GameObject obstaclePrefab;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Police" && !other.isTrigger)
        {
            if (other.GetComponent<Police>().Barrier == null)
            {
                GameObject go = Instantiate<GameObject>(obstaclePrefab);
                //go.GetComponent<BoxCollider>().enabled = false;
                //go.transform.parent = other.transform;
                //go.transform.localPosition = Vector3.zero;
                go.SetActive(false);
                other.GetComponent<Police>().Barrier = go;
            }
        }
    }
}
