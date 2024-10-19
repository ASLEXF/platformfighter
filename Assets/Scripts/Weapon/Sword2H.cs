using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Sword2H : MonoBehaviour, IWeapon
{
    BoxCollider boxCollider;
    CapsuleCollider playerCollider;

    [SerializeField] bool isActive = false;
    [SerializeField] int damage = 1;
    public float force = 2;

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

    private void OnTriggerEnter(Collider collider)
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
        // �������ʾ�Ӵ���
        // �������Ǽ�ʹ�������λ����Ϊ��ײ�㣬ʵ��Ӧ���п���ͨ��������ʽ����
        Vector3 contactPoint = other.bounds.ClosestPoint(transform.position);
        // �ڿ���̨����Ӵ���
        Debug.Log($"�Ӵ���: {contactPoint}");
        // �� Debug.DrawSphere �ڽӴ���λ�û���һ���������ֻ�� Game ��ͼ�пɼ���
        Debug.DrawLine(contactPoint, contactPoint + Vector3.up * 2f, Color.red, 3f);
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
