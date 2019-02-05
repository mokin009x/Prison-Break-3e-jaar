using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float range = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        range = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            Interact();
        }
    }

    void Interact()
    {
        Ray r = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(r,out hit ,range))
        {
            
            Debug.Log(message:"Hit" + hit.collider.gameObject.name);
            IInteractible i = hit.collider.gameObject.GetComponent<IInteractible>();
            if (i != null)
            {
                i.Action();
            }
        }
        else
        {
            Debug.Log("nothing has been hit");
        }
    }
}
