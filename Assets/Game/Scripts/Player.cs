using System;
using System.Collections;
using TMPro;
using UnityEngine;

public sealed class Player : MonoBehaviour
{
    private static readonly int State = Animator.StringToHash("State");
    private const int IDLE_ANIM_STATE = 0;
    private const int MOVE_ANIM_STATE = 1;
    private const int ATTACK_ANIM_STATE = 2;
    private const int DEATH_ANIM_STATE = 3;

    [SerializeField]
    private float moveSpeed;
    
    [SerializeField]
    private float rotationSpeed;
 
    [SerializeField]
    private Animator animator;

    [SerializeField] private TextMeshProUGUI m_AttackCounter;
    [SerializeField] private TextMeshProUGUI m_DeathText;
    
    private Vector3 moveDirection;
    private bool isMoving;

    private bool canAttack = true;

    private int deathAttackCounter = 5;
    private int currentAttackCounter = 0;

    public bool CanMove = true;
    
    public void Move(Vector3 direction)
    {
        this.moveDirection = direction;
        this.isMoving = true;
    }

    private void Update()
    {
        m_AttackCounter.text = $"Attacks to death: {deathAttackCounter - currentAttackCounter}";
        
        if (canAttack && Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (this.isMoving)
        {
            this.UpdateMovement();
            this.animator.SetInteger(State, MOVE_ANIM_STATE); //MOVE
            this.isMoving = false;
        }
        else if(CanMove && canAttack)
        {
            this.animator.SetInteger(State, IDLE_ANIM_STATE); //IDLE
        }
    }

    private void UpdateMovement()
    {
        var deltaTime = Time.fixedDeltaTime;
        this.transform.position += moveDirection * this.moveSpeed * deltaTime;

        var targetRotation = Quaternion.LookRotation(this.moveDirection, Vector3.up);
        var nextRotation = Quaternion.Slerp(this.transform.rotation, targetRotation, this.rotationSpeed * deltaTime);
        this.transform.rotation = nextRotation;
    }
    
    private void Attack()
    {
        this.animator.Play("Attack {2}");

        currentAttackCounter++;

        if (currentAttackCounter >= deathAttackCounter)
        {
            Death();
            Destroy(m_AttackCounter);
            return;
        }

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(0.4f);

        canAttack = true;
    }

    private void Death()
    {
        m_DeathText.gameObject.SetActive(true);
        this.animator.SetInteger(State, DEATH_ANIM_STATE);
        CanMove = false;
        canAttack = false;
    }
}
