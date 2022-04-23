using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Authentication : MonoBehaviour
{
    const string rootUrl = "http://localhost/elab/";

    [Header("Authentication")]
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;

    public TMP_InputField registerUsername;
    public TMP_InputField registerEmail;
    public TMP_InputField registerPassword;
    public TMP_InputField registerCPassword;

    public void OnLoginButtonClicked()
    {
        StartCoroutine(Login());
    }

    public void OnRegisterButtonClicked()
    {

    }

	IEnumerator Login()
	{
		WWWForm	form = new WWWForm();

		form.AddField("email", loginEmail.text);
		form.AddField("password", loginPassword.text);

        using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                if (responseText.StartsWith("GRANTED"))
                {
                    string[] dataChunks = responseText.Split('|');
                    foreach (var data in dataChunks)
                    {
                        Debug.Log(data);
                    }
                }
                else Debug.Log(responseText);
            }
        }
	}
}
