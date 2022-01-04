using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Gravity : MonoBehaviour
{
    public GameObject player;
    private GameObject[] boxes;
    public bool play_jump;

    void Start()
    {
      boxes = GameObject.FindGameObjectsWithTag("Box");
    }

    // Update is called once per frame
    void Update()
    {
        play_jump = false;
      if (Input.GetKeyDown(KeyCode.Q)) {
            if (!player.GetComponent<movement>().getOnGround())
            {
                return;
            }
            else
            {
                player.GetComponent<Rigidbody2D>().gravityScale *= -1;
                //player.GetComponent<Transform>().rotation = new Vector3(180, 0, 0);
                bool flipped;
                play_jump = true;
                if (flipped = player.GetComponent<SpriteRenderer>().flipY)
                {
                    player.GetComponent<SpriteRenderer>().flipY = false;
                } else
                {
                    player.GetComponent<SpriteRenderer>().flipY = true;
                }
                foreach (GameObject objectChild in boxes)
                {
                    objectChild.GetComponent<Rigidbody2D>().gravityScale *= -1;
                }
            }
      }
    }
}
