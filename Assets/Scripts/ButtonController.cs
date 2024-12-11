using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject gate;
    private bool pressed = false;
    [SerializeField] Color pressedColor;
    [SerializeField] Color unpressedColor;
    private SpriteRenderer buttonRenderer;
    private Collider2D buttonCollider;
    [SerializeField] float buttonCooldown = 1.0f;

    void Start()
    {
        buttonCollider = GetComponent<Collider2D>();
        buttonRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (pressed)
            {
                buttonRenderer.color = unpressedColor;
                buttonCollider.enabled = false;
                Invoke(nameof(ReEnableButton), buttonCooldown);
                pressed = false;

                gate.SetActive(true);
            }
            else
            {
                buttonRenderer.color = pressedColor;
                buttonCollider.enabled = true;
                Invoke(nameof(ReEnableButton), buttonCooldown);
                pressed = true;

                gate.SetActive(false);
            }
        }
    }

    void ReEnableButton()
    {
        buttonCollider.enabled = true;
    }
}
