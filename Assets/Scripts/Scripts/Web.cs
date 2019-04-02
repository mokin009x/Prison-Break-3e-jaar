using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.AI;
using UnityEngine.UI;


public class Web : MonoBehaviour
{
    public static Web instance;
    public string receivedText;
    public string currentCode;
    public GameObject changeNamePc;
    public InputField changeNameInput;
    

    
    
    //api stuff
    public string apiKey;
    public JSONNode getByName;
    public string defaultName;
    public string apiByNameReturnString;
    
    //api url templates
    public string baseUrl;
    public string apiNameUrlTemp;
    

    
    
    public void Awake()
    {
        baseUrl = "https://euw1.api.riotgames.com";
        apiNameUrlTemp = "/lol/summoner/v4/summoners/by-name/";
        apiKey = "?api_key=RGAPI-6812228c-1724-4411-81a1-bf3a8b2ce869";
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

    public void ApiChangeCodeByName()
    {
        if (changeNameInput.text != "")
        {
            string newName = changeNameInput.text;
            StartCoroutine(GetRequestByName(baseUrl, apiNameUrlTemp, newName , apiKey));
        }
       

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

        currentCode = "Current name = " + _name + "\n" + "Current code = " + apiByNameReturnString + "\n" + "Press E on this pc to change name";
        changeNamePc.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = currentCode;


    }

    




    // Update is called once per frame
    void Update()
    {

       
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(apiByNameReturnString);
        }

    }
}