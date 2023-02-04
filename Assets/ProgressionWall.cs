using System.Collections;
using UnityEngine;

public class ProgressionWall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int RaresThreshold = 3;
    [SerializeField] float speed = 3f;
    bool blocking = true; 
    bool playerNear = false; 
    bool movedone = false;
    [SerializeField] GameObject dog;
    [SerializeField] GameObject dogPosition;
    void Start()
    {
        for (int i = 1; i <= RaresThreshold; i++ )
        {
            GameObject doge = Instantiate(dog, 
                dogPosition.transform.position + new Vector3(i * 2.0f, 0f, 0f),
                Quaternion.identity,
                this.gameObject.transform
            );
            doge.transform.localScale = new Vector3(30f, 30f, 30f);
            doge.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        }
    }
    void Update()
    {
        if (blocking)
        {
            if (playerNear && GameManager.instance != null && GameManager.instance.rareCollectibles >= RaresThreshold)
            {
                blocking = false;
                StartCoroutine(MoveDelay());
            }
        } else {
            if (!movedone)
            {
                transform.Translate(new Vector3(0, Time.deltaTime * speed, 0));
            }

        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player")
        {
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "Player")
        {
            playerNear = false;
        }
    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(5f);
        movedone = true;
    }
}
