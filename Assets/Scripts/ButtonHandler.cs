using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IUpdateSelectedHandler
{
    public TMP_Text Color;
    private bool isPressed;
    private float timer;

    public void SetRandomColor()
    {
        Color.text = Random.Range(0, 255).ToString();
    }

    public void SetPlusColor()
    {
        if (int.Parse(Color.text) < 255)
            Color.text = (int.Parse(Color.text) + 1).ToString();
    }

    public void SetMinusColor()
    {
        if (int.Parse(Color.text) > 0)
            Color.text = (int.Parse(Color.text) - 1).ToString();
    }

    //Detect if a click occurs
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        timer = 0;
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
    IEnumerator Hold()
    {
        yield return new WaitForSeconds(0.5f);
    }


    public void OnUpdateSelected(BaseEventData data)
    {
        if (isPressed)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
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
