using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation : MonoBehaviour
{
    [SerializeField] Vector3 orientationPoint;

    private void Update()
    {
        getRaycastHit();

        updateArrowRotation();
    }

    private void getRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Platform")))
        {
            orientationPoint = hit.point;
        }
    }

    private void updateArrowRotation()
    {
        float rotation = Mathf.Atan2(orientationPoint.z - transform.position.z, orientationPoint.x - transform.position.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(90f, 0.0f, rotation);
    }
}
