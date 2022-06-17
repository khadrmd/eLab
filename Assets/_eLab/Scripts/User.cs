using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public static User Instance;

    public enum UserType {AUTHORIZED, NORMAL, GUEST}

    public UserType userType;
    public string userName;

    public User()
    {
        if (Instance == null) Instance = this;
        else
        {
            Instance = null;
            Instance = this;
        }
    }
}
