using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MissileMovement : MonoBehaviour
{
    public float speed = 30f;
    public float expRadius = 1f;
    public float damage;
    public Vector3 vectorOffset = new Vector3(0f, 90f, 0f);

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void seek(Transform newTarget, float newExpRadius, float newDamage)
    {
        target = newTarget;
        expRadius = newExpRadius;
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
        transform.LookAt(target);
        Vector3 v = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(v.x, v.y+vectorOffset.y, v.z);
    }

    void hitTarget()
    {
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, expRadius);
        foreach (Collider temp in colliders)
        {
            if(temp.tag == "Enemy")
                Damage(temp.transform);
        }
    }

    void Damage(Transform enemy)
    {
        enemy.gameObject.GetComponent<EnemyMovement>().takeDamage(damage); ;
    }
}
