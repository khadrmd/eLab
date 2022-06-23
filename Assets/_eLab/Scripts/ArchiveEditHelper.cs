using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArchiveEditHelper : MonoBehaviour
{
    public TextMeshProUGUI archiveId;
    public TMP_InputField title;
    public TMP_InputField desc;
    public TMP_InputField date;
    public TMP_Dropdown type;
    public RawImage img;

    public GameObject confirmDeletePanel;

    private void Start()
    {
        archiveId.text = "ARCHIVE - " + ArchiveData.Instance.id;
        title.text = ArchiveData.Instance.title;
        desc.text = ArchiveData.Instance.desc;
        date.text = ArchiveData.Instance.date;
        byte[] imgBytes = System.Convert.FromBase64String(ArchiveData.Instance.img);
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(imgBytes);
        img.texture = tex;
    }

    public void Delete()
    {
        confirmDeletePanel.SetActive(true);
    }

    public void OnYesButtonClicked()
    {
        AppManager.Instance.DeleteArchive(ArchiveData.Instance.id);
        CloseConfirmPanel();
        AppManager.Instance.LoadScene(3);
    }

    public void OnCancelButtonClicked()
    {
        CloseConfirmPanel();
    }

    public void CloseConfirmPanel()
    {
        confirmDeletePanel.SetActive(false);
    }
}
