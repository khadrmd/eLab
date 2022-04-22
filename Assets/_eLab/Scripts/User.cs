using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : GuestUser
{
    string username;
    
    public void SaveArchive()
    {
        archive.Save();
    }
}
