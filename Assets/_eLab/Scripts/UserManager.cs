using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager Instance;
    private void Awake()
    {
        _Init_();
    }

    void _Init_()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    public void Authorize(User user)
    {

    }

    public void Modify(User user)
    {

    }

    public void Modify(AuthorizedUser aUser)
    {

    }

    public void Login(string username, string password)
    {

    }

    public void Signup(string username, string email, string passwword, string cPassword)
    {

    }
}
