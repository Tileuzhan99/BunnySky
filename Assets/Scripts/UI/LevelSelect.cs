using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons;
    public TextMeshProUGUI[] levelLabels;
    public Button bossButton;
    public TextMeshProUGUI bossLabel;
    public GameObject confirmPanel;

    void OnEnable()
    {
        confirmPanel.SetActive(false);
        Refresh();
    }

    void Refresh()
    {
        SaveData save = SaveSystem.Data;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool open = i < save.unlockedLevels;
            levelButtons[i].interactable = open;

            string name = "LEVEL " + (i + 1);
            if (!open) levelLabels[i].text = name + "\n<size=60%>LOCKED</size>";
            else if (save.bestTimes[i] > 0f) levelLabels[i].text = name + "\n<size=60%>BEST " + LevelLoader.FormatTime(save.bestTimes[i]) + "</size>";
            else levelLabels[i].text = name;
        }

        bool bossOpen = save.unlockedLevels > levelButtons.Length;
        bossButton.interactable = bossOpen;
        if (!bossOpen) bossLabel.text = "BOSS\n<size=60%>LOCKED</size>";
        else if (save.bossBeaten) bossLabel.text = "BOSS\n<size=60%>BEATEN</size>";
        else bossLabel.text = "BOSS";
    }

    public void PlayLevel(int index)
    {
        LevelLoader.levelIndex = index;
        SceneManager.LoadScene("Game");
    }

    public void PlayBoss()
    {
        SceneManager.LoadScene("Boss");
    }

    public void AskReset()
    {
        confirmPanel.SetActive(true);
    }

    public void CancelReset()
    {
        confirmPanel.SetActive(false);
    }

    public void ConfirmReset()
    {
        SaveSystem.ResetProgress();
        confirmPanel.SetActive(false);
        Refresh();
    }
}
