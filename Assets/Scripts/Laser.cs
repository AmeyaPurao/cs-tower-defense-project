using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 5f;
    public float dps;

    [Header("Unity Setup")]
    public string enemyTag = "Enemy";
    public Transform rotate;
    public Transform laserStart;
    public LineRenderer lineRenderer;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        range = PlayerStats.instance.laserRange;
        dps = PlayerStats.instance.laserDPS;
        InvokeRepeating("findEnemy", 0f, 0.5f);
    }

    void findEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject closest = null;
        float minDist = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distEnemy < minDist)
            {
                closest = enemy;
                minDist = distEnemy;
            }
        }
        if (closest != null && minDist <= range)
        {
            target = closest;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (lineRenderer.enabled)
                lineRenderer.enabled = false;
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        rotate.rotation = Quaternion.Euler(0f, rotation.y + 90f, 0f);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, laserStart.position);
        lineRenderer.SetPosition(1, target.transform.position);
        fire();
    }

    void fire()
    {
        target.gameObject.GetComponent<EnemyMovement>().takeDamage(dps * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
