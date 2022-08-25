using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    //TODO: Visualise selectedObject, select UI Elements
    public TMP_Text RedCanalText;
    public TMP_Text GreenCanalText;
    public TMP_Text BlueCanalText;
    
    private GameObject selectedObject;
    private Color selectedObjectColor;
    private bool isSceneObjectSelected = false;
    private bool isCanvasObjectSelected = false;
    private GraphicRaycaster Raycaster;
    private PointerEventData PointerEventData;
    private EventSystem EventSystem;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneObjectHandler();
            CanvasObjectHandler();
        }

        if (isSceneObjectSelected || isCanvasObjectSelected)
        {
            ObjectInFocus();
        }
    }

    private void CanvasObjectHandler()
    {
        foreach (RaycastResult result in RaycastResults())
        {
            Debug.Log("Hit " + result.gameObject.name);
            isSceneObjectSelected = false;
            isCanvasObjectSelected = true;
            selectedObject = result.gameObject;

            selectedObjectColor = selectedObject.GetComponent<Graphic>().color;
            RedCanalText.text = ((int)(selectedObjectColor.r * 255.0f)).ToString();
            GreenCanalText.text = ((int)(selectedObjectColor.g * 255.0f)).ToString();
            BlueCanalText.text = ((int)(selectedObjectColor.b * 255.0f)).ToString();
            selectedObjectColor.a = 1f;
        }

    }

    private void SceneObjectHandler()
    {
        RaycastHit hit = CastRayToScene();
        if (hit.collider != null)
        {
            isSceneObjectSelected = true;
            isCanvasObjectSelected = false;
            selectedObject = hit.collider.gameObject;
            Debug.Log("Hit " + selectedObject.name);

            selectedObjectColor = selectedObject.GetComponent<MeshRenderer>().material.color;
            RedCanalText.text = ((int)(selectedObjectColor.r * 255.0f)).ToString();
            GreenCanalText.text = ((int)(selectedObjectColor.g * 255.0f)).ToString();
            BlueCanalText.text = ((int)(selectedObjectColor.b * 255.0f)).ToString();
            selectedObjectColor.a = 1f;
        }
    }

    void ObjectInFocus()
    {
        if (selectedObject != null)
        {
            if (isCanvasObjectSelected)
            {
                selectedObjectColor = new Color(
                    float.Parse(RedCanalText.text) / 255.0f,
                    float.Parse(GreenCanalText.text) / 255.0f,
                    float.Parse(BlueCanalText.text) / 255.0f,
                    1f);
                selectedObject.GetComponent<Graphic>().color = selectedObjectColor;
            }
            else if (isSceneObjectSelected)
            {
                selectedObjectColor = new Color(
                float.Parse(RedCanalText.text) / 255.0f,
                    float.Parse(GreenCanalText.text) / 255.0f,
                    float.Parse(BlueCanalText.text) / 255.0f,
                    1f);
                selectedObject.GetComponent<MeshRenderer>().material.color = selectedObjectColor;
            }
            else
            {
                selectedObject = null;
            }
        }
    }
    private List<RaycastResult> RaycastResults()
    {
        //Set up the new Pointer Event
        PointerEventData = new PointerEventData(EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        Raycaster.Raycast(PointerEventData, results);

        return results;
    }

    private RaycastHit CastRayToScene()
    {
        Vector3 screenMousePosFar = new(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
