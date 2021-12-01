using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Transform transform;
    public float speed = 0.02f;
    private bool onGround = false;
    private Camera camera;
    private GameObject[] boxes;
    private GameObject boxToMove = null;
    public float jumpAmount = 10;

    private Rigidbody2D rb;

    public bool getOnGround() {
      return onGround;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        camera = Camera.main;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Danger") {
            transform.position = new Vector3(-0.83f, 0.26f, 0.0f);
        } else if (collision.collider.tag == "Floor") {
            onGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor") {
            onGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        float oldXPos = xPos;
        float oldYPos = yPos;
        float oldZPos = zPos;

        foreach (GameObject box in boxes)
        {
            if (box.GetComponent<pullBehaviour>().contactWithPlayer)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    boxToMove = box;
                }
                else {
                    boxToMove = null;
                }
            }
        }
        // if (onGround) {yVelocity = 0;}
        if (Input.GetKey(KeyCode.A)) {
            xPos -= speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xPos += speed;
            // transform.position = new Vector3(xPos + speed, yPos, zPos);
        }
        if (Input.GetKeyDown(KeyCode.W) && onGround)
        {
            rb.AddForce(Vector2.up * rb.gravityScale * jumpAmount, ForceMode2D.Impulse);
        }
        transform.position = new Vector3(xPos, yPos, zPos);
        Vector3 change = new Vector3(transform.position.x - oldXPos, transform.position.y - oldYPos, zPos - oldZPos);
        if (boxToMove != null) {
            boxToMove.transform.position = boxToMove.transform.position + change;
        }
        camera.transform.position = new Vector3(xPos, camera.transform.position.y, camera.transform.position.z);
    }
}
