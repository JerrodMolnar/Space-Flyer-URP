using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public static class CameraUtility
{
    [SerializeField] private static List<CinemachineVirtualCamera> _cameras = new List<CinemachineVirtualCamera>();
    private static CinemachineVirtualCamera _activeCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera cam)
    {
        return _activeCamera == cam;
    }

    public static void SwitchCamera(CinemachineVirtualCamera cam)
    {
        cam.Priority = 10;
        _activeCamera = cam;

        foreach (var c in _cameras)
        {
            if (c != _activeCamera)
            {
                c.Priority = 0;
            }
        }
    }

    public static void RegisterCamera(CinemachineVirtualCamera cam)
    {
        _cameras.Add(cam);
    }

    public static void UnregisterCamera(CinemachineVirtualCamera cam)
    {
        _cameras.Remove(cam);
    }
}
