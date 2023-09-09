using UnityEngine;
using UnityEngine.Playables;

public class Trigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector _triggerDirector;
    [SerializeField] private MeshRenderer _meshRenderer;
    private bool _hasPlayed = false;

    private void Start()
    {
        if (_meshRenderer != null)
        {
            _meshRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggerDirector != null && other.CompareTag("Player") && !_hasPlayed)
        {
            if (_meshRenderer != null)
            {
                _meshRenderer.enabled = true;
            }
            _triggerDirector.Play();
            CameraSwitch._canPlayCinematic = false;
            _hasPlayed = true;
            other.GetComponent<ShipControls>().LowerSpeed();
        }

    }
}
