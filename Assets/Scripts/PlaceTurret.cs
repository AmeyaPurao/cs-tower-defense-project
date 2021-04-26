using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTurret : MonoBehaviour
{
    public ColorChange colorChange;
    // public Vector3 turretOffset;
    private GameObject currentTurret;
    private bool turret;
    private bool active = false;

    private void OnMouseDown()
    {
        if (Shop.instance.selected())
        {
            if (turret)
            {
                openTurretUI();
                // Debug.Log("Can't place turret here");
            }

            else if (!TurretUIManager.instance.gameObject.activeSelf)
            {
                currentTurret = Shop.instance.place(transform);
                if (currentTurret != null)
                {
                    turret = true;
                    colorChange.place();
                }
            }
            else
            {
                TurretUIManager.instance.deselectNode();
            }
        }
        else
        {
            if (turret)
                openTurretUI();
        }
    }

    public void openTurretUI()
    {
        TurretUIManager.instance.selectNode(this);
    }

    public bool isTurret()
    {
        return turret;
    }

    public GameObject getTurretPlaced()
    {
        return currentTurret;
    }

    public void selectedColor()
    {
        colorChange.selectedColor();
    }

    public void resetColor()
    {
        colorChange.resetColor();
    }
}
