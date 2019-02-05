using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Door : MonoBehaviour, IInteractible
{
    public int id;
    public bool open = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open && transform.rotation.eulerAngles.y < 80)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 80, 0), maxDegreesDelta: 5);
        } else if (!open && transform.rotation.eulerAngles.y > 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), maxDegreesDelta: 5);

        }
    }

    public void Open()
    {
        if (id == -1 || Inventory.instance.HasKey(id))
        {
            open = !open;
        }

    }

    public void Action()
    {
        Open();
    }
}
