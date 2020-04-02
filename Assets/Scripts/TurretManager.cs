using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public GameObject[] turrets;
    public Transform platform_list;
    public WalletManager walletmanager;

    private List<Transform> platforms = new List<Transform>();

    GameObject current_turret_selection = null;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        while (true)
        {
            Transform new_platform = platform_list.Find(i.ToString());
            if (new_platform == null)
            {
                break;
            }
            platforms.Add(new_platform);
            i++;
        }
    }

    void Update()
    {
        int to_show = -1;

        if (current_turret_selection == null)
        {
            float distance = 15f;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;

            int layermask = 1 << LayerMask.NameToLayer("Turret");

            if (Physics.Raycast(ray.origin, ray.direction, out hitinfo, distance, layermask))
            {
                to_show = hitinfo.transform.parent.parent.gameObject.GetInstanceID();
            }
        } else
        {
            to_show = -2;
        }

        foreach (Transform p in platforms)
        {
            if (p.childCount > 0)
            {
                MeshRenderer renderer = (MeshRenderer)p.GetChild(0).Find("Area").GetComponent(typeof(MeshRenderer));

                renderer.enabled = (to_show == -2 || p.GetChild(0).gameObject.GetInstanceID() == to_show);

            }
        }
    }

    public void spawn_turret(int turret_type)
    {
        GameObject to_build = turrets[turret_type];
        Turret tb_s = (Turret)to_build.GetComponent(typeof(Turret));

        int value = tb_s.value;

        if (!walletmanager.can_purchase(value)){
            return;
        }

        if (current_turret_selection == null)
        {
            GameObject new_turret = GameObject.Instantiate(turrets[turret_type], Vector3.one * 1000, Quaternion.identity);
            Turret tur = (Turret)new_turret.GetComponent(typeof(Turret));
            tur.platforms = platforms;
            tur.manager = this;

            current_turret_selection = new_turret;
        }
    }

    public void destroy_current()
    {
        GameObject.Destroy(current_turret_selection);
        dissociate_current();
    }

    public void turret_built(int value)
    {
        walletmanager.purchase(value);
        dissociate_current();
    }

    public void dissociate_current()
    {
        current_turret_selection = null;
    }
}
