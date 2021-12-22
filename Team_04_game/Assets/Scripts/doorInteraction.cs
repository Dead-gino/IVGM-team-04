using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorInteraction : MonoBehaviour
{
  public float speed = 0.001f;
  public float range = 1.5f;
  private float origYPos;
  private bool activated;
    public bool needKey = false;

  void Start() {
    origYPos = transform.position.y;
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
      if (collision.tag == "Player") {
            if (needKey)
            {
                if (collision.gameObject.GetComponent<movement>().keys > 0)
                {
                    activated = true;
                    collision.gameObject.GetComponent<movement>().keys--;
                }
            } else
            {
                activated = true;
            }
      }
  }

  void Update() {
    if (activated && transform.position.y < origYPos + range) {
      float xPos = transform.position.x;
      float yPos = transform.position.y;
      float zPos = transform.position.z;

      transform.position = new Vector3(xPos, yPos + speed*Time.deltaTime, zPos);
    }
  }
}
