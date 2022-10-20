using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private Vector3 player;
    public float moveSpeed = 10;

    public Animator anim;

    public LayerMask layerMask = 0;

    public Transform enemyTarget = null;

    public Transform bulletPool;
    private float bulletSpeed = 5.0f;
    public float range = 20f;

    public int playerHp;

    public void Start()
    {
        playerHp = 1000;
        player = transform.position;
        InvokeRepeating("SearchEnemy", 0f, 0.1f);
        InvokeRepeating("AutoAttack", 0f, 1f);
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.x -= moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.x += moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.y += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            player.y -= moveSpeed * Time.deltaTime;
        }

        transform.position = player;
    }

    private void SearchEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, layerMask);
        Transform shortestTarget = null;

        if(colliders.Length > 0)
        {
            float shortestDistance = Mathf.Infinity;
            foreach(Collider2D targetCollider in colliders)
            {
                float targetDistance = Vector3.SqrMagnitude(transform.position - targetCollider.transform.position);
                if(shortestDistance > targetDistance)
                {
                    shortestDistance = targetDistance;
                    shortestTarget = targetCollider.transform;
                }
            }
        }

        enemyTarget = shortestTarget;
    }

    private void AutoAttack()
    {
        if(enemyTarget != null)
        {
            GameObject bullet = bulletPool.GetChild(0).gameObject;
            bullet.gameObject.SetActive(true);
            bullet.transform.position = transform.position;
            bullet.transform.SetAsLastSibling();
            //bullet.GetComponent<Bullet>().SetEnemyPosition(enemyTarget); // DOTween용
            bullet.GetComponent<Bullet>().Init(enemyTarget);
        }
    }

    public void Damaged(int damage)
    {
        playerHp -= damage;
        if(playerHp <= 0)
        {
            // 캐릭터 Die 애니메이션 실행
        }
    }
}
