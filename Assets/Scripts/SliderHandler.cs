using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] private TMP_Text _colorText;
    public void Update()
    {
        if (_slider.value != ButtonHandler.Color.g * 255f)
            _slider.value = ButtonHandler.Color.g * 255f;
        else
        {
            _slider.onValueChanged.AddListener(value => {
                ButtonHandler.Color = new Color(ButtonHandler.Color.r, value / 255f, ButtonHandler.Color.b);
                _colorText.text = _slider.value.ToString();
            });
        }
    }
}