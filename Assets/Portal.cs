using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] string nextLevel;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
