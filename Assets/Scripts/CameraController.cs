﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public PlayerInput pInput;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 80.0f;
    public float cameraDampValue = 0.07f;//相机追赶的Damp时间值

    private float tmpEulerX;//相机的X轴的欧拉度数
    private float tmpEulerY;//相机的Y轴的欧拉度数
    private GameObject playerHandle;
    private GameObject cameraHandle;
    private GameObject model;
    private GameObject camera;

    private Vector3 cameraDampVelocity;

	void Awake () {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tmpEulerX = 20.0f;
        tmpEulerY = 0.0f;
        model = playerHandle.GetComponent<ActorController>().model;
        camera = Camera.main.gameObject;
	}
	
    //由于模型的移动大部分使用的是物理引擎，所以相机的跟随也在物理引擎中做
	void FixedUpdate () {
        Vector3 tmpModelEuler = model.transform.eulerAngles;//用于防止模型旋转的中间值

        playerHandle.transform.Rotate(Vector3.up, pInput.jRight * horizontalSpeed * Time.fixedDeltaTime);
        //使用减等于翻转方向
        tmpEulerX -= pInput.jUp * verticalSpeed * Time.fixedDeltaTime;
        //tmpEulerY += pInput.jRight * horizontalSpeed * Time.deltaTime;
        tmpEulerX = Mathf.Clamp(tmpEulerX, -40.0f, 40.0f);
        cameraHandle.transform.localEulerAngles = new Vector3(tmpEulerX, 0, 0);

        model.transform.eulerAngles = tmpModelEuler;

        //camera.transform.position = Vector3.SmoothDamp(camera.transform.position,
        //                                                                                 transform.position,
        //                                                                                 ref cameraDampVelocity,
        //                                                                                 cameraDampValue);
        camera.transform.position = transform.position;
        camera.transform.eulerAngles = transform.eulerAngles;
	}
}