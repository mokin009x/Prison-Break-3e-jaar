using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Door : MonoBehaviour, IInteractible
{
    public int id;
    public bool open = false;
    public Quaternion doorStartAngle;
    
    
    // Start is called before the first frame update
    void Start()
    {
        doorStartAngle = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (open && transform.rotation.eulerAngles.y > -90)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -90, 0), maxDegreesDelta: 5);
        } else if (!open && transform.rotation.eulerAngles.y > 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(doorStartAngle.eulerAngles), maxDegreesDelta: 5);

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
