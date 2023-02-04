using System.Collections;
using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    Vector3 dir;
    public float distance = 1000f;
    CharacterController1 controller;
    float pushforce = 0;

    bool moving = false;
    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Enemy" && moving == false)
        {
            dir = transform.position - other.transform.position;
            moving = true;
            StartCoroutine(BumpDelay());
            
            if (GameManager.instance != null)
                GameManager.instance.AddHP(-1);
            GetComponent<AudioSource>().Play();
            pushforce = 20;
        }
    }
    void Update()
    {
        controller = GetComponent<CharacterController1>();
        if (moving)
        {
            Debug.Log(dir);
            if (pushforce > 0)
            {
                controller.Move(dir, pushforce);
                pushforce -= Time.deltaTime * 10;
            }
        }
    }
 
    IEnumerator BumpDelay()
    {
        yield return new WaitForSeconds(0.7f);
        moving = false;
    }
}
