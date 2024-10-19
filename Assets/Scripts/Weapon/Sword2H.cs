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
        // 计算和显示接触点
        // 这里我们简单使用物体的位置作为碰撞点，实际应用中可以通过其他方式计算
        Vector3 contactPoint = other.bounds.ClosestPoint(transform.position);
        // 在控制台输出接触点
        Debug.Log($"接触点: {contactPoint}");
        // 用 Debug.DrawSphere 在接触点位置绘制一个球（这个球将只在 Game 视图中可见）
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
