using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class IceTrap : MonoBehaviour, ITrap
{
    Animator animator;
    ParticleSystem freezeParticle, readyParticle;

    [SerializeField] bool isActive = false;
    [SerializeField] float time = 3.0f;

    [SerializeField] List<Collider> colliders = new List<Collider>();
    [SerializeField] GameObject ice;

    void Awake()
    {
        animator = GetComponent<Animator>();
        freezeParticle = transform.Find("Freeze").GetComponent<ParticleSystem>();
        readyParticle = transform.Find("Ready").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliders.Remove(other);
        }
    }

    public void Activate()
    {
        animator.SetTrigger("Freeze");
        //StartCoroutine(activeEnumerator());
    }

    private IEnumerator activeEnumerator()
    {
        List<GameObject> iceObjs = new List<GameObject>();
        isActive = true;
        foreach (var collider in colliders)
        {
            collider.transform.parent.GetComponentInChildren<PlayerStatusEffect>().Frozen = true;
            GameObject iceObj = Instantiate(ice, new Vector3(collider.transform.position.x, 1.5f, collider.transform.position.z), Quaternion.Euler(-90, 0, 0));
            Physics.IgnoreCollision(iceObj.GetComponent<CapsuleCollider>(), collider);
            iceObjs.Add(iceObj);
        }
        yield return new WaitForSeconds(time);
        isActive = false;
        foreach (var iceObj in iceObjs)
        {
            Destroy(iceObj.gameObject);
        }
        iceObjs.Clear();
    }

    #region Animation Event

    void readyParticleStart() => readyParticle.Play();

    void readyParticleEnd() => readyParticle.Stop();

    void freeze()
    {
        StartCoroutine(activeEnumerator());
    }

    #endregion
}
