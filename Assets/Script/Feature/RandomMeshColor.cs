using UnityEngine;

public class RandomMeshColor : MonoBehaviour
{
    public Renderer m_Renderer;
    void Start()
    {
        m_Renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
