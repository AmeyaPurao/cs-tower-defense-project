using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Attributes")]
    public float range;
    public float fireRate;
    public float damage;
    public float blastRadius;

    [Header("Unity Setup")]
    public string enemyTag = "Enemy";
    public Transform rotate;
    public Transform firePoint;
    public GameObject missilePrefab;

    private GameObject target;
    private float fireCountdown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        range = PlayerStats.instance.missileRange;
        fireRate = PlayerStats.instance.missileFireRate;
        damage = PlayerStats.instance.missileDamage;
        blastRadius = PlayerStats.instance.missileBlastRadius;
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
            return;

        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        rotate.rotation = Quaternion.Euler(0f, rotation.y + 90f, 0f);

        if (fireCountdown <= 0)
        {
            fire();
            fireCountdown = 1 / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void fire()
    {
        GameObject missile = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
        missile.GetComponent<MissileMovement>().seek(target.transform, blastRadius, damage);
        Debug.Log("Fire");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
