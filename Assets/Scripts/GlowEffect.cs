using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    [Tooltip("First color")]
    [SerializeField]
    Color colorA;

    [Tooltip("Second color")]
    [SerializeField]
    Color colorB;

    [Tooltip("Speed of color transition")]
    [SerializeField]
    float glowSpeed = 2.0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (spriteRenderer != null)
        {
            float sinValue = Mathf.Sin(Time.time * glowSpeed);
            float lerpTime = (sinValue + 1.0f) / 2.0f; // Value calculation for the sake of smooth lerping.
            spriteRenderer.color = Color.Lerp(colorA, colorB, lerpTime);
        }
    }
}
