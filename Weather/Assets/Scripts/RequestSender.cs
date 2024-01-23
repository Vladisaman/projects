using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class RequestSender : MonoBehaviour
{
    [SerializeField] GameObject LoginPanel;
    [SerializeField] GameObject SignupPanel;

    [SerializeField] TMP_InputField LoginUsername;
    [SerializeField] TMP_InputField LoginPass;
    [SerializeField] TMP_InputField SignupUsername;
    [SerializeField] TMP_InputField SignupPass;
    [SerializeField] TMP_InputField Email;

    [SerializeField] PlayerScript PlayerCharacter;

    public const string serverAddress = "http://localhost:8080/post/";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SendRequest(string body)
    {
        UnityWebRequest request = UnityWebRequest.Post(serverAddress, body, "application/json");
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success");
            Debug.Log(request.downloadHandler.text);

            string answer = request.downloadHandler.text.Substring(0, request.downloadHandler.text.Length - 2);
            Debug.Log(answer);
            string[] intents = answer.Split('|');
            switch (intents[0])
            {
                case "logged":
                    LoginPanel.SetActive(false);
                    Debug.Log(JsonUtility.FromJson<User>(intents[1]));
                    PlayerCharacter.GetUserData(JsonUtility.FromJson<User>(intents[1]));
                    break;
                case "regged":
                    SignupPanel.SetActive(false);
                    break;
                case "failedLog":
                    break;
                case "failedReg":
                    break;
            }
        } 
        else
        {
            Debug.Log("Unsuccess");
            Debug.Log(request.result + " " + request.responseCode);
        }
    }

    public void Login()
    {
        User user = new User(LoginUsername.text, LoginPass.text, "");
        StartCoroutine(SendRequest(JsonUtility.ToJson(user) + "|login"));
    }

    public void Signup()
    {
        User user = new User(SignupUsername.text, SignupPass.text, Email.text);
        StartCoroutine(SendRequest(JsonUtility.ToJson(user) + "|signup"));
    }
}

[System.Serializable]
public class User
{
    public string Username;
    public string Password;
    public string Email;
    public Character character;

    public User(string Username, string Password, string Email)
    {
        this.Username = Username;
        this.Password = Password;
        this.Email = Email;
    }

    [System.Serializable]
    public class Character
    {
        public int coins;
    }
}
