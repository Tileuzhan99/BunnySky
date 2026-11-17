using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject levelsPanel;
    public GameObject settingsPanel;
    public TextMeshProUGUI messageText;

    void Start()
    {
        if (mainPanel != null) ShowMain();
        if (messageText != null) messageText.text = SaveSystem.message;
    }

    public void ShowMain()
    {
        mainPanel.SetActive(true);
        levelsPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowLevels()
    {
        mainPanel.SetActive(false);
        levelsPanel.SetActive(true);
    }

    public void ShowSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Quit()
    {
        SaveSystem.Save();
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
