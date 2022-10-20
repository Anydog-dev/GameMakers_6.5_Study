using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    IDLE,
    WALK,
    ATTACK,
    DIE
}

public class Enemy : MonoBehaviour
{
    private int hp;
    private int atk;
    private float spd;

    EnemyState enemyState;

    private Transform playerPos;

    private Vector2 tmpDircetion;

    private bool isAtk = false;

    private float distance;

    void Start()
    {
        SetState(EnemyState.WALK);
    }

    public void Init(int hp, int atk, float spd)
    {
        
        this.hp = hp;
        this.atk = atk;
        this.spd = spd;

        playerPos = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, playerPos.position);
        if(distance >= 2 && isAtk == true)
        {
            isAtk = false;
            SetState(EnemyState.WALK);
            StopCoroutine(Attack());
        }
    }

    public void SetState(EnemyState enemyState)
    {

        this.enemyState = enemyState;

        switch(enemyState)
        {
            case EnemyState.WALK:
                StartCoroutine(Walk());
                break;

            case EnemyState.ATTACK:
                StartCoroutine(Attack());
                break;
        }
    }

    public void Idle()
    {

    }

    public IEnumerator Walk()
    {
        while(!isAtk)
        {
            tmpDircetion = (playerPos.position - transform.position).normalized;
            transform.Translate(tmpDircetion * spd * Time.deltaTime);
            yield return null;

            if(distance < 2)
            {
                isAtk = true;
                SetState(EnemyState.ATTACK);
                break;
            }
        }
        yield return null;
    }

    public IEnumerator Attack()
    {
        while(isAtk)
        {
            if (distance >= 2)
            {
                break;
            }

            Debug.Log("Damaged");
            Player.Instance.Damaged(5);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            hp -= 50;

            if(hp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
