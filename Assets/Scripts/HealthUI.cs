using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;

    private void Awake()
    {
        healthBar.maxValue = PlayerManager._instance.player.MaxHealth;
        healthBar.value= PlayerManager._instance.player.CurrentHealth;
    }

    public void UpdateHealthUI()
    {
        healthBar.value = PlayerManager._instance.player.CurrentHealth;
    }
}
