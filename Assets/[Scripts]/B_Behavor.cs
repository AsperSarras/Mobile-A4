using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScreenBounds
{
    public Boundry horizontal;
    public Boundry vertical;
}

public class B_Behavor : MonoBehaviour
{
    public B_Direction bulletDirection;
    public float speed;
    public ScreenBounds bounds;
    private Vector3 velocity;
    public B_Manager bulletManager;
    public BulletType bulletType;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<B_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    void Move()
    {

        transform.position += velocity * Time.deltaTime;
    }

    void CheckBounds()
    {
        if ((transform.position.x > bounds.horizontal.max) ||
            (transform.position.x < bounds.horizontal.min) ||
            (transform.position.y > bounds.vertical.max) ||
            (transform.position.y < bounds.vertical.min))
        {
            // return the bullet to the pool
            bulletManager.ReturnBullet(this.gameObject, bulletType);
        }
    }

    public void SetDirection(B_Direction direction)
    {
        switch (direction)
        {
            case B_Direction.UP:
                velocity = Vector3.up * speed;
                Debug.Log("Is Moving + V" + velocity);
                break;
            case B_Direction.RIGHT:
                velocity = Vector3.right * speed;
                break;
            case B_Direction.DOWN:
                velocity = Vector3.down * speed;
                break;
            case B_Direction.LEFT:
                velocity = Vector3.left * speed;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((bulletType == BulletType.PLAYER) ||
            (bulletType == BulletType.ENEMY && other.gameObject.CompareTag("Player")))
        {
            bulletManager.ReturnBullet(this.gameObject, bulletType);
        }

    }
}