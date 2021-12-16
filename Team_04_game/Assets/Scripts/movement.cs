using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Transform transform;
    public float speed = 0.02f;

    private bool onGround = false;
    private List<GameObject> currentCollisions = new List<GameObject>();

    private Animator anim;

    private Camera camera;
    private GameObject[] boxes;
    private GameObject boxToMove = null;
    public float jumpAmount = 10;
    public int keys;

    private bool pulling;

    private Rigidbody2D rb;

    private Vector3 start;

    public bool getOnGround() {
      return onGround;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        camera = Camera.main;
        boxes = GameObject.FindGameObjectsWithTag("Box");
        rb = GetComponent<Rigidbody2D>();
        keys = 0;
        pulling = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Danger") {
            // reset player
            foreach (GameObject box in boxes) {
                box.GetComponent<pullBehaviour>().resetPosition();
            }
            transform.position = start;
        } else {
            currentCollisions.Add(collision.gameObject);
            onGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.tag == "Respawn") {
          start = new Vector3(collision.transform.position.x,
                      collision.transform.position.y + 0.5f,
                      collision.transform.position.z);
      }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        currentCollisions.Remove(collision.gameObject);
        bool stillTouchingGround = false;
        for (int i = 0; i < currentCollisions.Count; i++) {
            if (currentCollisions[i] == null) {
                currentCollisions.RemoveAt(i);
            }
            if (currentCollisions[i].tag == "Floor") {
                stillTouchingGround = true;
            }
        }
        if (!stillTouchingGround) {
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

        if (Input.GetKeyDown(KeyCode.E) && pulling)
        {
            pulling = false;
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            pulling = true;
        }

        foreach (GameObject box in boxes)
        {
            if (box.GetComponent<pullBehaviour>().contactWithPlayer)
            {
                if (pulling)
                {
                    boxToMove = box;
                }
                else {
                    boxToMove = null;
                }
            }
        }
        if (boxToMove != null && !boxToMove.GetComponent<pullBehaviour>().contactWithPlayer) {
            boxToMove = null;
        }
        if (Input.GetKey(KeyCode.A)) {
            xPos -= speed*Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            xPos += speed*Time.deltaTime;
            // transform.position = new Vector3(xPos + speed, yPos, zPos);
        }
        if (Input.GetKeyDown(KeyCode.W) && onGround)
        {
            rb.AddForce(Vector2.up * rb.gravityScale * jumpAmount, ForceMode2D.Impulse);
        }
        transform.position = new Vector3(xPos, yPos, zPos);
        Vector3 change = new Vector3(transform.position.x - oldXPos, transform.position.y - oldYPos, zPos - oldZPos);

        var rotationVector = transform.rotation.eulerAngles;
        anim.SetBool("Running", false);
        if (change.x != 0) {
            anim.SetBool("Running", onGround);
            if (change.x < 0) {
                rotationVector.y = 0;
            } else if (change.x > 0) {
                rotationVector.y = 180;
            }
        }
        transform.rotation = Quaternion.Euler(rotationVector);

        if (boxToMove != null) {
            boxToMove.transform.position = boxToMove.transform.position + change;
        }
        //camera.transform.position = new Vector3(xPos, camera.transform.position.y, camera.transform.position.z);
        camera.transform.position = new Vector3(xPos, yPos, camera.transform.position.z);
    }
}
