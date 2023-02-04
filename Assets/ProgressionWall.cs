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
        for (int i = 0; i < RaresThreshold; i++ )
        {
            float delta = 8.0f / RaresThreshold;
            GameObject doge = Instantiate(dog, 
                dogPosition.transform.position + new Vector3(i * delta - (RaresThreshold - 1) * delta / 2f, 0f, 0f),
                Quaternion.identity,
                this.gameObject.transform
            );
            doge.transform.localScale = new Vector3(10f, 10f, 10f);
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
