using UnityEngine;
using RAIN.Navigation.Targets;
using System.Collections;

public class MountPoint : MonoBehaviour {

    public NavigationTargetRig TRig;

    [ExecuteInEditMode]
    void Start()
    {
        TRig = this.GetComponent<NavigationTargetRig>();
        TRig.Target.MountPoint = gameObject.transform;
    }
}
