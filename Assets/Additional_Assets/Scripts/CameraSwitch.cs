using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _cameras;
    [SerializeField] private float _waitTime = 5;
    [SerializeField] private PlayableDirector _cinematicSequence;
    [SerializeField] private GameObject _spaceShip;
    private float _lastButtonPressTime = float.PositiveInfinity;
    private bool _isCinematicActive = false;
    private bool _isStartSequenceFinished = false;
    public static bool _canPlayCinematic = true;
    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _mousePosition;

    public delegate void IntroFinished();
    public static event IntroFinished finished;

    private void Start()
    {
        if (_cameras.Length == 0)
        {
            Debug.LogError("No Virtual Cameras Applied");
        }
        if (_cinematicSequence == null)
        {
            Debug.LogError("No playable director found for Cinematic Sequence");
        }
        if (_spaceShip == null)
        {
            Debug.LogError("No Space Ship found on CameraSwitch");
        }
        _position = new Vector3(0, 1.422f, 200);
        if (_spaceShip != null)
        {
            _rotation = _spaceShip.transform.rotation;
        }
    }

    private void OnEnable()
    {
        foreach (var cam in _cameras)
        {
            CameraUtility.RegisterCamera(cam);
        }

        finished += CallForEndOfStartSequence;
    }

    private void OnDisable()
    {
        foreach (var cam in _cameras)
        {
            CameraUtility.UnregisterCamera(cam);
        }
        finished -= CallForEndOfStartSequence;
    }

    private void Update()
    {
        if (_isStartSequenceFinished)
        {
            if (Input.anyKeyDown || _mousePosition != Input.mousePosition)
            {
                if (_isCinematicActive)
                {
                    _cinematicSequence.Stop();
                    _isCinematicActive = false;
                    _spaceShip.transform.position = _position;
                    _spaceShip.transform.rotation = _rotation;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (CameraUtility.IsActiveCamera(_cameras[0]))
                    {
                        CameraUtility.SwitchCamera(_cameras[1]);
                    }
                    else
                    {
                        CameraUtility.SwitchCamera(_cameras[0]);
                    }
                }
                _lastButtonPressTime = Time.time + _waitTime;
            }
            else if (_lastButtonPressTime < Time.time && !_isCinematicActive && _canPlayCinematic)
            {
                _isCinematicActive = true;
                _cinematicSequence.Play();
            }
            _mousePosition = Input.mousePosition;
        }
    }

    public void CameraSignalSwitch(CinemachineVirtualCamera cam)
    {
        Debug.Log("Camera Signal");
        CameraUtility.SwitchCamera(cam);
        if (finished != null)
        {
            finished();
        }
    }

    public void CallForEndOfStartSequence()
    {
        _isStartSequenceFinished = true;
    }
}
