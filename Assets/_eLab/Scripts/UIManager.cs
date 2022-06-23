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
    public ToggleGroup searchToggleGroup;
    public Toggle events;
    public Toggle projects;
    public Toggle achievements;
    public Toggle saved;
    public TMP_InputField searchDate;

    [Header("Create Panel")]
    public GameObject createPanel;
    public TMP_InputField createTitle;
    public TMP_InputField createDesc;
    public TMP_Dropdown createType;
    public TMP_InputField createDate;

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
        if (SceneManager.GetActiveScene().buildIndex == 0) //If on auth page
        {
            LoginPage.SetActive(true);
            RegisterPage.SetActive(false);
        }else if (SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 5) _Init_();
        else
        {
            searchPanel.SetActive(false);
            _Init_();
        }
    }

    void _Init_()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            accountName.text = User.Instance.userName;
        }
    }

    public void OnBackButtonClicked()
    {
        if (User.Instance.userType == User.UserType.AUTHORIZED) AppManager.Instance.LoadScene(3);
        else if (User.Instance.userType == User.UserType.NORMAL) AppManager.Instance.LoadScene(2);
        else AppManager.Instance.LoadScene(1);
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
            createTitle.text = string.Empty;
            createDesc.text = string.Empty;
            createDate.text = string.Empty;
        }

        if (SceneManager.GetActiveScene().buildIndex <= 3)
        {
            searchDate.text = string.Empty;
            events.isOn = false;
            projects.isOn = false;
            achievements.isOn = false;
            if (saved != null) saved.isOn = false;
        }
    }

    public void OnSearchButtonClicked()
    {
        string query = "";
        query = string.Format("{0}|",  searchDate.text);
        foreach (var filter in searchToggleGroup.ActiveToggles())
        {
            query = query + filter.name;
        }
        AppManager.Instance.SearchArchive(query);
    }

    public void OnPostButtonClicked()
    {
        string[] attributes =  new string[5];
        attributes[0] = createTitle.text;
        attributes[1] = createDesc.text;
        attributes[2] = createType.options[createType.value].text;
        attributes[3] = createDate.text;
        attributes[4] = User.Instance.userName;

        AppManager.Instance.CreateArchive(attributes);
    }
}
    