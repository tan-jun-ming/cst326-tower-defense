using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{

    private Transform healthbar;
    private SpriteRenderer border;
    private SpriteRenderer indicator;
    private Animator animator;

    public float health = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        healthbar = gameObject.transform.Find("Health");
        border = (SpriteRenderer)gameObject.transform.Find("Border").GetComponent(typeof(SpriteRenderer));
        indicator = (SpriteRenderer)healthbar.GetChild(0).GetComponent(typeof(SpriteRenderer));
        animator = (Animator)healthbar.GetChild(0).GetComponent(typeof(Animator));

        display_healthbar(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        display_healthbar(health != 1.0f);

        animator.SetFloat("health", health);
        Vector3 new_size = Vector3.one;
        new_size.x = health;
        healthbar.localScale = new_size;

    }

    void display_healthbar(bool do_display)
    {
        border.enabled = do_display;
        indicator.enabled = do_display;
    }
}
