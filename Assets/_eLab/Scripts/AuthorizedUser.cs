using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthorizedUser : User
{
    public void CreateArchive()
    {
        archive.Create();
    }

    public void RemoveArchive()
    {
        archive.Remove();
    }

    public void ModifyArchive()
    {
        archive.Modify();
    }
}
