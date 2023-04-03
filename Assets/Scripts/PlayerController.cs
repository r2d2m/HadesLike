using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CombatSystem {
    public float speed = 5f; // The movement speed of the player
    Animator animator;
    public Transform modelTransform; // Reference to the 3D model transform
    private float angle;
    [SerializeField] private float collisionCheckDistance = 0.01f;
    private float attackAnimationDuration;


    void Start() {
        animator = GetComponent<Animator>();
        attackAnimationDuration = getAnimationDuration();
    }

    float getAnimationDuration() {
        AnimationClip[] attackClips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in attackClips){
            if (clip.name == "Attack"){
                return clip.length;
            }
        } return 0;
    }

    void Update() {
        // Get the input from the horizontal and vertical axes
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
         // Calculate the movement vector
        Vector2 movement = new Vector2(horizontal, vertical);
        movement.Normalize();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, collisionCheckDistance, LayerMask.GetMask("Default"));

        // If no collider is hit, move the player
        if (hit.collider == null && !IsPerformingAttack()){
            transform.position += new Vector3(movement.x, movement.y, 0) * speed * Time.deltaTime;
        }

        bool isRunning = movement != Vector2.zero;
        animator.SetBool("isRunning", isRunning);

        if (movement != Vector2.zero) {
            angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        }

        float isoAngleX = Mathf.Lerp(35f, -35f, (Mathf.Sin(angle * Mathf.Deg2Rad) + 1f) / 2f);
        float isoAngleZ = Mathf.Lerp(35f, -35f, (Mathf.Cos(angle * Mathf.Deg2Rad) + 1f) / 2f);

        if (!IsPerformingAttack()) {
            modelTransform.rotation = Quaternion.Euler(new Vector3(isoAngleX, 90 - angle , isoAngleZ)); // Assuming the 3D model is facing up (along the positive Y-axis)
        }

        if (!IsPerformingAttack() && Input.GetKeyDown(KeyCode.Space)) {
            PerformAttack();
        }
    }

    private Vector2 CalculateIsometricMovement(float horizontal, float vertical) {
        // Calculate the isometric movement vector
        float x = (horizontal - vertical) * 0.5f;
        float y = (horizontal + vertical) * 0.5f;
        return new Vector2(x, y);
    }

    private void PlayerAttack() {
        Transform enemyTransform = GameObject.FindWithTag("Enemy").transform;
        Attack(enemyTransform);
    }

    private void PerformAttack() {
        Debug.Log("attack");
        animator.SetBool("isAttacking", true);

        PlayerAttack();

        // animator.SetBool("isAttacking", false);
    }

    private bool IsPerformingAttack(){
        int layerIndex = 0; // Assuming the attack animation is in the base layer
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(layerIndex);

        // Check if the player is in the attack animation or transitioning to/from it
        return currentState.IsName("Attack");
    }

    public void OnAttackFinished() {
        animator.SetBool("isAttacking", false);
    }
}
