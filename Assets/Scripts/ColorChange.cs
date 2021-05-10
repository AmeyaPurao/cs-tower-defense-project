using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ColorChange : MonoBehaviour
{
    public Color hoverColor;
    public Color noPlace;
    private Renderer rend;
    private Color startColor;
    public Color selectColor;
    private bool selected;
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        if (selected)
            return;
        // Debug.Log(Shop.instance.enoughMoney());
        if (Shop.instance.selected())
        {
            if (gameObject.GetComponent<PlaceTurret>().isTurret() || !Shop.instance.enoughMoney())
            {
                rend.material.color = noPlace;
                return;
            }
            rend.material.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (selected)
            return;
        rend.material.color = startColor;
    }

    public void place()
    {
        rend.material.color = noPlace;
    }

    public void selectedColor()
    {
        selected = true;
        rend.material.color = selectColor;
    }

    public void resetColor()
    {
        selected = false;
        rend.material.color = startColor;
    }
}
