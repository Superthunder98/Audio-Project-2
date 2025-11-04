using UnityEngine;

public class StartHorseAnimation : MonoBehaviour
{
    public Animator horseAnimator; // Assign this in the inspector
     public Animator bigWheelAnimator; // Assign this in the inspector
      public Animator smallWheelAnimator; // Assign this in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            horseAnimator.SetBool("StartRocking", true);
            bigWheelAnimator.SetBool("StartSpinning", true);
            smallWheelAnimator.SetBool("StartSpinning", true);
        }
    }
}
