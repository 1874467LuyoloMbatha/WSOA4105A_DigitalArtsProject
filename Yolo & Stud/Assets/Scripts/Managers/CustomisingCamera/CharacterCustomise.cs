using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterCustomise : MonoBehaviour
{
    [Header("Unity Handles")]
    [SerializeField] CinemachineVirtualCamera customisingVCam;
    [SerializeField] Transform rotationTarget;
    [SerializeField] Quaternion newRot;
    [SerializeField] Vector3 zoomAmount, newZoomAmount, dragStartPos, dragCurrentPos, newPos;

    [Header("Floats")]
    [SerializeField] float rotationAmount = 1;
    [SerializeField] float speed = 1, forwardBackwardSpeed;
    [SerializeField] float zoomSpeed, zoomMin, zoomMax;

    [Header("Booleans")]
    [SerializeField] bool shiftKey;
    void Start()
    {
        if (customisingVCam == null)
            customisingVCam = GetComponent<CinemachineVirtualCamera>();

        if (rotationTarget == null)
            rotationTarget = customisingVCam.LookAt.gameObject.transform;

        if(PlayerPrefs.HasKey(zoomSpeed.ToString()))
			zoomSpeed = PlayerPrefs.GetFloat(zoomSpeed.ToString());

		newRot = rotationTarget.rotation;
    }


    void Update()
    {
        HandleInput();
    }
    void HandleInput()
    {
        if (GameManager.Instance.GetGameState() != GameManager.GameState.PlayerCustomiserMode)
            return;

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
			Debug.Log("Angle: " + Vector3.Angle(rotationTarget.forward, Vector3.forward));
		}
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Q))
        {
            dragStartPos = Input.mousePosition;
            rotationTarget.rotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
			Debug.Log("Angle: " + Vector3.Angle(rotationTarget.forward, Vector3.forward));
		}

        //Panning
        if (!shiftKey)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rotationTarget.localPosition += new Vector3(0, 1, 0) * speed;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                rotationTarget.localPosition += new Vector3(0, 1, 0) * -speed;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                ////transform.eulerAngles.y >= 0 && transform.eulerAngles <= 180
                //if (rotationTarget.eulerAngles.y >= 0 && rotationTarget.eulerAngles.y <= 90 ||
                //	rotationTarget.eulerAngles.y >= 90 && rotationTarget.eulerAngles.y <= 180)
                //	rotationTarget.position += new Vector3(1, 0, 0) * -speed;
                //else
                //	rotationTarget.position += new Vector3(0, 0, 1) * -speed;

                if (Vector3.Angle(rotationTarget.forward, Vector3.forward) <= 90.0 ||
		            Vector3.Angle(rotationTarget.forward, Vector3.forward) >= 160)
					rotationTarget.position += new Vector3(1, 0, 0) * -speed;
                else
					rotationTarget.position += Vector3.forward * speed;
			}
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (Vector3.Angle(rotationTarget.forward, Vector3.forward) <= 90.0 ||
					Vector3.Angle(rotationTarget.forward, Vector3.forward) >= 160)
					rotationTarget.position += new Vector3(1, 0, 0) * speed;
                else
					rotationTarget.position += Vector3.forward * -speed;
			}
        }
        //Move back and forward
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftKey = true;
            if (Input.GetKey(KeyCode.W))
            {
				if (Vector3.Angle(rotationTarget.forward, Vector3.forward) <= 90.0 ||
					Vector3.Angle(rotationTarget.forward, Vector3.forward) >= 160)
					rotationTarget.position += Vector3.forward * forwardBackwardSpeed;
                else
					rotationTarget.position += Vector3.back * -forwardBackwardSpeed;
			}
            if (Input.GetKey(KeyCode.S))
            {
				if (Vector3.Angle(rotationTarget.forward, Vector3.forward) <= 90.0 ||
						Vector3.Angle(rotationTarget.forward, Vector3.forward) >= 160)
					rotationTarget.position += Vector3.back * forwardBackwardSpeed;
                else
					rotationTarget.position += Vector3.forward * -forwardBackwardSpeed;
			}
		}
        else
            shiftKey = false;
         
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

    public void SetZoomSpeed(float v)
    {
        zoomSpeed = v;
        PlayerPrefs.SetFloat(zoomSpeed.ToString(), v);
    }

    public float GetZoomSpeed()
    {
        return zoomSpeed;
    }
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/