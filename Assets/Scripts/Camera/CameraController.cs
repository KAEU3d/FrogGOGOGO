using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform frog;

    public float offsetY;
    public float zoomBase;
    private float ratio;

    private void Awake()
    {
        ratio = (float)Screen.height / (float)Screen.width;
        Camera.main.orthographicSize = zoomBase * ratio * 0.5f;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, frog.transform.position.y + offsetY * ratio, transform.position.z);
    }
}
