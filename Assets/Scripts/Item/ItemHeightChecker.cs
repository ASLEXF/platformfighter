using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeightChecker : MonoBehaviour
{
    [SerializeField] float height = -10.0f;

    private void Update()
    {
        if (transform.position.y < height)
        {
            Destroy(gameObject);
        }
    }
}
