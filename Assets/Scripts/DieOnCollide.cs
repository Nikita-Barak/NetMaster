using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieOnCollide : MonoBehaviour
{
    AnimationController unitAnimator;
    NewMover unitMover;
    Rigidbody2D unitRB;
    Collider2D unitCollider;
    EnemyAI unitAI;
    [SerializeField] float delay = 0.4f;

    void Start()
    {
        unitAnimator = GetComponent<AnimationController>();
        unitMover = GetComponent<NewMover>();
        unitRB = GetComponent<Rigidbody2D>();
        unitCollider = GetComponent<Collider2D>();
        unitAI = GetComponent<EnemyAI>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if((unitCollider.CompareTag("Player") && other.collider.CompareTag("Enemy")) || other.collider.CompareTag("Trap"))
        { 
            if (unitAnimator != null && unitCollider != null && unitRB != null)
            {
                unitRB.simulated = false;                
                unitCollider.enabled = false;
                unitAnimator.HandleDeath();
            }

            if(unitCollider.CompareTag("Player"))
            {
                unitMover.enabled = false;                
                Invoke(nameof(ResetScene), delay);
            }
            else
            {
                unitAI.enabled = false;
                Invoke(nameof(DestroySelf), delay);
            }
        }
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
