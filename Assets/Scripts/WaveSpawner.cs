using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public Transform enemyPrefab;
    public Transform startPoint;
    public bool expIncrease = false;
    public int waveMult = 1;
    public float waitTime = 0.2f;

    private int waveNum = 0;

    public void spawnWave()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Debug.Log("Spawn Wave");
            waveNum++;
            waitTime -= 0.01f;
            if (waitTime < 0.01f)
                waitTime = 0.01f;
            StartCoroutine(spawnRunner());
        }
    }

    IEnumerator spawnRunner()
    {
        if (expIncrease)
        {
            for (int i = 0; i < waveNum * waveNum; i++)
            {
                spawnEnemy();
                yield return new WaitForSeconds(waitTime);
            }
        }
        else
        {
            for (int i = 0; i < waveNum * waveMult; i++)
            {
                spawnEnemy();
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

    void spawnEnemy()
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);
        Transform enemy = Instantiate(enemyPrefab, startPoint.position + offset, startPoint.rotation);
        EnemyMovement scr = (EnemyMovement) enemy.gameObject.GetComponent(typeof(EnemyMovement));
        float speed = (0.1f * waveNum) + 1;
        if (speed > 5)
            speed = 5;
        scr.setSpeed(speed);
        float errorRate = waveNum * 0.015f+0.01f;
        if (errorRate > 0.3f)
            errorRate = 0.3f;
        scr.setError(errorRate);
        scr.setHealth(100 + (20 * waveNum));
    }

    public int getWaveNum()
    {
        return waveNum;
    }
}
