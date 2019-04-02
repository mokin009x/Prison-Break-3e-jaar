using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEditor.Experimental.GraphView;


public class Web : MonoBehaviour
{
    public static Web instance;
    public string receivedText;
    public string pcCode;
    public string test;
    string yayayay;

    
    
    //api stuff
    public string apiKey;
    public JSONNode getByName;
    public string defaultName;
    public string apiByNameReturnString;
    
    //api url templates
    public string baseUrl;
    public string apiNameUrlTemp;
    

    IEnumerator GetRequest(string _baseUrl, string _urlTemp, string _name, string _apiKey)
    {
        
        string combinedUrl = _baseUrl + _urlTemp + _name + _apiKey;
        Debug.Log("Combined url =" + combinedUrl);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(combinedUrl))
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
                
                
                var test = getByName["summonerLevel"].Value; ////////////////////////////////////////////////////////////////////////// here
                
                
                Debug.Log( ":\nReceived: " + receivedText);
                yield return receivedText;


                
                
            }


        }
        
    }
    
    public void Awake()
    {
        baseUrl = "https://euw1.api.riotgames.com";
        apiNameUrlTemp = "/lol/summoner/v4/summoners/by-name/";
        apiKey = "?api_key=RGAPI-4502af58-3eb4-4519-a21b-09c201a82a54";
        defaultName = "GoddessWithBIade";
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
        StartCoroutine(GetRequestByName(baseUrl, apiNameUrlTemp, defaultName, apiKey));



    }
    
    
    IEnumerator GetRequestByName(string _baseUrl, string _urlTemp, string _name, string _apiKey)
    {
        string combinedUrl = _baseUrl +_urlTemp + _name + _apiKey;
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(combinedUrl))
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
                
                getByName = JSON.Parse(receivedText);
                
                string recievedValue = getByName["summonerLevel"].Value;
                
                apiByNameReturnString = recievedValue.ToString();
                
            }


        }

    }

    




    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            //GetRequestByName(baseUrl, apiNameUrlTemp, defaultName, apiKey);
           // GetRequest(baseUrl, defaultName,apiKey);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(apiByNameReturnString);
        }

    }
}