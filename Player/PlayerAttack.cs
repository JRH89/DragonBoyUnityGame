using System.Security.Authentication;
using System.Linq;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] public float attackRange = 0.5f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballsound;
    [SerializeField] private AudioClip SwordHit;
    [SerializeField] public LayerMask enemyLayers;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && cooldownTimer > attackCooldown && playerMovement.canAttack())
         Attack();
        if(Input.GetKeyDown(KeyCode.H) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        Attack2();
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballsound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void Attack2()
    {
        SoundManager.instance.PlaySound(SwordHit);
        anim.SetTrigger("attack2");
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
        enemy.GetComponent<Health>().TakeDamage(2);
        }
        
        
        cooldownTimer = 0;      
    }

    private void OnDrawGizmosSelected() {
        {
            if (attackPoint == null)
            return;
            
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            return i;
        }
        return 0;
    }
}
