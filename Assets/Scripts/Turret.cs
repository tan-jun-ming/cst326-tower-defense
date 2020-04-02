using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private bool ghost = true;

    public float radius = 2f;
    public int max_fire_rate = 50;
    public GameObject bullet;

    private int cooldown;
    public int value = 15;

    [HideInInspector]
    public List<Transform> platforms = new List<Transform>();

    [HideInInspector]
    public TurretManager manager;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = max_fire_rate;

        Transform area = gameObject.transform.Find("Area");
        Vector3 new_area = area.localScale;
        new_area.x = radius * 2;
        new_area.z = radius * 2;

        area.localScale = new_area;
    }

    void set_alpha(float alpha)
    {
        foreach (Transform t in gameObject.transform.Find("TurretModel"))
        {
            MeshRenderer renderer = (MeshRenderer)t.gameObject.GetComponent(typeof(MeshRenderer));

            if (renderer == null)
            {
                continue;
            }

            Color col = renderer.material.color;
            col.a = alpha;
            renderer.material.color = col;
        }
        
    }

    Transform get_nearest_platform()
    {
        Transform min_platform = null;

        float distance = 15f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;

        int layermask = 1 << LayerMask.NameToLayer("TurretPlane");

        if (Physics.Raycast(ray.origin, ray.direction, out hitinfo, distance, layermask))
        {
            if (hitinfo.transform.name == "TurretPlane")
            {
                float min_dist = 999f;
                

                foreach (Transform p in platforms)
                {
                    if (p.childCount == 0)
                    {
                        float dist = (hitinfo.point - p.position).magnitude;

                        if (dist < min_dist)
                        {
                            min_dist = dist;
                            min_platform = p;
                        }
                    }
                }
            }
        }

        return min_platform;

    }

    // Update is called once per frame
    void Update()
    {
        if (ghost)
        {
            set_alpha(0.5f);

            Transform nearest_platform = get_nearest_platform();

            if (nearest_platform != null)
            {
                gameObject.transform.position = nearest_platform.position;

                if (Input.GetMouseButtonDown(0))
                {
                    gameObject.transform.parent = nearest_platform;
                    ghost = false;
                    set_alpha(1.0f);
                    manager.turret_built(value);
                }
            } else
            {
                gameObject.transform.position = Vector3.one * 1000;

                if (Input.GetMouseButtonDown(0))
                {
                    manager.destroy_current();
                }
            }

            return;
        }
    }

    void FixedUpdate()
    {
        if (ghost)
        {
            return;
        }

        if (cooldown <= 0)
        {
            if (fire())
            {
                cooldown = max_fire_rate;
            }

            return;
        }

        cooldown--;

    }

    bool fire()
    {
        Vector3 capsule_start = gameObject.transform.position;
        Vector3 capsule_end = gameObject.transform.position;

        capsule_start.y += 5f;
        capsule_end.y -= 5f;

        int layermask = 1 << LayerMask.NameToLayer("Enemy");

        Collider[] colliders = Physics.OverlapCapsule(capsule_start, capsule_end, radius, layermask);


        Transform target = null;
        float max_dist = 0;
        foreach (Collider c in colliders)
        {
            if (c.transform.CompareTag("Enemy"))
            {
                float dist = ((Enemy)c.gameObject.GetComponent(typeof(Enemy))).get_distance();
                if (dist > max_dist)
                {
                    max_dist = dist;
                    target = c.transform;
                }
            }
        }

        if (target == null)
        {
            return false;
        }

        GameObject new_bullet = GameObject.Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        Bullet bullet_script = (Bullet)new_bullet.GetComponent(typeof(Bullet));
        bullet_script.target = target;

        return true;
    }
}
