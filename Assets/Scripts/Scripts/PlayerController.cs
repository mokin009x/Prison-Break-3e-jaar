using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{
    public Door finalDoor;
    public bool inMenu;

    public float range = 2f;

    private void Awake()
    {
        range = 4f;
    }

    private void Start()
    {
        inMenu = false;
    }

    private void Update()
    {
        var r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(r.origin, r.direction);


        if (Physics.Raycast(r, out hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Item")) IventoryUI.instance.pickupIcon.SetActive(true);
        }
        else if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            if (!hit.collider.gameObject.CompareTag("Item")) IventoryUI.instance.pickupIcon.SetActive(false);
        }

        if (Input.GetButtonDown("Action"))
            if (!inMenu)
            {
                Interact();
                Debug.Log("pressed action");
            }

        if (Input.GetButtonDown("Inventory"))
        {
            if (!inMenu) SetInventoryVisible(!IventoryUI.instance.transform.GetChild(0).gameObject.activeSelf);

            Cursor.visible = true;
        }
    }

    public void CheckInput()
    {
        string currentCode;
        currentCode = Web.instance.apiByNameReturnString;
        //IventoryUI.instance.pcScreen.SetActive(false);
        if (IventoryUI.instance.CompareCode(currentCode))
        {
            //door open
            Debug.Log("open");
            IventoryUI.instance.pcScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            GetComponent<FirstPersonController>().enabled = true;
            finalDoor.open = true;
            inMenu = false;
        }
        else
        {
            Debug.Log("wrong code");
        }
    }

    public void CloseUi()
    {
        IventoryUI.instance.DisableUi();
        inMenu = false;

        GetComponent<FirstPersonController>().enabled = true;
    }


    public void SetInventoryVisible(bool value)
    {
        IventoryUI.instance.inventoryScreen.SetActive(value);
        GetComponent<FirstPersonController>().enabled = !value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void Interact()
    {
        var r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(r.origin, r.direction);

        var ignorePlayer = ~ LayerMask.GetMask("Player");


        if (Physics.Raycast(r, out hit, range, ignorePlayer))
        {
            //Debug.Log("Hit " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Pc"))
            {
                inMenu = true;
                GetComponent<FirstPersonController>().enabled = false;
                IventoryUI.instance.pcScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (hit.collider.gameObject.CompareTag("Name Changer"))
            {
                inMenu = true;
                GetComponent<FirstPersonController>().enabled = false;
                IventoryUI.instance.nameChanger.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (hit.collider.gameObject.CompareTag("Item")) IventoryUI.instance.pickupIcon.SetActive(true);
            /*else if (transform)
            {
                IventoryUI.instance.pickupIcon.SetActive(false);

            }*/

            var i = hit.collider.gameObject.GetComponent<IInteractible>();
            if (i != null) i.Action();
        }
    }
}