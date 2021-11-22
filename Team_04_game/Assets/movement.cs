using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Transform transform;
    float speed = 0.02f;
    bool onGround = false;
    float yVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        onGround = true;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        yVelocity = yVelocity - 0.03f;
        if (yVelocity < 0) {
            yVelocity = 0;
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.position = new Vector3(xPos-speed,yPos,zPos); 
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(xPos + speed, yPos, zPos);
        }
        if (Input.GetKeyDown(KeyCode.W) && onGround)
        {
            yVelocity = 0.2f;
        }
        transform.position = new Vector3(transform.position.x, yPos + yVelocity, zPos);
    }
}
