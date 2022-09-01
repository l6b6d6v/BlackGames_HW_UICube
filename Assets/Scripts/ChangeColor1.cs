using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeColor1 : MonoBehaviour
{
    //TODO: Visualise selectedObject, select UI Elements
    public TMP_Text RedCanalText;
    public TMP_Text GreenCanalText;
    public TMP_Text BlueCanalText;
    
    private GameObject _selectedObject;
    private Color _selectedObjectColor;
    private Camera _mainCamera;
    private bool _isSceneObjectSelected = false;
    private bool _isCanvasObjectSelected = false;
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

        if (_isSceneObjectSelected || _isCanvasObjectSelected)
        {
            ObjectIsFocused();
        }
    }

    private void CanvasObjectLogic()
    {
        foreach (RaycastResult result in RaycastResults())
        {
            Debug.Log("Hit " + result.gameObject.name);
            _isSceneObjectSelected = false;
            _isCanvasObjectSelected = true;
            _selectedObject = result.gameObject;

            _selectedObjectColor = _selectedObject.GetComponent<Graphic>().color;
            RedCanalText.text = ((int)(_selectedObjectColor.r * 255.0f)).ToString();
            GreenCanalText.text = ((int)(_selectedObjectColor.g * 255.0f)).ToString();
            BlueCanalText.text = ((int)(_selectedObjectColor.b * 255.0f)).ToString();
            _selectedObjectColor.a = 1f;
        }
    }

    private void SceneObjectLogic()
    {
        RaycastHit hit = CastRayToScene();
        if (hit.collider != null)
        {
            _isSceneObjectSelected = true;
            _isCanvasObjectSelected = false;
            _selectedObject = hit.collider.gameObject;
            Debug.Log("Hit " + _selectedObject.name);

            _selectedObjectColor = _selectedObject.GetComponent<MeshRenderer>().material.color;
            RedCanalText.text = ((int)(_selectedObjectColor.r * 255.0f)).ToString();
            GreenCanalText.text = ((int)(_selectedObjectColor.g * 255.0f)).ToString();
            BlueCanalText.text = ((int)(_selectedObjectColor.b * 255.0f)).ToString();
            _selectedObjectColor.a = 1f;
        }
    }

    private void ObjectIsFocused()
    {
        if (_selectedObject != null)
        {
            if (_isCanvasObjectSelected)
            {
                _selectedObjectColor = new Color(
                    float.Parse(RedCanalText.text) / 255.0f,
                    float.Parse(GreenCanalText.text) / 255.0f,
                    float.Parse(BlueCanalText.text) / 255.0f,
                    1f);
                _selectedObject.GetComponent<Graphic>().color = _selectedObjectColor;
            }
            else if (_isSceneObjectSelected)
            {
                _selectedObjectColor = new Color(
                float.Parse(RedCanalText.text) / 255.0f,
                    float.Parse(GreenCanalText.text) / 255.0f,
                    float.Parse(BlueCanalText.text) / 255.0f,
                    1f);
                _selectedObject.GetComponent<MeshRenderer>().material.color = _selectedObjectColor;
            }
            else
            {
                _selectedObject = null;
            }
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
