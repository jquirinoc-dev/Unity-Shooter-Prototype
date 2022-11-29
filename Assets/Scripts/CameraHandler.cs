using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public Camera firstPersonCamera;
    public Camera overheadCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            changeCameraView();
        }
    }

    void changeCameraView()
    {
        if (firstPersonCamera.enabled == true)
        {
            firstPersonCamera.enabled = false;
            overheadCamera.enabled = true;
        }

        if (overheadCamera.enabled == true)
        {
            overheadCamera.enabled = false;
            firstPersonCamera.enabled = true;
        }
    }
}
