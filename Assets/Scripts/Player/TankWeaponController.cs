// -----
// TankWeaponController.cs
// Prototype1 (v2)
// Manages ammo count and UI updates for tank weapons
// -----

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class TankWeaponController : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [Tooltip("Automatically set - do not add")]
    [SerializeField] private TankUIController tankUIController;

    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmoPerClip = 5;
    private int currentAmmo;
    private const string infiniteAmmoText = "∞";

    [Header("Timers")]
    [SerializeField] private float fireDelay = 2f;
    [SerializeField] private float reloadDelay = 2f;
    private bool isReloading = false;
    private bool isOnCooldown = false;

    [Header("Audio")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource fireSound;

    [Header("Projectile Settings")]
    [SerializeField] private GameObject shellPrefab;
    [Tooltip("Add the Muzzle Flash object here")]
    [SerializeField] private Transform firePoint;

    #endregion

    #region Game Cycle Methods

    void Start()
    {
        currentAmmo = maxAmmoPerClip;
        UpdateAmmoUI();
    }

    void Update()
    {
        //Reload if input triggered and not already full or reloading
        if (TankInputHandler.Instance.ReloadTriggered() && isReloading == false && currentAmmo < maxAmmoPerClip)
            StartCoroutine(ReloadRoutine());

        if (TankInputHandler.Instance.PrimaryFireTriggered() && isReloading == false && isOnCooldown == false && currentAmmo > 0)
            Fire();
    }

    #endregion

    #region Game Mechanic Methods

    private void Fire()
    {
        currentAmmo--;
        Instantiate(shellPrefab, firePoint.position, firePoint.rotation);
        muzzleFlash?.Play();
        fireSound?.Play();
        UpdateAmmoUI();
        Debug.Log("Fired shell! Remaining: " + currentAmmo);
        StartCoroutine(FireCooldownRoutine());
    }

    private IEnumerator FireCooldownRoutine()
    {
        isOnCooldown = true;
        tankUIController.ShowFireCooldownBar(true);

        float timer = 0f;
        while (timer < fireDelay)
        {
            timer += Time.deltaTime;
            float progress = timer / fireDelay;
            tankUIController.SetFireCooldownProgress(progress);
            yield return null;
        }

        tankUIController.SetFireCooldownProgress(0f);
        tankUIController.ShowFireCooldownBar(false);
        isOnCooldown = false;
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;
        tankUIController.ShowReloadBar(true);

        float timer = 0f;
        while (timer < reloadDelay)
        {
            timer += Time.deltaTime;
            float progress = timer / reloadDelay;
            tankUIController.SetReloadProgress(progress);
            yield return null;
        }

        currentAmmo = maxAmmoPerClip;
        tankUIController.SetReloadProgress(0f);
        tankUIController.ShowReloadBar(false);
        UpdateAmmoUI();
        isReloading = false;
    }

    private void UpdateAmmoUI()
    {
        tankUIController.SetAmmo(currentAmmo, -1);   // -1 signifies infinite
    }

    #endregion
}
