using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IventoryUI : MonoBehaviour
{
    public static IventoryUI instance;
    public GameObject inventoryScreen;

    public GameObject itemPrefab;
    private Dictionary<string, Pickup> items;
    public GameObject nameChanger;
    public Transform parent;
    public InputField pcInputField;
    public GameObject pcScreen;
    public GameObject pickupIcon;

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

    private void Start()
    {
        nameChanger = gameObject.transform.GetChild(3).gameObject;
        pickupIcon = gameObject.transform.GetChild(2).gameObject;

        inventoryScreen = gameObject.transform.GetChild(0).gameObject;

        pcScreen = gameObject.transform.GetChild(1).gameObject;
        pcInputField = pcScreen.transform.GetChild(0).gameObject.GetComponent<InputField>();

        for (var i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(false);

        Web.instance.changeNameInput = nameChanger.transform.GetChild(0).gameObject.GetComponent<InputField>();

        DisableUi();
    }

    public void DisableUi()
    {
        for (var i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
    }

    public bool CompareCode(string code)
    {
        if (pcInputField.text == code)
        {
            //open door
            pcInputField.gameObject.SetActive(false);
            return true;
        }

        return false;
    }

    public void RegisterPickUpItem(Pickup i)
    {
        if (!items.ContainsKey(i.objectName)) items.Add(i.objectName, i);
    }

    public void Add(Item i)
    {
        if (items.ContainsKey(i.name) && !items[i.name].isInInventory())
        {
            var go = Instantiate(itemPrefab, parent, false);
            go.GetComponent<Image>().sprite = items[i.name].image;
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.name;
            items[i.name].setInventoryObj(go);
        }
    }

    public void Remove(Item i)
    {
        if (items.ContainsKey(i.name)) items[i.name].Respawn();
    }
}