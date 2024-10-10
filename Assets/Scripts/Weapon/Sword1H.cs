using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class Sword1H : MonoBehaviour, IWeapon
{
    MeshCollider m_Collider;
    CapsuleCollider playerCollider;

    [SerializeField] bool isActive = false;
    [SerializeField] int damage = 1;
    public float force;

    private void Awake()
    {
        m_Collider = GetComponent<MeshCollider>();
        playerCollider = transform.root.GetChild(0).GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        Physics.IgnoreCollision(m_Collider, playerCollider);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!isActive) return;
        if (hitColliders.Contains(collider)) return;

        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.transform.parent.GetComponentInChildren<PlayerAttacked>().GetAttacked(damage, force, playerCollider);
        }
        else return;

        hitColliders.Add(collider);
    }

    #region One Strike

    List<Collider> hitColliders = new List<Collider>();  // avoid many interactions in one action

    public void StartOneStrike()
    {
        isActive = true;
    }

    public void EndOneStrike()
    {
        isActive = false;
        hitColliders.Clear();
    }
     
    #endregion
}
