using UnityEngine;

public class AnimationTriggerHeadDuck : MonoBehaviour
{
    public GameObject animatedObject; 
    private Animator animator;
    private bool isInTrigger = false;

    private void Start()
    {
        animator = animatedObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Duck Head", true);
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Duck Head", false);
            isInTrigger = false;
        }
    }
}