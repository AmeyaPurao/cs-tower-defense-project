using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyMovement : MonoBehaviour
{
    public float speed = 1f;
    public float errorRange = 0.2f;
    public int startHealth;
    public int enemyWorth;

    private float health;
    private Transform target;
    private int waypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //health = startHealth;
        target = Waypoints.waypoints[0];
    }

    public void takeDamage(int damage)
    {
        if (health <= damage)
        {
            die();
        }
        else
        {
            health -= damage;
        }
    }

    public void takeDamage(float damage)
    {
        if (health <= damage)
        {
            die();
        }
        else
        {
            health -= damage;
        }
    }

    void die()
    {
        Shop.instance.addMoney(enemyWorth);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);
        if (Vector3.Distance(target.position, transform.position) <= errorRange)
        {
            transform.position = target.position;
            nextWaypoint(); 
        }
    }

    void nextWaypoint()
    {
        waypointIndex++;

        if (waypointIndex >= Waypoints.waypoints.Length)
        {
            Lives.instance.loseLife();
            Destroy(gameObject);
            return;
        }
        else
        {
            target = Waypoints.waypoints[waypointIndex];
        }
    }
    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public void setError(float newError)
    {
        errorRange = newError;
    }
    public float getSpeed()
    {
        return speed;
    }
    public void setHealth(int newHealth)
    {
        health = newHealth;
    }
}
