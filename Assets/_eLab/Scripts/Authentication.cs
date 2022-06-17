/*
Include library
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;                  //Unity's base library
using UnityEngine.UI;               //Unity's UI library
using TMPro;                        //TextMeshPro (Advance text plugin for Unity)
using UnityEngine.Networking;       //Unity's networking library
using UnityEngine.SceneManagement;

public class Authentication : MonoBehaviour
{
    public static Authentication Instance;

    //TextMestPro input field class
    [Header("Authentication")]
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;

    public TMP_InputField registerUsername;
    public TMP_InputField registerEmail;
    public TMP_InputField registerPassword;
    public TMP_InputField registerCPassword;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    //Once Login button is clicked, start login coroutine
    public void OnLoginButtonClicked()
    {
        WWWForm form = new WWWForm();

        form.AddField("email", loginEmail.text);
        form.AddField("password", loginPassword.text);
        Database.Instance.Login(form);
    }

    public void OnContinueAsGuestClicked()
    {
        SceneManager.LoadScene(1);
    }

    //Not finished yet
    public void OnRegisterButtonClicked()
    {
        if (!registerPassword.text.Equals(registerCPassword.text))
        {
            Debug.Log("Password does not match!");
        }
        else
        {
            WWWForm form = new WWWForm();

            form.AddField("fullname", registerUsername.text);
            form.AddField("email", registerEmail.text);
            form.AddField("password", registerPassword.text);
            form.AddField("cpassword", registerCPassword.text);
            Database.Instance.Register(form);
        }
    }

    public void Clear()
    {
        loginEmail.text = "";
        loginPassword.text = "";

        registerUsername.text = "";
        registerEmail.text = "";
        registerPassword.text = "";
        registerCPassword.text = "";
    }

    public void BeginSession(User.UserType userType, string userName)
    {
        User user = new User();
        user.userType = userType;
        user.userName = userName;

        if (user.userType.Equals(User.UserType.AUTHORIZED)) SceneManager.LoadScene(3);
        else if (user.userType.Equals(User.UserType.NORMAL)) SceneManager.LoadScene(2);
    }
}
