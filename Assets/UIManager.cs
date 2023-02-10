using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    TextMeshProUGUI rareCollectiblesText;
    TextMeshProUGUI hpText;
    TextMeshProUGUI timerText;
    void Start()
    {
        GameObject rareCollectiblesObj = transform.Find("RareCollectibles").gameObject;
        rareCollectiblesText = rareCollectiblesObj.GetComponent<TextMeshProUGUI>();

        GameObject hpObj = transform.Find("HP").gameObject;
        hpText = hpObj.GetComponent<TextMeshProUGUI>();

        timerText = transform.Find("Timer").gameObject.GetComponent<TextMeshProUGUI>();

        if (GameManager.instance != null)
        {
            hpText.text = GameManager.instance.hp.ToString();
            rareCollectiblesText.text = GameManager.instance.rareCollectibles.ToString();
            GameManager.instance.uiManager = this;
        }
        if (SceneManager.GetActiveScene().name == "YouWin")
        {
            SetTime();
        }
    }
    void SetTime()
    {
        float timer = GameManager.instance.timer;
        int hundreths = (int)(timer * 100) % 100;
        int seconds = Mathf.FloorToInt(timer % 60);
        int minutes = Mathf.FloorToInt(timer / 60);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundreths);
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.timerStarted)
        {
            SetTime();
        }
    }

    public void SetRareCollectibles(int newScore)
    {
        rareCollectiblesText.text = newScore.ToString();
    }

    public void SetHP(int newHP)
    {
        hpText.text = newHP.ToString();
    }
}
