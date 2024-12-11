using System.Runtime.CompilerServices;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject gate;
    private bool pressed = false;
    private bool onButton = false;
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
        if (other.CompareTag("Player") && !onButton)
        {
            onButton = true;

            if (pressed)
            {
                pressed = false;
                buttonRenderer.color = unpressedColor;
                gate.SetActive(true);
            }
            else
            {
                pressed = true;
                buttonRenderer.color = pressedColor;
                gate.SetActive(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onButton = false;
            buttonCollider.enabled = false;
            Invoke(nameof(ReEnableButton), buttonCooldown);
        }
    }

    void ReEnableButton()
    {
        buttonCollider.enabled = true;
    }
}