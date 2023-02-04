using System.Collections;
using UnityEngine;

public class ProgressionWall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int RaresThreshold = 3;
    [SerializeField] float speed = 3f;
    bool blocking = true; 
    bool movedone = false;

    void Update()
    {
        if (blocking)
        {
            if (GameManager.instance != null && GameManager.instance.rareCollectibles >= RaresThreshold)
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
    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(5f);
        movedone = true;
    }
}
