using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 70f;
    public GameObject particles;
    public float damage;
    private Transform target;

    public void seek(Transform newTarget, float newDamage)
    {
        target = newTarget;
        damage = newDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            hitTarget();
            return;
        }
            
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void hitTarget()
    {
        GameObject temp = Instantiate(particles, target.position, transform.rotation);
        Destroy(temp, 2f);
        target.gameObject.GetComponent<EnemyMovement>().takeDamage(damage);
        Destroy(gameObject);
    }

    public void setDamage(float newDam)
    {
        damage = newDam;
    }
}
