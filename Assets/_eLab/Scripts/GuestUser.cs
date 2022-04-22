using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestUser : MonoBehaviour
{
    protected Archive archive = Archive.Instance;

    string username; //Random string
    public void ViewArchive()
    {
        archive.View();
    }

    public void SearchArchive()
    {
        archive.Search("Title");
    }
}
