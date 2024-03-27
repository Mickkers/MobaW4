using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBillboard : MonoBehaviour
{
    private void Start()
    {
        Camera.onPreRender += OnPreRenderCam;
    }

    private void OnPreRenderCam(Camera cam)
    {
        if(this == null)
        {
            return;
        }
        BillboardCanvas(Camera.current.transform);
    }

    private void BillboardCanvas(Transform cam)
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
    }
}
