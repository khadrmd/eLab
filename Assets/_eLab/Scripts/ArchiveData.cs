using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveData
{
    public static ArchiveData Instance;

    public int id;
    public string title;
    public string desc;
    public string date;
    public string type;
    public string author;
    public string img;

    public ArchiveData()
    {
        if (Instance == null) Instance = this;
        else
        {
            Instance = null;
            Instance = this;
        }
    }
}
