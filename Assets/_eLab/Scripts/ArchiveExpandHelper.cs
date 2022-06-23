using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArchiveExpandHelper : MonoBehaviour
{
    public TextMeshProUGUI archiveId;
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;
    public TextMeshProUGUI date;
    public TextMeshProUGUI type;
    public TextMeshProUGUI author;
    public RawImage img;

    private void Start()
    {
        archiveId.text = "ARCHIVE - " + ArchiveData.Instance.id;
        title.text = ArchiveData.Instance.title;
        desc.text = ArchiveData.Instance.desc;
        date.text = ArchiveData.Instance.date;
        type.text = ArchiveData.Instance.type;
        author.text = ArchiveData.Instance.author;
        byte[] imgBytes = System.Convert.FromBase64String(ArchiveData.Instance.img);
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(imgBytes);
        img.texture = tex;
    }
}
