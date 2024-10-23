using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe2H : MonoBehaviour, IWeapon
{
    BoxCollider boxCollider;
    CapsuleCollider playerCollider;

    [SerializeField] bool isActive = false;
    [SerializeField] int damage = 1;
    [SerializeField] float force = 2;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        playerCollider = GetComponentInParent<CapsuleCollider>();
    }

    private void Start()
    {
        Physics.IgnoreCollision(boxCollider, playerCollider);
        gameObject.tag = "Weapon";
    }

    private void OnTriggerStay(Collider collider)
    {
        if (!isActive) return;
        if (hitColliders.Contains(collider)) return;

        DrawContactPoint(collider);

        if (collider.gameObject.CompareTag("Player"))
        {
            collider.GetComponent<PlayerAttacked>().GetAttacked(damage, force, playerCollider);
            Debug.Log($"Hit {collider}");
        }
        else return;

        hitColliders.Add(collider);
    }

    private void DrawContactPoint(Collider other)
    {
        if (other.CompareTag("Platform")) return;
        Vector3 contactPoint = other.bounds.ClosestPoint(transform.position);
        Debug.Log($"½Ó´¥µã: {contactPoint}");
        Debug.DrawLine(contactPoint, contactPoint + Vector3.up * 2f, Color.red, 3f);
    }

    #region One Strike

    List<Collider> hitColliders = new List<Collider>();  // avoid duplicated interactions in one action

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
