using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonSelectedInventory : MonoBehaviour
{
    public Button initialButton;

    private void OnEnable()
    {
        initialButton.Select();
        initialButton.OnSelect(null);
    }

    private void OnDisable()
    {
        //if (!gameObject.name.Equals("Select Inventory"))
        //{
        //    GameObject inventoryWindow = GameObject.Find("Select Inventory");
        //    inventoryWindow.GetComponent<UIButtonSelectedInventory>().initialButton.Select();
        //    inventoryWindow.GetComponent<UIButtonSelectedInventory>().initialButton.OnSelect(null);
        //}
    }
}
