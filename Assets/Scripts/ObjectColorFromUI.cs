using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectColorFromUI : MonoBehaviour, IColorizebleObject
{
    [SerializeField] private Color _color;
    private Image _image;
    private void Awake()
    {
        _image = gameObject.GetComponent<Image>();
    }

    public void SetComponentColor()
    {
        _color = _image.color;
    }

    public void SetObjectColorFromComponent()
    {
        _image.color = _color;
    }

    public void SetColor(Color color)
    {
        _color = color;
    }

    public Color GetColor()
    {
        return _image.color;
    }
}
