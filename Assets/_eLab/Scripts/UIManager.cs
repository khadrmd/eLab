using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Home Page")]
    public Transform contentParent;

    [Header("Pages")]
    public GameObject LoginPage;
    public GameObject RegisterPage;

    [Header("Search Panel")]
    public GameObject searchPanel;
    public TMP_InputField searchField;
    public ToggleGroup toggleGroup;
    public Toggle events;
    public Toggle projects;
    public Toggle achievements;
    public Toggle saved;
    public TMP_InputField dateField;

    [Header("Create Panel")]
    public GameObject createPanel;
    public TMP_InputField title;
    public TMP_InputField desc;
    public TMP_Dropdown type;

    [Header("Account")]
    public TextMeshProUGUI accountName;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3) Debug.Log(type.options[type.value].text);

        if (SceneManager.GetActiveScene().buildIndex == 0) //If on auth page
        {
            LoginPage.SetActive(true);
            RegisterPage.SetActive(false);
        }
        else
        {
            searchPanel.SetActive(false);
            _Init_();
        }
    }

    void _Init_()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1) accountName.text = User.Instance.userName;
    }

    

    public void OnSignUpClicked()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) //If on auth page
        {
            Authentication.Instance.Clear();
            LoginPage.SetActive(false);
            RegisterPage.SetActive(true);
        }
        else return;
    }

    public void OnSignInClicked()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) //If on auth page
        {
            Authentication.Instance.Clear();
            RegisterPage.SetActive(false);
            LoginPage.SetActive(true);
        }
        else return;
    }

    public void OnSearchBarClicked()
    {
        ClearPanel();
        searchPanel.SetActive(true);
    }

    public void OnCreateButtonClicked()
    {
        ClearPanel();
        createPanel.SetActive(true);
    }

    public void OnClosePanelClicked()
    {
        ClearPanel();
        searchPanel.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == 3) createPanel.SetActive(false);
    }

    public void ClearPanel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            title.text = string.Empty;
            desc.text = string.Empty;
        }

        dateField.text = string.Empty;
        events.isOn = false;
        projects.isOn = false;
        achievements.isOn = false;
        if (saved != null) saved.isOn = false;
    }

    public void OnSearchButtonClicked()
    {
        string query = "";
        query = string.Format("{0}|", dateField.text);
        foreach (var filter in toggleGroup.ActiveToggles())
        {
            query = query + filter.name;
        }
        AppManager.Instance.SearchArchive(query);
    }
}
    