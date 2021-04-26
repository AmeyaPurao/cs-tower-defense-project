using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStats : MonoBehaviour
{
    public int turretType;
    public bool isUpgraded;

    public Laser laser;
    public Fire missile;
    public Shoot standard;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = PlayerStats.instance;
    }

    public void upgradeTurret()
    {
        if (isUpgraded)
            return;
        if (turretType == 0)
        {
            standard.range += playerStats.standardRangeIncrease;
            standard.damage += playerStats.standardDamageIncrease;
            standard.fireRate += playerStats.standardFireRateIncrease;
        }
        else if (turretType == 1)
        {
            missile.range += playerStats.missileRangeIncrease;
            missile.damage += playerStats.missileDamageIncrease;
            missile.fireRate += playerStats.missileFireRateIncrease;
            missile.blastRadius += playerStats.missileBlastRadiusIncrease;
        }
        else
        {
            laser.range += playerStats.laserRangeIncrease;
            laser.dps += playerStats.laserDPSIncrease;
        }
        isUpgraded = true;
    }

    public int getUpgradeCost()
    {
        switch (turretType)
        {
            case 0:
                return playerStats.standardUpgradeCost;
                break;
            case 1:
                return playerStats.missileUpgradeCost;
                break;
            case 2:
                return playerStats.laserUpgradeCost;
                break;
        }
        return 0;
    }

    public int getSellValue()
    {
        if (turretType == 0)
        {
            if (isUpgraded)
                return (int) Mathf.Round(playerStats.sellMultiplier * (playerStats.standardTurretCost + playerStats.standardUpgradeCost));
            return (int) Mathf.Round(playerStats.sellMultiplier * (playerStats.standardTurretCost));
        }
        else if (turretType == 1)
        {
            if (isUpgraded)
                return (int) Mathf.Round(playerStats.sellMultiplier * (playerStats.missileTurretCost + playerStats.missileUpgradeCost));
            return (int) Mathf.Round(playerStats.sellMultiplier * (playerStats.missileTurretCost));
        }
        else
        {
            if (isUpgraded)
                return (int) Mathf.Round(playerStats.sellMultiplier * (playerStats.laserTurretCost + playerStats.laserUpgradeCost));
            return (int) Mathf.Round(playerStats.sellMultiplier * (playerStats.laserTurretCost));
        }
    }

    public void sellTurret()
    {
        playerStats.money += getSellValue();
        Destroy(gameObject);
    }
}
