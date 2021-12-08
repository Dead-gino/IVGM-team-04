using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyInteraction : MonoBehaviour
{
    GameObject toDelete;

    // Start is called before the first frame update
    void Start()
    {
        toDelete = this.gameObject;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (toDelete != null)
        {
            if (collision.collider.tag == "Player")
            {
                collision.gameObject.GetComponent<movement>().keys++;
                Destroy(toDelete);
            }
        }
    }
}
