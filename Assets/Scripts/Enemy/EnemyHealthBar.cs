using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private Canvas canvas;
    public float ActiveTime = 2;
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color=gradient.Evaluate(1f);
        canvas = this.GetComponentInParent<Canvas>();
        canvas.enabled = false;
    }
    public void SetHealth(float health)
    {
        canvas.enabled = true;
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (IsInvoking("ControlCanvas"))
        {
            CancelInvoke("ControlCanvas");
        }
        Invoke("ControlCanvas", ActiveTime);
    }
    private void ControlCanvas()
    {
        canvas.enabled = false;
    }
}
