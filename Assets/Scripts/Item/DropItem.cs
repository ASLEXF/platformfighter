using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]
public class DropItem : MonoBehaviour
{
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;
    Rigidbody rb;
    MaterialPropertyBlock materialPropertyBlock;

    public ItemType itemType;
    public string itemName;

    public bool isThrown = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        materialPropertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        destroyOnHeight();
    }

    public void SetOutLineOn()
    {
        Material material = meshRenderer.material;
        material.EnableKeyword("_EMISSION");
    }

    public void SetOutLineOff()
    {
        Material material = meshRenderer.material;
        material.DisableKeyword("_EMISSION");
    }

    #region Thrown

    public List<GameObject> collisionObjs = new List<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        if (!isThrown) return;
        if (collisionObjs.Contains(collision.gameObject)) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent.GetComponentInChildren<PlayerAttacked>().GetHeavyAttacked(2, 0, meshCollider);
            collisionObjs.Add(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            isThrown = false;
            collisionObjs.Clear();
        }

        if (collision.gameObject.CompareTag("Shield") || collision.gameObject.CompareTag("Wall"))
        {
            isThrown = false;
            collisionObjs.Clear();
            GameEvents.Instance.ItemDropped();
            //Vector3 newDirection = (transform.right + transform.forward + transform.up).normalized;
            //rb.velocity = rb.velocity.magnitude * newDirection;
        }
    }

    #endregion

    #region Destroy

    void destroyOnHeight()
    {
        if (gameObject.transform.position.y < -10)
        {
            Destroy(this);
        }
    }

    #endregion

}
