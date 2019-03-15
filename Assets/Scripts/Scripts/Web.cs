using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
  
    

    public string receivedText;
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                receivedText = webRequest.downloadHandler.text;
                Debug.Log( ":\nReceived: " + receivedText);
                
                
            }
        }
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/GoddessWithBIade%20?api_key=RGAPI-d6c10dc6-ce5c-4b79-9e3d-9089edb8cf22"));



    }




    // Update is called once per frame
    void Update()
    {
      

    }
}