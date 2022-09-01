using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColorizebleObject
{
    void SetComponentColor();
    void SetColor(Color color);
    void SetObjectColorFromComponent();
    Color GetColor();
}
