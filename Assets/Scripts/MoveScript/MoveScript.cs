using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float speed = 1;
   

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
    }
}