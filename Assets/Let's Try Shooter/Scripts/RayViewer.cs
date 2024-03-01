using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{
    // Start is called before the first frame update
    public float weaponRange = 50f;
    private Camera fpsCamera;
    void Start() {
        fpsCamera = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 lineOrigin = fpsCamera.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0f));
        Debug.DrawLine(lineOrigin, fpsCamera.transform.forward * weaponRange, Color.green);
    }
}
