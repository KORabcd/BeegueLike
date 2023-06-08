using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Entity entity;
    public Slider slider;
    public float HPPercent{ get; set; }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = entity.entityStatus.maxHealth;
        slider.value = entity.entityStatus.currentHealth;
    }
}
