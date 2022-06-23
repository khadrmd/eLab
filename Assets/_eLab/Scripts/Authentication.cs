using System.Collections;
using System.Collections.Generic;
using UnityEngine;                  
using UnityEngine.UI;               
using TMPro;                        
using UnityEngine.Networking;       
using UnityEngine.SceneManagement;

public class Authentication : MonoBehaviour
{
    public static Authentication Instance;

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

    public void OnLoginButtonClicked()
    {
        WWWForm form = new WWWForm();

        form.AddField("email", loginEmail.text);
        form.AddField("password", loginPassword.text);
        Database.Instance.Login(form);
    }

    public void OnContinueAsGuestClicked()
    {
        AppManager.Instance.BeginSession(User.UserType.GUEST);
    }

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
}
