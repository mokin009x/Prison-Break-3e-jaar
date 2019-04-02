using System.Collections;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Web : MonoBehaviour
{
    public static Web instance;
    public string apiByNameReturnString;


    //api stuff
    public string apiKey;
    public string apiNameUrlTemp;

    //api url templates
    public string baseUrl;
    public InputField changeNameInput;
    public GameObject changeNamePc;
    public string currentCode;
    public string defaultName;
    public JSONNode getByName;
    public string receivedText;


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
    private void Start()
    {
        StartCoroutine(GetRequestByName(baseUrl, apiNameUrlTemp, defaultName, apiKey));
    }

    public void ApiChangeCodeByName()
    {
        if (changeNameInput.text != "")
        {
            var newName = changeNameInput.text;
            StartCoroutine(GetRequestByName(baseUrl, apiNameUrlTemp, newName, apiKey));
        }
    }


    private IEnumerator GetRequestByName(string _baseUrl, string _urlTemp, string _name, string _apiKey)
    {
        var combinedUrl = _baseUrl + _urlTemp + _name + _apiKey;

        using (var webRequest = UnityWebRequest.Get(combinedUrl))
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

                Debug.Log(":\nReceived: " + receivedText);

                getByName = JSON.Parse(receivedText);

                var recievedValue = getByName["summonerLevel"].Value;

                apiByNameReturnString = recievedValue;
            }
        }

        currentCode = "Current name = " + _name + "\n" + "Current code = " + apiByNameReturnString + "\n" +
                      "Press E on this pc to change name";
        changeNamePc.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = currentCode;
    }


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) Debug.Log(apiByNameReturnString);
    }
}