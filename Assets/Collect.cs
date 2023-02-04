using UnityEngine;
using static GameManager;

public class Collect : MonoBehaviour
{
    [SerializeField] CollectibleType collectibleType = CollectibleType.Basic;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = true;
    }


    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player")
        {
            if (GameManager.instance != null)
                GameManager.instance.AddCollectible(collectibleType);
                 
            if (audioSource.enabled)
            {
                audioSource.Play();
            }
            Destroy(gameObject);
        }
    }
}
