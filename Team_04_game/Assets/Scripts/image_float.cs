using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class image_float : MonoBehaviour
{

    public float variance;
    public float speed;
    private float count = 0;
    private bool ascend = true;
    private Transform pos;
    private Vector3 mov;

    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        mov = new Vector3(0, speed * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (count < variance && ascend)
        {
            count += speed * Time.deltaTime;
            pos.Translate(mov);
        } else if (count >= variance && ascend)
        {
            count += speed * Time.deltaTime;
            pos.Translate(mov);
            ascend = false;
        } else if (count > 0 && !ascend)
        {
            count -= speed * Time.deltaTime;
            pos.Translate(-mov);
        } else
        {
            count -= speed * Time.deltaTime;
            pos.Translate(-mov);
            ascend = true;
        }
    }
}
