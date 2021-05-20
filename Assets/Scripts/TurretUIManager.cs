using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUIManager : MonoBehaviour
{
    public static TurretUIManager instance;

    [Header("Unity Setup")]
    public Text upgradeDisplay;
    public Text sellDisplay;
    public Button upgradeButton;

    private PlaceTurret selectedNode;
    private TurretStats nodeTurret;
    private PlayerStats playerStats;

    private int sellValue;
    private int upgradeCost;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void selectNode(PlaceTurret newNode)
    {
        if (playerStats == null)
        {
            playerStats = PlayerStats.instance;
        }
        if (newNode == selectedNode)
        {
            deselectNode();
        }
        else
        {
            if (selectedNode != null)
                selectedNode.resetColor();
            selectedNode = newNode;
            nodeSetup();
            gameObject.SetActive(true);
            selectedNode.selectedColor();
        }
    }

    public void deselectNode()
    {
        selectedNode.resetColor();
        selectedNode = null;
        gameObject.SetActive(false);
    }

    public void upgrade()
    {
        if (playerStats.money >= upgradeCost && !nodeTurret.isUpgraded)
        {
            playerStats.money -= upgradeCost;
            nodeTurret.upgradeTurret();
            upgradeButton.GetComponent<CanvasGroup>().alpha = 0.2f;
        }
    }

    public void sell()
    {
        Debug.Log(nodeTurret == null);
        nodeTurret.sellTurret();
        deselectNode();
    }

    public void nodeSetup()
    {
        nodeTurret = selectedNode.getTurretPlaced().GetComponent<TurretStats>();
        sellValue = nodeTurret.getSellValue();
        upgradeCost = nodeTurret.getUpgradeCost();
        upgradeDisplay.text = "$" + upgradeCost.ToString();
        sellDisplay.text = "$" + sellValue.ToString();
        if (playerStats.money < upgradeCost || nodeTurret.isUpgraded)
            upgradeButton.GetComponent<CanvasGroup>().alpha = 0.2f;
        else
            upgradeButton.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void exitApp() {
        Application.Quit();
    }
}
