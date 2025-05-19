// -----
// TankUIController.cs
// Prototype1 (v2)
// Manages ammo and health UI
// -----

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TankUIController : MonoBehaviour
{
    #region Variables

    [Header("Player UI")]
    [SerializeField] private Image playerHealthFill;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Image ammoIcon;

    [Header("Cooldown UI")]
    [SerializeField] private GameObject reloadBarObject;
    [SerializeField] private Image reloadBarFill;
    [SerializeField] private TextMeshProUGUI reloadText;

    [SerializeField] private GameObject fireCooldownBarObject;
    [SerializeField] private Image fireCooldownBarFill;
    [SerializeField] private TextMeshProUGUI fireCooldownText;

    #endregion


    #region Game Mechanic Methods

    //
    // Health
    //

    public void SetPlayerHealth(float current, float max)
    {
        playerHealthFill.fillAmount = current / max;
    }


    //
    // Ammo
    //

    public void SetAmmo(int current, int reserve)
    {
        if (reserve < 0)
            ammoText.text = $"{current} / ∞";
        else
            ammoText.text = $"{current} / {reserve}";
    }

    //
    // Cooldown
    //

    public void ShowReloadBar(bool show)
    {
        reloadBarObject.SetActive(show);
        if (show)
            reloadText.text = "Reloading...";
    }

    public void SetReloadProgress(float progress)
    {
        if (reloadBarFill != null)
            reloadBarFill.fillAmount = Mathf.Clamp01(progress);
    }

    public void ShowFireCooldownBar(bool show)
    {
        fireCooldownBarObject.SetActive(show);
        if (show)
            fireCooldownText.text = "Overheat...";
    }

    public void SetFireCooldownProgress(float progress)
    {
        if (fireCooldownBarFill != null)
            fireCooldownBarFill.fillAmount = Mathf.Clamp01(progress);
    }

    #endregion
}
