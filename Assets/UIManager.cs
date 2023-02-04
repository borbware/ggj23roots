using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject rareCollectiblesObj;
    TextMeshProUGUI rareCollectiblesText;
    void Start()
    {
        rareCollectiblesObj = transform.Find("RareCollectibles").gameObject;
        rareCollectiblesText = rareCollectiblesObj.GetComponent<TextMeshProUGUI>();
        GameManager.instance.uiManager = this;
    }
    public void SetRareCollectibles(int newScore)
    {
        rareCollectiblesText.text = newScore.ToString();
    }
}
