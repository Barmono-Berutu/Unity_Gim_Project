using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mAnimator.SetTrigger("jump");
        }
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
}
