using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColorFromScene : MonoBehaviour, IColorizebleObject
{
    [SerializeField] private Color _color;
    private MeshRenderer _mesh;

    private void Awake()
    {
        _mesh = gameObject.GetComponent<MeshRenderer>();
    }

    public void SetComponentColor()
    {
        _color = _mesh.material.color;
    }

    public void SetObjectColorFromComponent()
    {
        _mesh.material.color = _color;
    }

    public void SetColor(Color color)
    {
        _color = color;
    }

    public Color GetColor()
    {
        return _mesh.material.color;
    }
}
