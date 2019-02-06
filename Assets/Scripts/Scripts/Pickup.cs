using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour, IInteractible
{
    public string objectName;
    public float Weight;
    public Sprite image;
    private GameObject inventoryObj;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action()
    {
        if (Inventory.instance.AddItem(CreateItem()))
        {
            gameObject.SetActive(false);
        }
    }

//----------------------------------------    
    
    public void SetinventoryObj(GameObject go)
    {
        inventoryObj = go;
    }

    public void removeInventoryObj()
    {
        Destroy(inventoryObj);
        inventoryObj = null;
    }
//----------------------------------------
    protected abstract Item CreateItem();

}
