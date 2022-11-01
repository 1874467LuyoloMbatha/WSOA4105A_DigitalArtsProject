using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterCustomise : MonoBehaviour
{
    [Header("Unity Handles")]
    [SerializeField] CinemachineVirtualCamera customisingVCam;
    [SerializeField] Transform rotationTarget;
    [SerializeField] Quaternion newRot;
    [SerializeField] Vector3 zoomAmount, newZoomAmount, dragStartPos, dragCurrentPos, newPos;

    [Header("Floats")]
    [SerializeField] float rotationAmount = 1;
    [SerializeField] float speed = 1;
    [SerializeField] float zoomSpeed, zoomMin, zoomMax;

    void Start()
    {
        if (customisingVCam == null)
            customisingVCam = GetComponent<CinemachineVirtualCamera>();

        if (rotationTarget == null)
            rotationTarget = customisingVCam.LookAt.gameObject.transform;
        newRot = rotationTarget.rotation;
    }


    void Update()
    {
        HandleInput();
    }
    void HandleInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float fov = customisingVCam.m_Lens.FieldOfView;
            float tar = Mathf.Clamp(fov + -Input.mouseScrollDelta.y, zoomMin, zoomMax);

            customisingVCam.m_Lens.FieldOfView = Mathf.Lerp(fov, tar, zoomSpeed * Time.deltaTime);
            //  newZoomAmount += Input.mouseScrollDelta.y * zoomAmount;
        }

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.E))
        {
            dragStartPos = Input.mousePosition;
            rotationTarget.rotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Q))
        {
            dragStartPos = Input.mousePosition;
            rotationTarget.rotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        //Panning
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
            rotationTarget.position += Vector3.up * speed;
		}
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rotationTarget.position += Vector3.up * -speed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rotationTarget.position += Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rotationTarget.position += Vector3.left * -speed;
        }
        /*  if (Input.GetMouseButton(0))
          {
              dragCurrentPos = Input.mousePosition;
              Vector3 diff = dragStartPos - dragCurrentPos;

              dragStartPos = dragCurrentPos;

              //  rotationAmount += diff.x;

              newRot *= Quaternion.Euler(Vector3.up * (-diff.x / 5f));
              rotationTarget.rotation = newRot;
          }*/
        //  newRot *= Quaternion.Euler(Vector3.up * rotationAmount);
        // rotationTarget.rotation = Quaternion.Lerp(rotationTarget.rotation, newRot, Time.deltaTime * speed);
    }

}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/