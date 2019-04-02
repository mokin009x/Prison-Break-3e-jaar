using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IventoryUI : MonoBehaviour
{
    public static IventoryUI instance;
    
    public GameObject itemPrefab;
    public Transform parent;
    private Dictionary<string, Pickup> items;
    public GameObject pcScreen;
    public GameObject inventoryScreen;
    public InputField pcInputField;
    public GameObject pickupIcon;
    public GameObject nameChanger;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        
        items = new Dictionary<string, Pickup>();
    }

    void Start()
    {

        nameChanger = gameObject.transform.GetChild(3).gameObject;
        pickupIcon = gameObject.transform.GetChild(2).gameObject;
        
        inventoryScreen = gameObject.transform.GetChild(0).gameObject;
       
        pcScreen = gameObject.transform.GetChild(1).gameObject;
        pcInputField = pcScreen.transform.GetChild(0).gameObject.GetComponent<InputField>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        Web.instance.changeNameInput = nameChanger.transform.GetChild(0).gameObject.GetComponent<InputField>();

        DisableUi();

    }

    public void DisableUi()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    public bool CompareCode(string code)
    {
        if (pcInputField.text == code)
        {
            //open door
            pcInputField.gameObject.SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RegisterPickUpItem(Pickup i)
    {
        if (!items.ContainsKey(i.objectName))
        {
            items.Add(i.objectName, i);
        }
    }

    public void Add(Item i)
    {
       
        if (items.ContainsKey(i.name) && !items[i.name].isInInventory())
        {
            GameObject go = Instantiate(itemPrefab, parent, false);
            go.GetComponent<Image>().sprite = items[i.name].image;
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.name;
            items[i.name].setInventoryObj(go);
        }
    }

    public void Remove(Item i)
    {
        if (items.ContainsKey(i.name))
        {
            items[i.name].Respawn();
        }
    }
}