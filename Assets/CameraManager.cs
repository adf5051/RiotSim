using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public CameraValues[] cameraSpots;
    public Camera main;
    int spotIndex=0;

    void Start()
    {
        main.transform.parent = cameraSpots[spotIndex].transform;
        main.transform.localPosition = Vector3.zero;
        main.transform.localEulerAngles = Vector3.zero;
        main.fieldOfView = cameraSpots[spotIndex].fov;
        main.orthographic = cameraSpots[spotIndex].ortho;
        main.orthographicSize = cameraSpots[spotIndex].size;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            spotIndex++;
            spotIndex = spotIndex % cameraSpots.Length;

            main.transform.parent = cameraSpots[spotIndex].transform;
            main.transform.localPosition = Vector3.zero;
            main.transform.localEulerAngles = Vector3.zero;
            main.fieldOfView = cameraSpots[spotIndex].fov;
            main.orthographic = cameraSpots[spotIndex].ortho;
            main.orthographicSize = cameraSpots[spotIndex].size;
        }
    }

}
