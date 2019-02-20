using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{

    public float range = 2f;

   
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
            SetInventoryVisible(!IventoryUI.instance.gameObject.activeSelf);
            Cursor.visible = true;
        }
    }

    public void SetInventoryVisible(bool value)
    {
        IventoryUI.instance.gameObject.SetActive(value);
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
            IInteractible i = hit.collider.gameObject.GetComponent<IInteractible>();
            if (i != null)
            {
                i.Action();
            }
        }
    }
}