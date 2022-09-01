using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IUpdateSelectedHandler
{
    public static Color Color;
    [SerializeField] private TMP_Text _colorText;
    private bool _isPressed = false;
    private float _timer;

    public void SetRandomColor()
    {
        Color = new Color(Color.r, Color.g, Random.Range(0, 255) / 255f);
        _colorText.text = ((int)(Color.b * 255f)).ToString();
        Debug.Log(Color);
    }

    public void SetPlusColor()
    {
        if (Color.r * 255f < 255f)
        {
            Color = new Color(Color.r + 1 / 255f, Color.g, Color.b);
            _colorText.text = ((int)(Color.r * 255f)).ToString();
            Debug.Log(Color);
        }
    }

    public void SetMinusColor()
    {
        if (Color.r * 255f > 0)
        {
            Color = new Color(Color.r - 1 / 255f, Color.g, Color.b);
            _colorText.text = ((int)(Color.r * 255f)).ToString();
            Debug.Log(Color);
        }
    }

    //Detect if a click occurs
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _timer = 0;
        _isPressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }
 
    public void OnUpdateSelected(BaseEventData data)
    {
        if (_isPressed)
        {
            _timer += Time.deltaTime;
            if (_timer > 0.5f)
            {
                if (data.selectedObject.CompareTag("PlusButton"))
                {
                    SetPlusColor();
                }
                else
                {
                    SetMinusColor();
                }
            }
        }
    }
}
