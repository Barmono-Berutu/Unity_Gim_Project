using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator mAnimator;
    private Rigidbody rb;

    [SerializeField] private float jumpForce = 13f;  // Tinggi lompatan
    [SerializeField] private float forwardJumpForce = 40f;  // Jarak lompat ke depan
    [SerializeField] private LayerMask groundMask;   // Untuk mendeteksi tanah

    private bool isGrounded;
    private float lastJumpTime;
    private float groundCheckDelay = 0.2f; // Waktu tunda sebelum bisa lompat lagi

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Pastikan Root Motion aktif di Animator
        mAnimator.applyRootMotion = true;
    }

    void Update()
    {
        // Periksa apakah karakter menyentuh tanah (dengan sedikit delay agar lebih natural)
        if (Time.time - lastJumpTime > groundCheckDelay)
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);
        }

        // Kontrol lompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Kontrol animasi lainnya
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            mAnimator.SetTrigger("strafe_left");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            mAnimator.SetTrigger("strafe_right");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            mAnimator.SetTrigger("dizzy");
        }
    }

    void Jump()
    {
        mAnimator.SetTrigger("jump");

        // Simpan waktu lompat agar tidak bisa lompat terus-menerus
        lastJumpTime = Time.time;

        // Reset kecepatan vertikal dan horizontal agar tidak ada sisa kecepatan sebelumnya
        rb.linearVelocity = Vector3.zero;

        // Tambahkan gaya ke atas (melompat) dan ke depan (maju)
        Vector3 jumpDirection = (Vector3.up * jumpForce) + (transform.forward * forwardJumpForce);
        rb.AddForce(jumpDirection, ForceMode.Impulse);
    }
}
