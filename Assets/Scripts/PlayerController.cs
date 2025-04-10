using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    private float[] lanes = { -5.3f, 0.7f, 6.7f }; // Posisi jalur tetap
    private int currentLane = 1; // Mulai dari jalur tengah
    private float laneSwitchSpeed = 10f;
    private Vector3 targetPosition;

    private bool isSwitchingLane = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = new Vector3(lanes[currentLane], transform.position.y, transform.position.z);
    }

    void Update()
    {
        HandleJumpAndSlide();
        HandleLaneSwitch();
        MoveToTarget();
        KeepPlayerOnLane(); // Pastikan player tetap di jalur
    }

    void HandleJumpAndSlide()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetTrigger("slide");
        }
    }

    void HandleLaneSwitch()
    {
        if (isSwitchingLane) return; // Mencegah perpindahan saat masih bergerak

        if (IsPlayingAnimation("RunLookBack") || IsPlayingAnimation("idle"))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
        {
            currentLane--;
            isSwitchingLane = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentLane < lanes.Length - 1)
        {
            currentLane++;
            isSwitchingLane = true;
        }

        targetPosition = new Vector3(lanes[currentLane], transform.position.y, transform.position.z);
    }

    void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * laneSwitchSpeed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition;
            isSwitchingLane = false;
        }
    }

    void KeepPlayerOnLane()
    {
        // Pastikan posisi pemain tetap dalam jalur yang valid
        float closestLane = lanes[0];
        float minDistance = Mathf.Abs(transform.position.x - lanes[0]);

        for (int i = 1; i < lanes.Length; i++)
        {
            float distance = Mathf.Abs(transform.position.x - lanes[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestLane = lanes[i];
            }
        }

        // Jika posisi terlalu jauh dari jalur mana pun, paksa kembali ke jalur terdekat
        if (minDistance > 1f) // Batas aman dari jalur
        {
            transform.position = new Vector3(closestLane, transform.position.y, transform.position.z);
            currentLane = System.Array.IndexOf(lanes, closestLane); // Update currentLane agar tetap sesuai
        }
    }

    bool IsPlayingAnimation(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

}
