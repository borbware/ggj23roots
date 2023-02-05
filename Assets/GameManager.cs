using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum CollectibleType {
        Basic,
        Rare
    }

    public static GameManager instance;
    public UIManager uiManager;
    public int rareCollectibles = 0;
    public int basicCollectibles = 0;
    public int hp = 4;
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            if (SceneManager.GetActiveScene().name == "TitleScreen")
            {
                Application.Quit();
            } else {
                SceneManager.LoadScene("TitleScreen");
            }
        }
        if (SceneManager.GetActiveScene().name == "YouWin" && Input.anyKey)
        {
            Application.Quit();
        }
    }
    

    public void AddHP(int HPdifference)
    {
        hp += HPdifference;
        
        if (uiManager != null)
            uiManager.SetHP(hp);
        if (hp == 0)
        {
            SceneManager.LoadScene("GameOver");
        }

    }
    public void AddCollectible(CollectibleType collectibleType)
    {
        if (collectibleType == CollectibleType.Rare)
        {
            rareCollectibles++;
            if (uiManager != null)
                uiManager.SetRareCollectibles(rareCollectibles);
        } else if (collectibleType == CollectibleType.Basic)
        {
            AddHP(1);
        }
    }
    public void ResetCollectibles()
    {
        rareCollectibles = 0;
        if (uiManager != null)
            uiManager.SetRareCollectibles(rareCollectibles);
    }
}
