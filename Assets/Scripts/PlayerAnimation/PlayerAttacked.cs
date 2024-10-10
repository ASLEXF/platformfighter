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

    private void Awake()
    {
        controller = transform.parent.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        health = transform.parent.GetComponentInChildren<PlayerHealth>();
        statusEffect = transform.parent.GetComponentInChildren<PlayerStatusEffect>();
    }

    public void GetAttacked(int damage, float force, Collider collider)
    {
        if (!health.isInvincible)
        {
            health.TakeDamage(damage);

            Vector3 position = transform.position - collider.bounds.center;
            position.z = 0;  // prevent wrong displacements
            knockbackPos += position.normalized * force;  // add all forces if get attacked at the same time

            animator.SetTrigger("Attacked");
        }
    }

    public void GetHeavyAttacked(int damage, float force, Collider collider)
    {
        if (!health.isInvincible)
        {
            health.TakeDamage(damage);

            Vector3 position = transform.position - collider.bounds.center;
            position.z = 0;  // prevent wrong displacements
            knockbackPos += position.normalized * force;  // add all forces if get attacked at the same time

            animator.SetTrigger("HeavyAttacked");
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
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
        controller.StopAllCoroutines();
        controller.GetComponent<NavMeshAgent>().ResetPath();
        //statusEffect.Stunned = true;
    }

    void stopStunned()
    {
        //statusEffect.Stunned = false;
    }

    #endregion
}
