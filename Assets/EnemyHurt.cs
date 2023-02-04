using System.Collections;
using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    Vector3 dir;
    public float distance = 1000f;
    float time = 1.0f;

    bool moving = false;
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Enemy" && moving == false)
        {
            dir = transform.root.position - other.transform.root.position;
            moving = true;
            StartCoroutine(BumpDelay());
            
            Debug.Log("asdf");
            if (GameManager.instance != null)
                GameManager.instance.AddHP(-1);
        }
    }
    void Update()
    {
        if(moving)
        {
            Debug.Log(dir * Time.deltaTime * (distance / time));
            transform.Translate(dir * Time.deltaTime * (distance / time));
        }
    }
 
    IEnumerator BumpDelay()
    {
        yield return new WaitForSeconds(3f);
        moving = false;
    }
}
