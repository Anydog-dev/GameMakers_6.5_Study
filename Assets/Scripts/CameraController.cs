using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed = 5.0f;
    private Vector3 distance;

    private float width;
    private float height;

    public GameObject map;
    private Vector2 mapSize;

    private float moveX;
    private float moveY;

    void Start()
    {
        distance = this.transform.position - player.transform.position;

        height = Camera.main.orthographicSize;
        width = (height * Screen.width) / Screen.height;

        mapSize = map.GetComponent<SpriteRenderer>().size;
    }

    void Update()
    {
        this.transform.position = player.position + distance;

        moveX = (float)(mapSize.x/2) - width;
        float clampX = Mathf.Clamp(transform.position.x, -moveX, moveX);

        moveY = (float)(mapSize.y/2) - height;
        float ClampY = Mathf.Clamp(transform.position.y, -moveY, moveY);

        this.transform.position = new Vector3(clampX, ClampY, -10);
    }
}
