using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Database : MonoBehaviour
{
    public static Database Instance;
    const string rootUrl = "http://localhost/elab/";

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    public void Search(string query)
    {
        StartCoroutine(SearchReq(query));
    }

    IEnumerator SearchReq(string query)
    {
        string[] queries = query.Split('-');
        foreach (var item in queries)
        {
            Debug.Log(item);
        }

        yield return null;
        UIManager.Instance.ClearPanel();
    }

    public void Login(WWWForm form)
    {
        StartCoroutine(LoginReq(form));
    }

    IEnumerator LoginReq(WWWForm form)
    {
        //Unity's method for sending packets
        using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "login.php", form))
        {
            yield return www.SendWebRequest();

            //If sending failed, output the fail error
            if (www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //If successful, download the response from the API
                string responseText = www.downloadHandler.text;
                if (responseText.StartsWith("GRANTED"))
                {
                    Authentication.Instance.Clear();
                    string[] dataChunks = responseText.Split('|');
                    if(int.Parse(dataChunks[3]) == 1)
                    {
                        Authentication.Instance.BeginSession(User.UserType.AUTHORIZED, dataChunks[1]);
                    }
                    else
                    {
                        Authentication.Instance.BeginSession(User.UserType.NORMAL, dataChunks[1]);
                    }
                }
                else Debug.Log(responseText);
            }
        }
    }

    public void Register(WWWForm form)
    {
        StartCoroutine(RegisterReq(form));
    }

    IEnumerator RegisterReq(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "register.php", form))
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
                Authentication.Instance.Clear();
            }
        }
    }
}
