using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Gravity : MonoBehaviour
{
    public GameObject player;
    private GameObject[] boxes;

    void Start()
    {
      boxes = GameObject.FindGameObjectsWithTag("Box");
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Q)) {
          if (!player.GetComponent<movement>().getOnGround()) {
            return;
          }
          player.GetComponent<Rigidbody2D>().gravityScale *= -1;
          //player.GetComponent<Transform>().rotation = new Vector3(180, 0, 0);
          foreach (GameObject box in boxes)
          {
              box.GetComponent<Rigidbody2D>().gravityScale *= -1;
          }
      }
    }
}
