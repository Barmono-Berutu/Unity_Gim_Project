using UnityEngine;

public class FootCollision : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        // Periksa apakah animator sudah diisi melalui Inspector, jika tidak, baru ambil dari GameObject
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Foot collision with obstacle");
            if (animator != null)
            {
                animator.SetTrigger("hit_foot");
            }
            else
            {
                Debug.LogWarning("Animator not assigned in FootCollision script.");
            }
        }
    }
}
