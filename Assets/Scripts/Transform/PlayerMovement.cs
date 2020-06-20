using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float BankingValue = 90f;
    public Transform VisualChild;

    private Camera _Cam;
    private Rigidbody _Body;
    private float _DistanceToCam;
    private Vector3 _Velocity;
    private Vector3 _LastPosition;
    private Vector3 _Rotation;
    private Vector3 _TouchPosition;
    private Vector3 _ScreenToWorld;

    void Start()
    {
        _Cam = Camera.main;
        _Body = GetComponent<Rigidbody>();
        _DistanceToCam = (_Cam.transform.position - transform.position).magnitude;
    }

    void FixedUpdate()
    {
        _Velocity = transform.position - _LastPosition;

        _Move();

        _LastPosition = transform.position;
    }

    private void _Move()
	{
        _TouchPosition = Input.mousePosition;
        _TouchPosition.z = _DistanceToCam;
        _ScreenToWorld = _Cam.ScreenToWorldPoint(_TouchPosition);
        var toGoalPoint = _ScreenToWorld - transform.position;
        var rightAxis = toGoalPoint.x > 0f ? 1f : toGoalPoint.x < 0f ? -1f : 0f;

        var movement = Vector3.Lerp(_Body.position, _ScreenToWorld, Speed * Time.fixedDeltaTime);
        _Body.MovePosition(movement);

        _Rotation.z = -rightAxis * Mathf.Clamp01(toGoalPoint.magnitude) * BankingValue;
        _Body.MoveRotation(Quaternion.Euler(_Rotation));
    }
}
