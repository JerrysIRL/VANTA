using UnityEngine;

public class DarkMovement : Movement
{
    [Header("Dark properties")]
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravity = -5f;
    [SerializeField] private float inAirTopSpeed;
    private GameObject _otherPlayer;
    
    private bool _moving;
    private float _ySpeedDark = 0;
    
    private static readonly int JumpBool = Animator.StringToHash("JumpBool");
    private static readonly int JumpTrigger = Animator.StringToHash("JumpTrigger");

    private void Awake()
    {
        _otherPlayer = FindObjectOfType<LightMovement>().gameObject;
    }

    void Update()
    {
        ControlTopSpeed();
        ApplyGravity();
        SetYSpeed(_ySpeedDark);
        Move(_otherPlayer.transform.position);
        SetAudioBool();
    }
   

    public void Jump()
    {
        if (Controller.isGrounded)
        {
            Animator.SetTrigger(JumpTrigger);
            _ySpeedDark = jumpHeight;
        }
        
    }
    
    public bool GetMovingBool() => _moving;


    private void ControlTopSpeed()
    {
        if(Controller.isGrounded)
        {
            Animator.SetBool(JumpBool, false);
            SetTopSpeed();
        }
        else
        {
            SetNewTopSpeed(inAirTopSpeed);
            Animator.SetBool(JumpBool, true);
        }
        Animator.SetFloat("CurrentSpeed", Controller.velocity.magnitude);
    }

    private void ApplyGravity()
    {
        if (!Controller.isGrounded)
        {
            _ySpeedDark += gravity * Time.deltaTime;
        }
    }
    
    private void SetAudioBool()
    {
        if (GetMoveVector().magnitude > 0.1)
        {
            _moving = true;
        }
        else
        {
            _moving = false;
        }
    }
}