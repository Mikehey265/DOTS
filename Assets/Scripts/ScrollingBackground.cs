using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float backgroundScrollSpeed;
    [SerializeField] private Renderer backgroundRenderer;

    private void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(0, backgroundScrollSpeed * Time.deltaTime);
    }
}
