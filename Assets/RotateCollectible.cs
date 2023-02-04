using UnityEngine;

public class RotateCollectible : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 250.0f;
    [SerializeField] float upDownAmplitude = 0.1f;
    [SerializeField] float upDownSpeed = 6.0f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * rotateSpeed, 0f));
        transform.localPosition = new Vector3(0f, upDownAmplitude * Mathf.Sin(Time.time * upDownSpeed), 0f);
    }
}
