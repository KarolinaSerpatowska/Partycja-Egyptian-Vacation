using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    public Camera camera;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + camera.transform.forward);
    }

}
