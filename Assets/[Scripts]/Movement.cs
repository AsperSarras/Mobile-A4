using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10.0f;
    public Boundry boundry;
    public float verticalPos;

    public bool usingMobileInput = false;

    public Transform bulletSpawnPoint;
    [Range(0.1f, 1.0f)]
    public float fireRate = 0.2f;


    private Camera camera;
    private B_Manager bulletManager;
    private S_Manager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<S_Manager>();
        bulletManager = FindObjectOfType<B_Manager>();

        transform.position = new Vector2(0.0f, verticalPos);
        camera = Camera.main;

        usingMobileInput = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;

        InvokeRepeating("FireBullets", 0.1f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (usingMobileInput)
        {
            GetMobileInput();
        }
        else
        {
            GetConventionalInput();
        }

        Move();

        if (Input.GetKeyDown(KeyCode.K))
        {
            scoreManager.AddPoints(10);
        }

        //CheckBounds();
    }

    void GetConventionalInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        transform.position += new Vector3(x, 0, 0);
    }

    void GetMobileInput()
    {

        foreach (Touch touch in Input.touches)
        {
            var destination = camera.ScreenToWorldPoint(touch.position);
            transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime * speed);
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //float y = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        //transform.position += new Vector3(x, 0, 0);

        transform.position += new Vector3(x, 0, 0);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, boundry.min, boundry.max), verticalPos);
        //Mathf.Clamp(x,boundry.min,boundry.max)
        //transform.position = new Vector2(transform.position.x + x, 0);
        //transform.position = new Vector2(transform.position.x + x,transform.position.y + y);
    }

    //void CheckBounds()
    //{
    //    if (transform.position.x > boundry.max)
    //    {
    //        transform.position = new Vector2(boundry.max, verticalPos);
    //    }
    //    if (transform.position.x < boundry.min)
    //    {
    //        transform.position = new Vector2(boundry.min, verticalPos);
    //    }
    //}

    void FireBullets()
    {
        bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.PLAYER);
    }
}
