using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField] Collider Player;
    [SerializeField] Collider MovePoint;
    [SerializeField] Collider ControlPoint;

    private void Start()
    {
        Physics.IgnoreCollision(Player, MovePoint);
        Physics.IgnoreCollision(Player, ControlPoint);
        Physics.IgnoreCollision(MovePoint, ControlPoint);
    }
}
