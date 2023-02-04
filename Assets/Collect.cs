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
            GameManager.instance.AddCollectible(collectibleType);
                 
            if (audioSource.enabled)
            {
                audioSource.Play();
                Debug.Log("asdf");
            }
            Destroy(gameObject);
        }
    }
}
