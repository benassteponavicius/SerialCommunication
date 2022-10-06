using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 direction = new Vector3();
    private float x, y;
    void Update()
    {
       // x = Input.GetAxis("Horizontal");
        //y = Input.GetAxis("Vertical");

        direction.x = x;
        direction.z = y;
        
        Move(direction);
    }

    void Move(Vector3 dir)
    {
        transform.position += dir * Time.deltaTime;
    }

    public void CallMove(float xAxis, float yAxis)
    {
        x = xAxis;
        y = yAxis;
    }
}
