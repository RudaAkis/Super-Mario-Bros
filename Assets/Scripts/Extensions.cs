using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 dir)
    {
        float radius = 0.1f;
        float distance = 0.3f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, dir, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    //Test how similar the direction of 2 vector is
    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 dir = other.position - transform.position;
        return Vector2.Dot(dir.normalized, testDirection) > 0.25f;
    }
}
