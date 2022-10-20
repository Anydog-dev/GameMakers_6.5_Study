using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] spawnPoint;
    public Transform enemyPool;

    private int currentWave = 1;

    private int[] enemyNum = {5, 7, 1, 10, 1}; // 5레벨은 보

    private int[] enemyHp = { 50, 75, 100, 125, 500 };
    private int[] enemyAtk = { 10, 15, 20, 25, 100 };
    private float[] enemySpd = { 1, 1, 1, 1, 3 };

    public int deadEnemyNum;

    // Start is called before the first frame update
    void Start()
    {
        StartWave();
    }
    
    public void StartWave()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        for(int i=0; i<enemyNum[currentWave-1]; i++) // 오브젝트 풀링의 기초
        {
            GameObject enemy = enemyPool.GetChild(i).gameObject;
            enemy.gameObject.SetActive(true);
            enemy.transform.position = spawnPoint[i].transform.position;
            enemy.GetComponent<Enemy>().Init(enemyHp[currentWave - 1], enemyAtk[currentWave - 1], enemySpd[currentWave - 1]);
            yield return new WaitForSeconds(.5f);
        }
        yield break;
    }

    public void WaveClearCheck() // 어느 클래스에서 이 함수를 호출해야 할 지 고민을 해야함.
    {
        if(deadEnemyNum == enemyNum[currentWave])
        {
            currentWave++;
        }

        Debug.Log("웨이브 " + currentWave + " 을 클리어 했습니다.");
    }
}
