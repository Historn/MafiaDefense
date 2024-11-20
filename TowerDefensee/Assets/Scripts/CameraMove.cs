using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private bool moverlo = true;

    public float speed = 50;
    public float borderThickness = 10f;

    public float scrollSpeed = 5f;
    public float zoomSpeed = 100f;
    public float minY = 10f;
    public float maxY = 80;

    void Update()
    {
        if(GameManager.GameIsOver)
        {
            this.enabled = false;
                return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
            moverlo = !moverlo;

        if (!moverlo)
            return;

      if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - borderThickness)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= borderThickness)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - borderThickness)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= borderThickness)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * zoomSpeed * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;



    }
}