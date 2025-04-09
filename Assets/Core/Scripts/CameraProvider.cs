using UnityEngine;

public class CameraProvider : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Vector3 _cameraSize;
    private Transform _targetT;
    
    public Camera Camera => _camera;

    private void Awake()
    {
        var height = Camera.orthographicSize * 2;
        var aspectRatio = (float)Screen.width / Screen.height;
        var width = height * aspectRatio;
        
        _cameraSize = new(width, height, 0f);
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    public void SetObjectToFollow(Transform targetT)
    {
        _targetT = targetT;
    }

    private void FollowTarget()
    {
        if (!_targetT || !_targetT.gameObject)
        {
            return;
        }
        
        var newPos = new Vector3(_targetT.position.x, _targetT.position.y, Camera.transform.position.z);
        Camera.transform.position = MonoContext.Instance.Field.ClampedMoveInFieldZone(newPos, _cameraSize);
    }
}
