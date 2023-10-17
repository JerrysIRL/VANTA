using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public abstract class Movement : MonoBehaviour
{
    [Header("Movement tunable properties")] 
    [SerializeField] private float topSpeed = 10f;
    [SerializeField] private float distanceThreshold = 25f;
    
    [SerializeField] private int playerIndex;

    [Range(0, 1)] public float moveSmoothTime = 0.25f;
    [Range(0, 1)] public float turnSmoothTime = 0.3f;

    protected CharacterController Controller;
    protected Animator Animator;
    protected Vector3 Direction;
    
    private Vector3 _currentMoveVelocity;   
    private Vector3 _moveDampVelocity;
    private Vector3 _moveVectors;
    
    private float _turnSmoothVelocity;
    private float _ySpeed;
    private float _currentTopSpeed;

    void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        _moveVectors = Vector3.zero;
        SetTopSpeed();
    }

    public void SetMoveVector(Vector2 vec) 
    {
        _moveVectors = new Vector3(vec.x,0, vec.y);
    }

    protected Vector3 GetMoveVector() => _moveVectors;

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    protected void SetYSpeed(float ySpeed)
    {
        _ySpeed = ySpeed;
    }

    protected void SetNewTopSpeed(float speed)
    {
        _currentTopSpeed = Mathf.Lerp(_currentTopSpeed, speed, Time.deltaTime * 10);
    }

    protected Vector3 CalculateNextPos()
    {
        Vector3 newPos = transform.position + Direction;
        return newPos;
    }

    /// <summary>
    /// Checks the distance between players is withing the threshold.
    /// </summary>
    /// <returns> 'True' if distance is greater or equals the threshold</returns>
    protected bool CheckDistance(Vector3 otherPlayer)
    {
        Vector3 newPos = CalculateNextPos();
        float dist = Vector3.Distance(newPos, otherPlayer);

        if (dist >= distanceThreshold)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets top speed to initial value
    /// </summary>
    /// <returns></returns>
    protected void SetTopSpeed()
    {
        _currentTopSpeed = Mathf.Lerp(_currentTopSpeed, topSpeed, Time.deltaTime * 10);
    }

    protected void Move(Vector3 otherPlayer)
    {
        Vector3 clampedInput = Vector3.ClampMagnitude(_moveVectors, 1);
        Direction = clampedInput.normalized;
        
        if (clampedInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0f);
        }
        if (CheckDistance(otherPlayer))
        {
            return;
        }

        _currentMoveVelocity = Vector3.SmoothDamp(_currentMoveVelocity, clampedInput * _currentTopSpeed, ref _moveDampVelocity, moveSmoothTime);
        _currentMoveVelocity.y = _ySpeed;
        
        Controller.Move(_currentMoveVelocity * Time.deltaTime);
    }
}