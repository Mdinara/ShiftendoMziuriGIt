using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownSliderScript : MonoBehaviour
{
    public Slider CooldownSlider;
    private float t = 0;

    void Start()
    {
        CooldownSlider.value = CooldownSlider.minValue;
    }

    void Update()
    {
        if (t < 1f && GooCleanerScript.Cooldown == true)
        {
            t += Time.deltaTime / 5f;
            CooldownSlider.value = Mathf.Lerp(CooldownSlider.maxValue, CooldownSlider.minValue, t);
        }

        if (CooldownSlider.value == CooldownSlider.minValue)
        {
            CooldownSlider.value = CooldownSlider.maxValue;
            t = 0f;
        }

    }
}
