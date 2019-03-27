using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public static Web instance;
    public string receivedText;
    public string pcCode;


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

                pcCode = receivedText;
            }
        }
    }
    
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
        
    }
    
    // Start is called before the first frame update
    void Start()
    { 
        StartCoroutine(GetRequest("https://euw1.api.riotgames.com/lol/champion-mastery/v4/scores/by-summoner/cqOXbSOi9pHlwamaVmMyGW8IOc-iAFeNhtuBGkffl__2g-M?api_key=RGAPI-ad690d44-2f9e-4b9f-b653-053f1fd427e9"));

       


    }




    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(pcCode);
        }

    }
}