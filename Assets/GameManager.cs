using UnityEngine;


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
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
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
            basicCollectibles++;
        }
    }
}
