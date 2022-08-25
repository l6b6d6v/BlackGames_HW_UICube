using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    public Slider slider;
    public TMP_Text Color;

    private void Start()
    {
        slider.maxValue = 255;
        slider.minValue = 0;
        slider.onValueChanged.AddListener(v => {
            Color.text = v.ToString("0");
        });
    }

    public void Update()
    {
        if (((int)slider.value).ToString() != Color.text)
        {
            slider.value = int.Parse(Color.text);
        }
        else
        {
            slider.onValueChanged.AddListener(v => {
                Color.text = ((int)slider.value).ToString();
            });
        }
    }
}
