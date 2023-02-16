using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform thePlayer, spawnPoint;

    private void Start()
    {
        virtualCamera= GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (thePlayer == null)
        {
            virtualCamera.m_Follow = spawnPoint;
        }
        else
        {
            virtualCamera.m_Follow = thePlayer;
        }
    }
}
