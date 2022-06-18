using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance;

    public GameObject archivePref;
    public Texture2D imgPlaceholder;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null) Instance = this;
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneAsync(index));
    }

    IEnumerator LoadSceneAsync(int index)
    {
        var asyncLoadLevel = SceneManager.LoadSceneAsync(index);
        Debug.Log("Loading the Scene");
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        Debug.Log("Scene Loaded");
        SearchArchive("|");
    }

    public void SearchArchive(string query)
    {
        string[] att = query.Split('|');

        WWWForm form = new WWWForm();
        form.AddField("date", att[0]);
        form.AddField("filter", att[1]);

        Database.Instance.ReadArchive(form);
    }

    public void LoadArchive(string[] archives)
    {
        ClearContent();
        foreach (var archive in archives)
        {
            string[] attributes = archive.Split('|');
            var arch = Instantiate(archivePref, UIManager.Instance.contentParent);
            Archive archAtt = arch.GetComponent<Archive>();
            archAtt.title.text = attributes[0];
            archAtt.desc.text = attributes[1];
            archAtt.type.text = attributes[2];
            archAtt.author.text = attributes[3];
            if (attributes[4].Equals("null")) archAtt.img.texture = imgPlaceholder;
            else
            {
                byte[] imgBytes = System.Convert.FromBase64String(attributes[4]);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(imgBytes);
                archAtt.img.texture = tex;
            }
        }
    }

    public void ClearContent()
    {
        Transform parent = UIManager.Instance.contentParent;
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
}
