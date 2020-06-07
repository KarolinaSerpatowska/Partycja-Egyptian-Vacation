using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;

    public void setMax(float val)
    {
        slider = this.GetComponent<Slider>();
        //Debug.Log(val);

        slider.maxValue = val;
        slider.value = slider.maxValue;
    }

    public void setBar(float hp)
    {
        slider.value = hp;
    }
}
