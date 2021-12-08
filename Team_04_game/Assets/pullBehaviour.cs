using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pullBehaviour : MonoBehaviour
{

    public bool contactWithPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
