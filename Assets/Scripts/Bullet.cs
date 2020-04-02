using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    private float speed = 0.1f;
    private float damage = 10;


    void FixedUpdate()
    {
        if (target == null)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        gameObject.transform.LookAt(target, Vector3.up);
        Vector3 new_pos = Vector3.MoveTowards(gameObject.transform.position, target.position, speed);

        gameObject.transform.position = new_pos;

        if (Mathf.Abs((gameObject.transform.position - target.position).magnitude) < 0.0001)
        {
            ((Enemy)target.gameObject.GetComponent(typeof(Enemy))).damage(damage);
            GameObject.Destroy(gameObject);
        }
    }
}
