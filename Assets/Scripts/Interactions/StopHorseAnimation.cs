using UnityEngine;

public class StopHorseAnimation : MonoBehaviour
{
    public Animator horseAnimator; // Assign this in the inspector
     public Animator bigWheelAnimator; // Assign this in the inspector
      public Animator smallWheelAnimator; // Assign this in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            horseAnimator.SetBool("StartRocking", false);
            bigWheelAnimator.SetBool("StartSpinning", false);
            smallWheelAnimator.SetBool("StartSpinning", false);
        }
    }
}
