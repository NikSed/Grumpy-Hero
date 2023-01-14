using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private BoxCollider2D _mapBounds;
    [SerializeField] private float smoothSpeed = 0.5f;

    private float xMin, xMax, yMin, yMax;
    private float camY, camX;
    private float camOrthsize;
    private float cameraRatio;

    private Camera _camera;
    private Vector3 smoothPos;

    private void Start()
    {
        xMin = _mapBounds.bounds.min.x;
        xMax = _mapBounds.bounds.max.x;
        yMin = _mapBounds.bounds.min.y;
        yMax = _mapBounds.bounds.max.y;
        _camera = GetComponent<Camera>();
        camOrthsize = _camera.orthographicSize;
        cameraRatio = (xMax + camOrthsize) / 1.7f;
    }

    private void Update()
    {
        camY = Mathf.Clamp(_target.position.y, yMin + camOrthsize, yMax - camOrthsize);
        camX = Mathf.Clamp(_target.position.x, xMin + cameraRatio, xMax - cameraRatio);
        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, this.transform.position.z), smoothSpeed);
        this.transform.position = smoothPos;
    }
}
