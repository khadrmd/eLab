﻿using System.Collections;
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

    public void ReadArchive(WWWForm form)
    {
        StartCoroutine(ReadArchiveReq(form));
    }

    IEnumerator ReadArchiveReq(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(rootUrl + "read_archive.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                if (!responseText.StartsWith("No"))
                {
                    string[] archives = responseText.Split('#');
                    AppManager.Instance.LoadArchive(archives);
                }
                else
                {
                    Debug.Log(responseText);
                    AppManager.Instance.ClearContent();
                }
                UIManager.Instance.ClearPanel();
            }
        }
    }

    public void Login(WWWForm form)
    {
        StartCoroutine(LoginReq(form));
    }

    IEnumerator LoginReq(WWWForm form)
    {
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
