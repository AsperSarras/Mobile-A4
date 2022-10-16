using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class B_Manager : MonoBehaviour
{
    [Range(10, 200)]
    public int playerBulletNumber = 50;
    public int playerBulletCount;
    public int activePlayerBullets = 0;
    [Range(10, 200)]
    public int enemyBulletNumber = 50;
    public int enemyBulletCount;
    public int activeEnemyBullets = 0;

    private Queue<GameObject> PlayerBulletPool;
    private Queue<GameObject> EnemyBulletPool;
    private BulletFactory factory;

    // Start is called before the first frame update
    void Start()
    {
        PlayerBulletPool = new Queue<GameObject>();
        EnemyBulletPool = new Queue<GameObject>(); 
        factory = GameObject.FindObjectOfType<BulletFactory>();
        BuildBulletPools();
    }

    void BuildBulletPools()
    {
        for (int i = 0; i < playerBulletNumber; i++)
        {
            PlayerBulletPool.Enqueue(factory.CreateBullet(BulletType.PLAYER));
        }

        for (int i = 0; i < enemyBulletNumber; i++)
        {
            EnemyBulletPool.Enqueue(factory.CreateBullet(BulletType.ENEMY));
        }
    }


    public GameObject GetBullet(Vector2 position, BulletType type)
    {
        GameObject bullet = null;

        switch (type)
        {
            case BulletType.PLAYER:
                {
                    if (PlayerBulletPool.Count < 1)
                    {
                        PlayerBulletPool.Enqueue(factory.CreateBullet(BulletType.PLAYER));
                    }
                    bullet = PlayerBulletPool.Dequeue();
                    activePlayerBullets++;
                    playerBulletCount = PlayerBulletPool.Count;
                }

                break;
            case BulletType.ENEMY:
                {
                    if (EnemyBulletPool.Count < 1)
                    {
                        EnemyBulletPool.Enqueue(factory.CreateBullet(BulletType.ENEMY));
                    }
                    bullet = EnemyBulletPool.Dequeue();
                    activeEnemyBullets++;
                    enemyBulletCount = EnemyBulletPool.Count;
                }

                break;
        }

        bullet.SetActive(true);
        bullet.transform.position = position;

        return bullet;
    }

    public void ReturnBullet(GameObject bullet, BulletType type)
    {
        bullet.SetActive(false);

        switch (type)
        {
            case BulletType.PLAYER:

                PlayerBulletPool.Enqueue(bullet);
                activePlayerBullets--;
                playerBulletCount = PlayerBulletPool.Count;

                break;
            case BulletType.ENEMY:
                EnemyBulletPool.Enqueue(bullet);
                activeEnemyBullets--;
                enemyBulletCount = EnemyBulletPool.Count;
                break;
        }
    }

}