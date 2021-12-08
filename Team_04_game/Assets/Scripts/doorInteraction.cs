using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorInteraction : MonoBehaviour
{
  public float speed = 0.001f;
  public float range = 1.5f;
  private float origYPos;
  private bool activated;

  void Start() {
    origYPos = transform.position.y;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
      if (collision.collider.tag == "Player") {
            if (collision.gameObject.GetComponent<movement>().keys > 0)
            {
                activated = true;
                collision.gameObject.GetComponent<movement>().keys--;
            }
      }
  }

  void Update() {
    if (activated && transform.position.y < origYPos + range) {
      float xPos = transform.position.x;
      float yPos = transform.position.y;
      float zPos = transform.position.z;

      transform.position = new Vector3(xPos, yPos + speed, zPos);
    }
  }
}
