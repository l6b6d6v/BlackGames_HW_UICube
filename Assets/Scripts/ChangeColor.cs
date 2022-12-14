using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private TMP_Text _redCanalText;
    [SerializeField] private TMP_Text _greenCanalText;
    [SerializeField] private TMP_Text _blueCanalText;

    private bool _isObjectSelected = false;
    private GameObject _selectedObject;

    private Camera _mainCamera;
    private GraphicRaycaster _raycaster;
    private PointerEventData _pointerEventData;
    private EventSystem _eventSystem;

    void Awake()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        _raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        _eventSystem = GetComponent<EventSystem>();
        //Set up the new Pointer Event
        _pointerEventData = new PointerEventData(_eventSystem);

        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneObjectLogic();
            CanvasObjectLogic();
        }

        if (Input.GetMouseButton(0))
        {
            ObjectIsFocused();
        }
    }

    private void CanvasObjectLogic()
    {
        foreach (RaycastResult result in RaycastResults())
        {
            _selectedObject = result.gameObject;
            ColorizeObject();
            UpdateColorText();
        }
    }

    private void SceneObjectLogic()
    {
        RaycastHit hit = CastRayToScene();
        if (hit.collider != null)
        {
            _selectedObject = hit.collider.gameObject;
            ColorizeObject();
            UpdateColorText();
        }
    }

    private void ColorizeObject()
    {
        Debug.Log("Hit " + _selectedObject.name);
        _isObjectSelected = true;
        _selectedObject.GetComponent<IColorizebleObject>().SetComponentColor();
        ButtonHandler.Color = _selectedObject.GetComponent<IColorizebleObject>().GetColor();
    }

    private void UpdateColorText()
    {
        _redCanalText.text = ((int)(ButtonHandler.Color.r * 255.0f)).ToString();
        _greenCanalText.text = ((int)(ButtonHandler.Color.g * 255.0f)).ToString();
        _blueCanalText.text = ((int)(ButtonHandler.Color.b * 255.0f)).ToString();
    }

    public void ObjectIsFocused()
    {
        if (_selectedObject != null)
        {
            if (_isObjectSelected)
            {
                _selectedObject.GetComponent<IColorizebleObject>().SetColor(ButtonHandler.Color);
                _selectedObject.GetComponent<IColorizebleObject>().SetObjectColorFromComponent();
            }
            else
                _selectedObject = null;
        }
    }

    private List<RaycastResult> RaycastResults()
    {
        //Set the Pointer Event Position to that of the mouse position
        _pointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        _raycaster.Raycast(_pointerEventData, results);

        return results;
    }

    private RaycastHit CastRayToScene()
    {
        Vector3 screenMousePosFar = new(
            Input.mousePosition.x,
            Input.mousePosition.y,
            _mainCamera.farClipPlane);
        Vector3 screenMousePosNear = new(
            Input.mousePosition.x,
            Input.mousePosition.y,
            _mainCamera.nearClipPlane);
        Vector3 worldMousePosFar = _mainCamera.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = _mainCamera.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
