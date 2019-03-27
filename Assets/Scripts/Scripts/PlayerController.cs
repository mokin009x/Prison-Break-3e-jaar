using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{

    public float range = 2f;
    public Door finalDoor;
    private void Awake()
    {
        range = 4f;
    }

    

    void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            Interact();
            Debug.Log("pressed action");

        }
        
        if (Input.GetButtonDown("Inventory"))
        {
            SetInventoryVisible(!IventoryUI.instance.transform.GetChild(0).gameObject.activeSelf);
            
            Cursor.visible = true;
        }
        
      
    }

    public void CheckInput()
    {
        string currentCode;
        currentCode = Web.instance.pcCode;
        //IventoryUI.instance.pcScreen.SetActive(false);
        if (IventoryUI.instance.CompareCode(currentCode))
        {
            //door open
            Debug.Log("open");
            IventoryUI.instance.pcScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            GetComponent<FirstPersonController>().enabled = true;
            finalDoor.open = true;


        }
        else
        {
            Debug.Log("wrong code");
        }
    }

    public void SetInventoryVisible(bool value)
    {
        IventoryUI.instance.inventoryScreen.SetActive(value);
        GetComponent<FirstPersonController>().enabled = !value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void Interact()
    {
        Ray r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(r.origin,r.direction);

        int ignorePlayer =~ LayerMask.GetMask("Player");

       
        if (Physics.Raycast(r, out hit, range, ignorePlayer))
        {    
            Debug.Log("Hit " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Pc"))
            {
                GetComponent<FirstPersonController>().enabled = false;
                IventoryUI.instance.pcScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            IInteractible i = hit.collider.gameObject.GetComponent<IInteractible>();
            if (i != null)
            {
                i.Action();
            }
        }
    }
}