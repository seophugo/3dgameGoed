using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform shootPoint;       // kan op camera of player hoofd zitten
    public float bulletSpeed = 20f;
    public int maxAmmo = 10;
    public int bulletDamage = 1;
    public float reloadTime = 2f;

    private int currentAmmo;
    private bool isReloading = false;

    [Header("UI")]
    public TextMeshProUGUI ammoText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip reloadClip;

    [Header("Camera")]
    public Camera playerCamera;  // sleep hier je Camera in

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        // Schieten
        if (Input.GetMouseButtonDown(0) && !isReloading)
            Shoot();

        // Reload
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
            StartCoroutine(Reload());
    }

    void Shoot()
    {
        if (currentAmmo <= 0) return;

        currentAmmo--;

        // Ray van camera naar crosshair
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100f); // ver weg als niets raakt

        // Spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Vector3 direction = (targetPoint - shootPoint.position).normalized;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = direction * bulletSpeed;

        bullet.transform.forward = direction; // zodat bullet model goed kijkt

        // Damage instellen
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.damage = bulletDamage;

        UpdateAmmoUI();
    }

    IEnumerator Reload()
    {
        isReloading = true;

        if (audioSource != null && reloadClip != null)
            audioSource.PlayOneShot(reloadClip);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
    }

    public void AddAmmoUpgrade(int amount)
    {
        maxAmmo += amount;
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
            ammoText.text = currentAmmo + " / " + maxAmmo;
    }
}
