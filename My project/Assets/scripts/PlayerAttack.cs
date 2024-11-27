using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the player's animator
    }

    void Update()
    {
        // Listen for the F key press to trigger the attack
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking) // Check if F is pressed and if not already attacking
        {
            Attack();
        }
    }

    void Attack()
    {
        // Prevent spamming the attack if it's already in progress
        isAttacking = true;

        // Trigger the attack animation (assuming you have a trigger parameter named "AttackTrigger")
        animator.SetTrigger("AttackTrigger");

        // You can also apply the attack logic here (e.g., damage detection, etc.)
        // For example:
        // Detect enemies in range and apply damage to them

        // After the attack animation is done, reset the attacking state
        // Assuming the attack animation has a known length, reset after that.
        // Alternatively, you can listen for the animation end event to reset it.
        Invoke("ResetAttack", 1.0f); // Reset after 1 second (assuming the animation duration is 1 second)
    }

    void ResetAttack()
    {
        isAttacking = false;
    }
}
