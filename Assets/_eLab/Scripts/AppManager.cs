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
        SearchArchive("||");
    }

    public void BeginSession(User.UserType userType, string userName, int id)
    {
        User user = new User();
        user.userType = userType;
        user.userName = userName;
        user.id = id;

        if (user.userType.Equals(User.UserType.AUTHORIZED)) LoadScene(3);
        else if (user.userType.Equals(User.UserType.NORMAL)) LoadScene(2);
    }

    public void BeginSession(User.UserType userType)
    {
        User user = new User();
        user.userType = userType;
        user.userName = null;
        user.id = 0;
        LoadScene(1);
    }

    public void SearchArchive(string query)
    {
        string[] att = query.Split('|');

        WWWForm form = new WWWForm();
        form.AddField("keyword", att[0]);
        form.AddField("date", att[1]);
        form.AddField("filter", att[2]);

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
            archAtt.id = int.Parse(attributes[0]);
            archAtt.title.text = attributes[1];
            archAtt.desc.text = attributes[2];
            archAtt.type.text = attributes[3];
            archAtt.author.text = attributes[4];
            archAtt.date.text = attributes[5];
            if (attributes[6].Equals("null")) archAtt.img.texture = imgPlaceholder;
            else
            {
                byte[] imgBytes = System.Convert.FromBase64String(attributes[6]);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(imgBytes);
                archAtt.img.texture = tex;
            }
        }
    }

    public void CreateArchive(string[] attributes)
    {
        WWWForm form = new WWWForm();
        form.AddField("title", attributes[0]);
        form.AddField("desc", attributes[1]);
        form.AddField("type", attributes[2]);
        form.AddField("date", attributes[3]);
        form.AddField("author", attributes[4]);

        Database.Instance.InputArchive(form);
    }

    public void DeleteArchive(int id)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);

        Database.Instance.DeleteArchive(form);
    }

    public void EditArchive(string[] attributes)
    {
        if(attributes.Length == 5)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", int.Parse(attributes[0]));
            form.AddField("title", attributes[1]);
            form.AddField("desc", attributes[2]);
            form.AddField("date", attributes[3]);
            form.AddField("type", attributes[4]);

            Database.Instance.UpdateArchive(form);
        }
        else
        {
            Debug.LogError("Edit Input ERROR");
        }
        
    }

    public void ClearContent()
    {
        Transform parent = UIManager.Instance.contentParent;
        if(parent != null)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}
