using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private List<Item> items;
    public float MaximumWeight=10.0f;
    public float TotalWeight;

    void Awake()
    {
        if (Inventory.instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
        
        items = new List<Item>();
    }


    public bool AddItem(Item item)
    {
        if (TotalWeight + item.weight > MaximumWeight)
        {
            return false;
        }
        else
        {
            items.Add(item);
            TotalWeight += item.weight;
            return true;
        }
    }

    public void RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            TotalWeight -= item.weight;    
        }
        
    }

    public bool HasKey(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] is AccessItem)
            {
                AccessItem it = (AccessItem) items[i];

                if (it.door == id)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void printToConsole()
    {
        foreach (Item i in items)
        {
            Debug.Log(i.name + "--" + i.weight );    
        }
        
        Debug.Log("Total Weight" + TotalWeight);
        
    }
}
