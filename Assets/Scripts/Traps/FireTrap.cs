using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.ParticleSystem;

public class FireTrap : MonoBehaviour, ITrap
{
    Animator animator;
    VisualEffect VE;
    ParticleSystem sprayParticle, readyParticle;

    [SerializeField] bool isActive = false;
    [SerializeField] float time = 4.0f;
    [SerializeField] int damage = 2;

    [SerializeField] List<Collider> colliders = new List<Collider>();
    [SerializeField] float force = 20.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        VE = GetComponentInChildren<VisualEffect>();
        VE.Stop();
        sprayParticle = transform.Find("Particle").GetComponent<ParticleSystem>();
        readyParticle = transform.Find("Ready").GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        //if (!isActive) return;
        //// squeeze out
        //for (int i = 0; i < colliders.Count; ++i)
        //{
        //    Rigidbody rb = colliders[i].gameObject.GetComponent<Rigidbody>();
        //    Vector3 direction = colliders[i].transform.position - transform.position;
        //    float distance = direction.magnitude;
        //    rb.AddExplosionForce(force, new Vector3(transform.position.x, 0.5f, transform.position.z), 2f, 0.5f, ForceMode.Impulse);
        //    colliders.RemoveAt(i);
        //    i--;
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isActive) return;

        if (other.gameObject.CompareTag("Player"))
        {
            // deal damage
            other.transform.parent.GetComponentInChildren<PlayerHealth>().TakeDamage(damage);
            // squeeze out
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = other.transform.position - transform.position;
            float distance = direction.magnitude;
            rb.AddExplosionForce(force, new Vector3(transform.position.x, 0.5f, transform.position.z), 2f, 0.5f, ForceMode.Impulse);

            //colliders.Add(other);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    colliders.Remove(other);
    //}

    public void Activate()
    {
        animator.SetTrigger("Spray");
        //StartCoroutine(activeEnumerator());
    }

    //private IEnumerator activeEnumerator()
    //{
    //    isActive = true;
    //    VE.Play();
    //    particle.Play();
    //    yield return new WaitForSeconds(time);
    //    isActive = false;
    //    VE.Stop();
    //    yield return new WaitForSeconds(1);
    //    particle.Stop();
    //}

    #region Animation Event

    void readyParticleStart() => readyParticle.Play();

    void readyParticleEnd() => readyParticle.Stop();

    void sprayStart()
    {
        isActive = true;
        VE.Play();
        sprayParticle.Play();
    }

    void sprayEnd()
    {
        isActive = false;
        VE.Stop();
    }

    void particleEnd()
    {
        sprayParticle.Stop();
    }

    #endregion
}
