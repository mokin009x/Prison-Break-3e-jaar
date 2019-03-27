using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public GameObject pcUiElement;

    
    // Start is called before the first frame update
    void Start()
    {
        pcUiElement = IventoryUI.instance.pcScreen;
    }

    public void StartPc()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
