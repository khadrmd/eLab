using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        if(User.Instance.userType == User.UserType.AUTHORIZED)
        {
            optionButton.SetActive(true);
            saveToggle.SetActive(true);
        }else if(User.Instance.userType == User.UserType.NORMAL)
        {
            saveToggle.SetActive(true);
        }
        else
        {
            optionButton.SetActive(false);
            saveToggle.SetActive(false);
        }
    }

    public void Delete()
    {
        AppManager.Instance.DeleteArchive(this.id);
    }
}
