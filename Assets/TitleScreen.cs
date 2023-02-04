using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] string firstLevel = "Level1";


    void Update()
    {
        if (Input.anyKey) {
            SceneManager.LoadScene(firstLevel);
        }
    }
}
