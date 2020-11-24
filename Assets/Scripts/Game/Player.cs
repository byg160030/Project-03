using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public Camera playerCamera;

    [Header("Audio")]
    public AudioSource shieldrechargingSound;
    public AudioSource lowshieldSound;
    public AudioSource noshieldSound;

    [Header("Gameplay")]
    public float updatedHealth;
    public float maxHealth;
    public float pointIncreasePersecond;
    public int initialAmmo = 12;
    public float knockbackForce = 10;
    public float hurtDuration = 0.5f;
    public Text healthUI;

    private int ammo;
    public int Ammo { get { return ammo; } }

    private bool killed;
    public bool Killed { get { return killed; } }

    private bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        updatedHealth = 100;
        maxHealth = 100;
        pointIncreasePersecond = 5;
        ammo = initialAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        updatedHealth += pointIncreasePersecond * Time.deltaTime;

        if (updatedHealth > maxHealth)
        {
            updatedHealth = 100;
            shieldrechargingSound.Play();
        }

        if (updatedHealth >= 50)
        {
            lowshieldSound.Play();
        }

        if (updatedHealth >= 20)
        {
            noshieldSound.Play();
        }

        if (updatedHealth < 0)
        {
            updatedHealth = 0;
        }

        healthUI.text = (int)updatedHealth + " Shield";

        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0 && Killed == false)
            {
                ammo--;

                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet(true);
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                bulletObject.transform.forward = playerCamera.transform.forward;
            }

        }
    }

    // Check for collisions.
    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<AmmoCrate>() != null)
        {
            // Collect ammo crates
            AmmoCrate ammoCrate = otherCollider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);
        }

        else if (otherCollider.GetComponent<HealthPack>() != null)
        {
            // Collect health pack
            HealthPack healthPack = otherCollider.GetComponent<HealthPack>();
            updatedHealth += healthPack.health;

            Destroy(healthPack.gameObject);
        }

        if (isHurt == false)
        {
            GameObject hazard = null;

            if (otherCollider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = otherCollider.GetComponent<Enemy>();
                if (enemy.Killed == false)
                {
                    hazard = enemy.gameObject;
                    updatedHealth -= enemy.damage;
                }
            }
            else if (otherCollider.GetComponent<Bullets>() != null)
            {
                Bullets bullet = otherCollider.GetComponent<Bullets>();
                if (bullet.ShotByPlayer == false)
                {
                    hazard = bullet.gameObject;
                    updatedHealth -= bullet.damage;
                    bullet.gameObject.SetActive(false);
                }
            }

            if (hazard != null)
            {
                isHurt = true;

                // Perform the knockback effect.
                Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReciever>().AddForce(knockbackDirection, knockbackForce);

                StartCoroutine(HurtRoutine());
            }

            if (updatedHealth <= 0)
            {
                if (killed == false)
                {
                    killed = true;

                    //OnKill();
                }
            }
        }
    }

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);

        isHurt = false;
    }
    private void OnKill()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
    }
}