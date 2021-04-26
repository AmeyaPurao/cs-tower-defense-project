using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [Header("Public Instance")]
    public static PlayerStats instance;

    [Header("Player Stats")]
    public int money;
    public int lives;

    [Header("Unity Stats")]
    public int startingMoney;
    public int startingLives;
    public int standardTurretCost;
    public int missileTurretCost;
    public int laserTurretCost;
    public Vector3 standardOffset;
    public Vector3 missileOffset;
    public Vector3 laserOffset;

    [Header("Weapon Stats")]
    public float sellMultiplier;
    public float standardRange;
    public float standardDamage;
    public float standardFireRate;
    public float missileRange;
    public float missileDamage;
    public float missileFireRate;
    public float missileBlastRadius;
    public float laserRange;
    public float laserDPS;

    [Header("Weapon Upgrades")]
    public int standardUpgradeCost;
    public float standardRangeIncrease;
    public float standardDamageIncrease;
    public float standardFireRateIncrease;
    public int missileUpgradeCost;
    public float missileRangeIncrease;
    public float missileDamageIncrease;
    public float missileFireRateIncrease;
    public float missileBlastRadiusIncrease;
    public int laserUpgradeCost;
    public float laserRangeIncrease;
    public float laserDPSIncrease;

    [Header("Unity Setup")]
    public GameObject standardTurretPrefab;
    public GameObject missileTurretPrefab;
    public GameObject laserTurretPrefab;
    public Text moneyDisplay;

    private int lastMoney;

    void Awake()
    {
        instance = this;
        lives = startingLives;
        money = startingMoney;
        lastMoney = 0;
    }

    public void restart()
    {
        lives = startingLives;
        money = startingMoney;
    }

    public bool afford(int amount)
    {
        return amount <= money;
    }

    /*public int getTurretCost(GameObject newTurret)
    {
        if (newTurret = standardTurretPrefab)
        {
            return standardTurretCost;
        }
        else if (newTurret = missileTurretPrefab)
        {
            return missileTurretCost;
        }
        else if (newTurret = laserTurretPrefab)
        {
            return laserTurretCost;
        }
        return 0;
    }*/

    private void Update()
    {
        if (lastMoney != money)
            updateMoney();
    }

    private void updateMoney()
    {
        lastMoney = money;
        moneyDisplay.text = "$" + money;
    }
}
