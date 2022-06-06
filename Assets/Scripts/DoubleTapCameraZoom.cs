using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DoubleTapCameraZoom : MonoBehaviour
{
    bool zoomed = false;
    [SerializeField] float MaxTimeWait = 1;
    [SerializeField] float VariancePosition = 1;
    CinemachineVirtualCamera cam;
    [SerializeField] float zoomMultiplier = 2;
    float defZoom, newZoom, dif;
    bool isZooming = false;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        defZoom = cam.m_Lens.OrthographicSize;
        newZoom = defZoom * zoomMultiplier;
        dif = newZoom - defZoom;
    }

    //private void Update()
    //{
    //    if (IsDoubleTap())
    //    {
    //        ToggleZoom();
    //    }
    //}
    bool IsDoubleTap()
    {
        bool result = false;

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            float DeltaTime = Input.GetTouch(0).deltaTime;
            float DeltaPositionLenght = Input.GetTouch(0).deltaPosition.magnitude;

            if (DeltaTime > 0 && DeltaTime < MaxTimeWait && DeltaPositionLenght < VariancePosition)
                result = true;
        }
        return result;
    }

    public void ToggleZoom()
    {
        if (isZooming) { return; }
        zoomed = !zoomed;
        switch (zoomed)
        {
            case true:
                StartCoroutine(SetZoom(dif));
                break;
            case false:
                StartCoroutine(SetZoom(-dif));
                break;
        }
    }

    IEnumerator SetZoom(float _p)
    {
        isZooming = true;
        for (int i = 0; i < 50; i++)
        {
            cam.m_Lens.OrthographicSize += .02f * _p;
            yield return null;
        }
        isZooming = false;
    }
}
