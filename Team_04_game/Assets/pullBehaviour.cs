using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pullBehaviour : MonoBehaviour
{

    public bool contactWithPlayer = false;
    private Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            contactWithPlayer = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            contactWithPlayer = false;
        }
    }

    public void resetPosition()
    {
      transform.position = startingPos;
    }
}
