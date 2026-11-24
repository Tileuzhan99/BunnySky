using UnityEngine;
using UnityEngine.UI;

public class ClickSound : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlayClick);
    }

    void PlayClick()
    {
        SoundManager.Play(SoundManager.instance.clickSound);
    }
}
