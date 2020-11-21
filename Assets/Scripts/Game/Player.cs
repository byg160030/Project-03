using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public Camera playerCamera;

    [Header("Gameplay")]
    public int initialHealth = 100;
    public int initialAmmo = 12;
    public float knockbackForce = 10;
    public float hurtDuration = 0.5f;

    private int health;
    public int Health { get { return health; } }

    private int ammo;
    public int Ammo { get { return ammo; } }

    private bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        ammo = initialAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0)
            {
                ammo--;

                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet();
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                bulletObject.transform.forward = playerCamera.transform.forward;
            }

        }
    }

    // Check for collisions.
    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.collider.GetComponent<AmmoCrate>() != null)
        {
            // Collect ammo crates
            AmmoCrate ammoCrate = hit.collider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);
        }
        else if (hit.collider.GetComponent<Enemy>() != null)
        {
            if (isHurt == false)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                health -= enemy.damage;

                isHurt = true;

                // Perform the knockback effect.
                Vector3 hurtDirection = (transform.position - enemy.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReciever>().AddForce (knockbackDirection, knockbackForce);

                StartCoroutine(HurtRoutine());
            }
        }
    }

    IEnumerator HurtRoutine ()
    {
        yield return new WaitForSeconds(hurtDuration);

        isHurt = false;
    }
}
