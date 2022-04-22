using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archive : MonoBehaviour
{
    public static Archive Instance;
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

    public void Search(string name)
    {

    }

    public void Search(string name, string filter)
    {

    }

    public void View()
    {

    }

    public void Save()
    {

    }

    public void Create()
    {

    }

    public void Remove()
    {

    }

    public void Modify()
    {

    }
}
