using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Archive : MonoBehaviour
{
    public int id;

    public RawImage img;
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;
    public TextMeshProUGUI type;
    public TextMeshProUGUI author;
    public TextMeshProUGUI date;

    public GameObject optionButton;
    public GameObject saveToggle;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex <= 3)
        {
            if (User.Instance.userType == User.UserType.AUTHORIZED)
            {
                optionButton.SetActive(true);
                saveToggle.SetActive(true);
            }
            else if (User.Instance.userType == User.UserType.NORMAL)
            {
                saveToggle.SetActive(true);
            }
            else
            {
                optionButton.SetActive(false);
                saveToggle.SetActive(false);
            }
        }
    }

    public void OnOptionButtonClicked()
    {
        ArchiveData archive = new ArchiveData();
        archive.id = id;
        archive.title = title.text;
        archive.desc = desc.text;
        archive.date = date.text;
        Texture2D tex = img.texture as Texture2D;
        archive.img = System.Convert.ToBase64String(tex.EncodeToPNG());
        AppManager.Instance.LoadScene(5);
    }
}
