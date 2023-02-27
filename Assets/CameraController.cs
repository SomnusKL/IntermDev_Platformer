using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{public GameObject player;
    public float CamSpeed;
    public Transform background; 
    public float smoothTime = 0.3f; 
    private Vector3 minBounds, maxBounds; // Min and max bounds of the background
    private float vertExtent, horzExtent; // Vertical and horizontal extents of the camera
    
    void Start()
    {
        vertExtent = Camera.main.orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;
    }

    void Update()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, player.transform.position, CamSpeed * Time.deltaTime);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    void LateUpdate()
    {
        minBounds = background.position + new Vector3(-horzExtent, -vertExtent, 0f);
        maxBounds = background.position + new Vector3(horzExtent, vertExtent, 0f);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        transform.position = Vector3.Lerp(transform.position, pos, smoothTime);
    }
}
