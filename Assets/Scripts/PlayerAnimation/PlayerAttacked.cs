using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class PlayerAttacked : MonoBehaviour
{
    PlayerController controller;
    Animator animator;
    PlayerHealth health;
    PlayerStatusEffect statusEffect;
    HandLSlot handLSlot;
    AudioSource[] audioSource;
    Transform VFX;
    ParticleSystem hit;

    public bool isBlocking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        VFX = transform.Find("VFX");
        hit = VFX.GetChild(0).GetComponent<ParticleSystem>();
    }

    public void Initialize()
    {
        controller = transform.parent.GetComponent<PlayerController>();
        health = transform.parent.GetComponentInChildren<PlayerHealth>();
        statusEffect = transform.parent.GetComponentInChildren<PlayerStatusEffect>();
        handLSlot = transform.parent.GetComponent<QuickRefer>().handLSlot;
        audioSource = transform.parent.Find("Audio").GetComponents<AudioSource>();

        health.Initialize();
    }

    public void GetAttacked(int damage, float force, Collider collider, Vector3 point = new Vector3())
    {
        if (health.isInvincible) return;

        if (checkHit(collider))
        {
            Attacked(damage, force, collider);
        }
        else
        {
            Blocked(damage, point);
        }
    }

    public void Attacked(int damage, float force, Collider collider)
    {
        health.TakeDamage(damage);

        Vector3 position = transform.position - collider.bounds.center;
        position.y = 0;  // prevent wrong displacements
        knockbackPos += position.normalized * force;  // add all forces if get attacked at the same time

        if (isBlocking)
        {
            isBlocking = false;
            animator.SetBool("IsBlocking", false);
            animator.ResetTrigger("BlockAttacked");
        }

        if (statusEffect.lifeStatus == LifeStatusEnum.Alive)
        {
            if (damage == 1)
                animator.SetTrigger("Attacked");
            else
                animator.SetTrigger("HeavyAttacked");
        }
            
        audioSource[0].Play();
        hit.transform.rotation = Quaternion.Euler(0.0f, Mathf.Atan2(position.x, position.z) * Mathf.Rad2Deg, 0.0f);
        hit.Play();
    }

    public void Blocked(int damage, Vector3 point)
    {
        Shield shield = handLSlot.GetCurrentItemObj()!.GetComponent<Shield>();
        shield.Damage(damage);

        animator.SetTrigger("BlockAttacked");
        audioSource[1].Play();
        Transform spark = VFX.Find("Spark");
        spark.position = point;
        spark.GetComponent<ParticleSystem>().Play();
    }

    public void GetHeavyAttacked(int damage, float force, Collider collider)
    {
        if (!health.isInvincible)
        {
            health.TakeDamage(damage);

            Vector3 position = transform.position - collider.bounds.center;
            position.z = 0;  // prevent wrong displacements
            knockbackPos += position.normalized * force;  // add all forces if get attacked at the same time

            if (statusEffect.lifeStatus == LifeStatusEnum.Alive)
                animator.SetTrigger("HeavyAttacked");
            audioSource[0].Play();
            hit.transform.rotation = Quaternion.Euler(0.0f, Mathf.Atan2(position.x, position.z) * Mathf.Rad2Deg, 0.0f);
            hit.Play();
        }
    }

    private bool checkHit(Collider collider)
    {
        if (!isBlocking) return true;

        float angle = Mathf.Acos(Mathf.Clamp(Vector3.Dot(collider.transform.forward, gameObject.transform.forward), -1f, 1f));

        return angle < Mathf.PI / 2;
    }

    public void ShieldBroken()
    {
        isBlocking = false;
        animator.SetBool("IsBlocking", false);
        animator.ResetTrigger("BlockAttacked");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void Respawn()
    {
        animator.SetTrigger("Respawn");
    }

    #region Knockback

    Vector3 knockbackPos;
    float elapsedTime = 0f;
    float duration = 1f;

    private void knockback()
    {
        if (knockbackPos != Vector3.zero)
        {
            StartCoroutine(MoveToTarget());
        }
    }

    private IEnumerator MoveToTarget()
    {
        Vector3 targetPosition = controller.gameObject.transform.position + knockbackPos;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            controller.gameObject.transform.position = Vector3.Lerp(controller.gameObject.transform.position, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;

        knockbackPos = Vector3.zero;
    }

    #endregion

    #region Stun

    private void getStunned()
    {
        //controller.StopAllCoroutines();
        //controller.GetComponent<NavMeshAgent>().ResetPath();
        //statusEffect.Stunned = true;
    }

    void stopStunned()
    {
        //statusEffect.Stunned = false;
    }

    #endregion
}
