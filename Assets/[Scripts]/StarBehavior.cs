using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBehavior : MonoBehaviour
{
    public float vericalSpeed;
    public Boundry boundry;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    public void Move()
    {
        transform.position -= new Vector3(0, vericalSpeed, 0);
    }

    public void CheckBounds()
    {
        if(transform.position.y < boundry.min)
        {
            ResetStars();
        }
    }

    public void ResetStars()
    {
        transform.position = new Vector2(0, boundry.max);
    }
}
