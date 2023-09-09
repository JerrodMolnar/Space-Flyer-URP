using UnityEngine;

public class ShipControls : MonoBehaviour
{
    [SerializeField] private float _hRotSpeed = 10f;
    [SerializeField] private float _zRotSpeed = 0.2f;
    [SerializeField] private float _vRotSpeed = 10f;
    [SerializeField] private float _currentSpeed;
    private float _vertical;
    private float _horizontal;
    private bool _introSequenceFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        _currentSpeed = 0;
    }

    private void OnEnable()
    {
        CameraSwitch.finished += CallForEndOfStartSequence;
    }

    // Update is called once per frame
    void Update()
    {        
        if (_introSequenceFinished)
        {
            ShipMovement();
        }
    }

    private void OnDisable()
    {
        CameraSwitch.finished -= CallForEndOfStartSequence;
    }

    private void ShipMovement()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.T))
        {
            _currentSpeed++;
            if (_currentSpeed > 10)
            {
                _currentSpeed = 10;
            }
        }//increase speed

        if (Input.GetKeyDown(KeyCode.G))
        {
            _currentSpeed--;
            if (_currentSpeed < 1)
            {
                _currentSpeed = 1;
            }
        }//decrease speed

        Vector3 rotateH = new Vector3(0, _horizontal, 0);
        transform.Rotate(rotateH * _hRotSpeed * Time.deltaTime);

        Vector3 rotateV = new Vector3(_vertical, 0, 0);
        transform.Rotate(rotateV * _vRotSpeed * Time.deltaTime);

        transform.Rotate(new Vector3(0, 0, -_horizontal * _zRotSpeed), Space.Self);

        transform.position += transform.forward * _currentSpeed * Time.deltaTime;
    }

    public void CallForEndOfStartSequence()
    {
        _introSequenceFinished = true;
    }

    public void LowerSpeed()
    {
        _currentSpeed = 1;
    }

    public void ZeroSpeed()
    {
        _introSequenceFinished = false;
        _currentSpeed = 0;
    }
}
