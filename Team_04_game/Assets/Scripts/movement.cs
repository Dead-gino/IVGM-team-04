using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Transform transf;
    public float speed = 0.02f;

    private bool onGround = false;
    private List<GameObject> currentCollisions = new List<GameObject>();

    private Animator anim;

    private Camera cam;
    private GameObject[] boxes;
    private GameObject boxToMove = null;
    private GameObject interactable = null;
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
        transf = GetComponent<Transform>();
        start = new Vector3(transf.position.x, transf.position.y, transf.position.z);
        cam = Camera.main;
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
            pulling = false;
            transf.position = start;
        } else {
            currentCollisions.Add(collision.gameObject);
            onGround = true;
            anim.SetBool("In_Air", false);
            System.Console.WriteLine("standing");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn") {
            start = new Vector3(collision.transform.position.x,
                        collision.transform.position.y + 0.5f,
                        collision.transform.position.z);
        } else if (collision.tag == "Interactable") {
            interactable = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Interactable") {
            interactable.transform.GetChild(0).gameObject.SetActive(false);
            interactable = null;
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
            anim.SetBool("In_Air", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = transf.position.x;
        float yPos = transf.position.y;
        float zPos = transf.position.z;
        float oldXPos = xPos;
        float oldYPos = yPos;
        float oldZPos = zPos;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            pulling = true;
        } else
        {
            pulling = false;
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

        if (Input.GetKeyDown(KeyCode.E) && interactable != null) {
            interactable.transform.GetChild(0).gameObject.SetActive(!interactable.transform.GetChild(0).gameObject.activeSelf);
            //interactable.transform.GetChild(0).gameObject.SetActive(true);
        }
        transf.position = new Vector3(xPos, yPos, zPos);
        Vector3 change = new Vector3(transf.position.x - oldXPos, transf.position.y - oldYPos, zPos - oldZPos);

        var rotationVector = transf.rotation.eulerAngles;
        anim.SetBool("Running", false);

        if (change.x != 0) {
            anim.SetBool("Running", onGround);
            if (change.x < 0) {
                rotationVector.y = 0;
            } else if (change.x > 0) {
                rotationVector.y = 180;
            }
        }

        if (boxToMove != null) {
            boxToMove.transform.position = boxToMove.transform.position + change;
            if ((boxToMove.transform.position.x < transf.position.x && rotationVector.y == 180) || (boxToMove.transform.position.x > transf.position.x && rotationVector.y == 0)) {
              anim.SetBool("Pull", true);
              anim.SetBool("Push", false);
            } else {
              anim.SetBool("Push", true);
              anim.SetBool("Pull", false);
            }
        } else {
          anim.SetBool("Pull", false);
          anim.SetBool("Push", false);
        }
        
        transf.rotation = Quaternion.Euler(rotationVector);
        //camera.transform.position = new Vector3(xPos, camera.transform.position.y, camera.transform.position.z);
        cam.transform.position = new Vector3(xPos, yPos, cam.transform.position.z);
    }
}
