using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Administrator : MonoBehaviour
{
    UserManager userManager = UserManager.Instance;

    public void AuthorizeUser(User user)
    {
        userManager.Authorize(user);
    }

    public void Modify(User user)
    {
        userManager.Modify(user);
    }

    public void Modify(AuthorizedUser aUser)
    {
        userManager.Modify(aUser);
    }
}
