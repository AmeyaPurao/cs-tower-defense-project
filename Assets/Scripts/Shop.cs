using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Static Instance")]
    public static Shop instance;

    [Header("Unity Setup")]
    public Text standardPrice;
    public Text missilePrice;
    public Text laserPrice;

    private PlayerStats playerStats;
    private GameObject currentTurret;
    private int currentTurretCost;
    private Vector3 currentOffset;
    private GameObject currentNode;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerStats = PlayerStats.instance;
        standardPrice.text = "$" + playerStats.standardTurretCost;
        missilePrice.text = "$" + playerStats.missileTurretCost;
        laserPrice.text = "$" + playerStats.laserTurretCost;
    }

    public bool selected()
    {
        return currentTurret != null;
    }

    public void setStandardTurret()
    {
        if (currentTurret == playerStats.standardTurretPrefab)
        {
            currentTurret = null;
            return;
        }

        currentTurret = playerStats.standardTurretPrefab;
        currentTurretCost = playerStats.standardTurretCost;
        currentOffset = playerStats.standardOffset;
    }

    public void setMissileTurret()
    {
        if (currentTurret == playerStats.missileTurretPrefab)
        {
            currentTurret = null;
            return;
        }

        currentTurret = playerStats.missileTurretPrefab;
        currentTurretCost = playerStats.missileTurretCost;
        currentOffset = playerStats.missileOffset;
    }

    public void setLaserTurret()
    {
        if (currentTurret == playerStats.laserTurretPrefab)
        {
            currentTurret = null;
            return;
        }

        currentTurret = playerStats.laserTurretPrefab;
        currentTurretCost = playerStats.laserTurretCost;
        currentOffset = playerStats.laserOffset;
    }

    public GameObject place(Transform tran)
    {
        if (enoughMoney())
        {
            playerStats.money -= currentTurretCost;
            GameObject tempTurret = Instantiate(currentTurret, tran.position + currentOffset, tran.rotation);
            return tempTurret;
        }
        return null;
        /* if (currentTurret == standardTurretPrefab)
        {
            if (playerStats.money >= standardTurretCost)
            {
                playerStats.money -= standardTurretCost;
                updateMoney();
                GameObject turretTemp = Instantiate(currentTurret, tran.position + standardOffset, tran.rotation);
                return turretTemp;
            }
            else
            {
                Debug.Log("Not enough money");
                return null;
            }
        }
        else if (currentTurret == missileTurretPrefab)
        {
            if (playerStats.money >= missileTurretCost)
            {
                playerStats.money -= missileTurretCost;
                updateMoney();
                GameObject turretTemp = Instantiate(currentTurret, tran.position + missileOffset, tran.rotation);
                return turretTemp;
            }
            else
            {
                Debug.Log("Not enough money");
                return null;
            }
        }
        else
        {
            if (playerStats.money >= laserTurretCost)
            {
                playerStats.money -= laserTurretCost;
                updateMoney();
                GameObject turretTemp = Instantiate(currentTurret, tran.position + laserOffset, tran.rotation);
                return turretTemp;
            }
            else
            {
                Debug.Log("Not enough money");
                return null;
            }
        }*/
    }

    public bool enoughMoney()
    {
        return playerStats.afford(currentTurretCost);
    }

    public void addMoney(int amount)
    {
        playerStats.money += amount;
    }
}