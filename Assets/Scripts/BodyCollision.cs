using UnityEngine;

public class BodyCollision : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        // Periksa apakah animator sudah diisi melalui Inspector
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Body collision with obstacle");
            if (animator != null)
            {
                animator.SetTrigger("hit_body");
            }
            else
            {
                Debug.LogWarning("Animator not assigned in BodyCollision script.");
            }
        }
    }
}
