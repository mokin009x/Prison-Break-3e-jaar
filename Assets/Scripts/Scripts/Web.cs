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
                
                //getByName = JSON.Parse(combinedUrl);
                
                var test = getByName["summonerLevel"].Value; ////////////////////////////////////////////////////////////////////////// here
                
                
                Debug.Log( ":\nReceived: " + receivedText);
                yield return receivedText;


                
                
            }


        }
        
    }
    
    public void Awake()
    {
        baseUrl = "https://euw1.api.riotgames.com/lol/";
        apiNameUrlTemp = "summoner/v4/summoners/by-name/";
        apiKey = "?api_key=RGAPI-d27c7601-65c6-4bcf-b7f8-8d66a8743049";
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
        StartCoroutine(GetRequest(baseUrl, apiNameUrlTemp, defaultName, apiKey));



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
                getByName = JSON.Parse(combinedUrl);
                
                var test = getByName["id"].Value;
                
                
                receivedText = webRequest.downloadHandler.text;
                Debug.Log( ":\nReceived: " + receivedText);

                apiByNameReturnString = test;
                
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
            Debug.Log(yayayay);
        }

    }
}