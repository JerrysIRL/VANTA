using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MultipeTargetCamera : MonoBehaviour
{
    [Header("Tunable variables")]
    [Range(0, 1)] [SerializeField] private float smoothTime;
    [SerializeField] private float minZoom = 30f; 
    [SerializeField] private float maxZoom = 100f;
    [SerializeField] private float zoomLimiter = 30f;
    [SerializeField] Vector3 cameraOffset;

    public Transform[] targets = new Transform[2];
    private Vector3 _velocity;
    private Camera _cam;
    private Bounds _bounds;
    
    private void Start()
    {
        _cam = Camera.main;
    }

    void LateUpdate()
    {
        _bounds = EncapsulateTargets();
        MoveCamera();
        Zoom();
    }

    private void MoveCamera()
    {
        Vector3 centerPosition = GetBoundsCenterPosition(_bounds);
        Vector3 newPos = centerPosition + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref _velocity, smoothTime);
    }
    
    float GetGreatestDistance() => (_bounds.size.x > _bounds.size.z) ? _bounds.size.x : _bounds.size.z;

    /// <summary>
    /// Adjust the cameras FOV based on the boundingBOx 
    /// </summary>
    void Zoom()
    {
        float distance = GetGreatestDistance();
        float newZoom = Mathf.Lerp(maxZoom, minZoom, distance / zoomLimiter);
        _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, newZoom, Time.deltaTime);
    }

   
    /// <returns> Bounding box containing both players</returns>
    private Bounds EncapsulateTargets()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds;
    }

    private Vector3 GetBoundsCenterPosition(Bounds bounds)
    {
        if (targets.Length == 1)
        {
            return targets[0].position;
        }

        return bounds.center;
    }
}