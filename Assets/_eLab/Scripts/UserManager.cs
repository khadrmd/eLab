using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
}
