using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 15.0f;
    private float rotatioinSpeed = 10.0f;

    private Vector2 tmpDirection;

    public void Init(Transform enemyPos)
    {
        tmpDirection = (enemyPos.position - transform.position).normalized;
    }

    private void Update()
    {
        transform.Translate(tmpDirection * bulletSpeed * Time.deltaTime, Space.World); // Space.World에 주
        transform.Rotate(0, 0, 45f * Time.deltaTime * rotatioinSpeed);
    }

    //public void SetEnemyPosition(Transform enemyPos) DOTween용도
    //{
    //    transform.DOMove(enemyPos.position, 15f).SetEase(Ease.Linear).SetSpeedBased().OnComplete(() =>
    //    {
    //        this.gameObject.SetActive(false);
    //    });
    //}
}
