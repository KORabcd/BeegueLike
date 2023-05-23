using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Player player;
    public Slider slider;
    public float HPPercent{ get; set; }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = player.entityStatus.maxHealth;
        slider.value = player.entityStatus.currentHealth;
    }
}
