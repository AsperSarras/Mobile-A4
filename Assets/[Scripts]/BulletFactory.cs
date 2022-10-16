using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    public GameObject bulletPrefab;

    //Sprite textute
    public Sprite _playerBulletSprite;
    public Sprite _enemyBulletSprite;

    //Bullet Parent
    public Transform bulletParent;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _playerBulletSprite = Resources.Load<Sprite>("Sprite/Bullet");
        _enemyBulletSprite = Resources.Load<Sprite>("Sprite/EnemySmallBullet");

        bulletPrefab = Resources.Load<GameObject>("Prefabs/PlayerBullet");

        bulletParent = GameObject.Find("Bullets").transform;
    }

    public GameObject CreateBullet(BulletType type = BulletType.PLAYER)
    {
        var bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent); //no prefab
        switch (type)
        {
            case BulletType.PLAYER:
                bullet.GetComponent<SpriteRenderer>().sprite = _playerBulletSprite;
                bullet.GetComponent<B_Behavor>().SetDirection(B_Direction.UP);
                bullet.GetComponent<B_Behavor>().bulletType = BulletType.PLAYER;
                break;
            case BulletType.ENEMY:
                bullet.GetComponent<SpriteRenderer>().sprite = _enemyBulletSprite;
                bullet.GetComponent<B_Behavor>().SetDirection(B_Direction.DOWN);
                bullet.GetComponent<B_Behavor>().bulletType = BulletType.ENEMY;
                break;
        }

        if (bullet != null)
        {
            bullet.SetActive(false);
        }

        return bullet;
    }
}

/*
 * On BulletManager
 * public Queue<GameObject> PlayerBulletPool
 * public Queue<GameObject> EnemyBulletPool
 * public int playerBulletNumber = 50
 * public int enemeyBulletNumber = 50
 * int playerBulletCount
 * int playerActiveBullet
 * int enemyBulletCount
 * int enemyActiveBullet
 * public BulletFactory factory
 * Start()
 * {
 * PlayerBulletPool = new Queue<GameObject>()
 * EnemyBulletPool = new Queue<GameObject>()
 * factory = GameObject.FindObjectOfType<BulletFactory>();
 * }
 * public GameObject GetBullet(Vector2 position, Bullet type) (A lot of stuff)
 * {
 * 
 * }
 * 
 * 
 * 
 * CreateBullet() (DELETE)
 * {
 * var bullet = factory.CreateBullet(BulletType.PLAYER);
 * bulletPool.Enqueue(factory.CreateBullet(BulletType.PLAYER);
 * }
 * 
 * 
 * On EnenmyBehavior
 * public tansfornm bulletSpawnPoint
 * [Range(0.1f, 1.0f)]
 * public float fireRate = 0.2f
 * public BulletManager = bulletManager
 * 
 * void Star()
 * {
 *   some changes here
 * }
 * 
 * public FireBullet()
 * {
 * bulletManager.GetBullet(bulletSpawnPoint.position, BulletPosition.DOWN;
 * }
 * 
 */
