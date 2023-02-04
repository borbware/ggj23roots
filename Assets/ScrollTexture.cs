using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    [SerializeField] float ScrollX = 0.5f;
    [SerializeField] float ScrollY= 0.5f;
    void Update()
    {
        float OffsetX = Time.time * ScrollX;        
        float OffsetY = Time.time * ScrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
    }
}
