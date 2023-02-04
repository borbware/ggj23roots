using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    TextMeshProUGUI rareCollectiblesText;
    TextMeshProUGUI hpText;
    void Start()
    {
        GameObject rareCollectiblesObj = transform.Find("RareCollectibles").gameObject;
        rareCollectiblesText = rareCollectiblesObj.GetComponent<TextMeshProUGUI>();

        GameObject hpObj = transform.Find("HP").gameObject;
        hpText = hpObj.GetComponent<TextMeshProUGUI>();

        GameManager.instance.uiManager = this;
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
