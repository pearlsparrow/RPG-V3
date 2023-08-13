using Animancer;
using UnityEngine;


[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(CharacterController))]
public class MainPlayer : MonoBehaviour
{
    [Header("Settings")] [SerializeField] private float speed;
    [SerializeField] private float rotationSmoothTime;
    [SerializeField] private float smoothAcceleration = 0.2f;
    [SerializeField] private float acceleration;
    [SerializeField] private float deAccelaration;
    [SerializeField] private float jumpHeigh = 3f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpFallGravity = 14.00f;
    [SerializeField] private float jumpFallModifier = 1.5f;
    [SerializeField] private float downForce = 0.8f;
    [SerializeField] private float waterDownForce = 0.05f;

    //[SerializeField] float nearGroundValue = 0.4f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private AudioClip[] footSteps;


    [Header("Weapons")] public GameObject rightHandEquipedSlot;
    public GameObject leftHandEquipedSlot;
    public GameObject rightHandUnequipedSlot;
    public GameObject leftHandUnequipedSlot;

    public GameObject rightHandItem;
    public GameObject leftHandItem;

    private float accelarationClamp = 1f;
    private InputHandler inputHandler;
    private CharacterController characterController;
    private StateMachine stateMachine;
    private StateFactory factory;
    private Animator animator;
    public bool isGrounded = true;
    private Vector3 velocity;
    private Vector3 currentPos;
    private Vector3 previousPos;
    private Vector3 climbDirection;
    private bool isNearGround;
    private float targetAngle;
    private TrackWalls trackWalls;
    private float maxSpeed;

    private float currentAccelaration;
    private float currentAngle;
    private float currentAngleVelocity;
    private float directionY;
    Vector3 rotatedMovement;
    private float currentGravity;
    private bool attackEnded = false;
    float smoothVelocityAcceleration;
    private bool nextAttack = false;
    private bool canMove = true;
    private bool canClimb = false;
    private bool canGoBackToGround = false;
    private bool equiped = false;
    private Vector3 lastDirection;
    private float lastAcceleration;

    private AnimancerComponent animancer;
    [Header("Animations")] public Float1ControllerTransition blendEquiped;
    public Float1ControllerTransition blendUnequiped;
    public Float1ControllerTransition blendShield;
    Float1ControllerState state;


    public ClipTransition jump;
    public ClipTransition inAir;
    public ClipTransition landing;
    public ClipTransition jump_u;
    public ClipTransition inAir_u;
    public ClipTransition landing_u;
    public ClipTransition[] attacks;
    public ClipTransition attack1;
    public ClipTransition attack2;
    public ClipTransition attack3;
    public ClipTransition equip;
    public ClipTransition unequip;
    public ClipTransition BlockIn;
    public ClipTransition BlockOut;
    public ClipTransition roll;


    public StateMachine StateMachine
    {
        get => stateMachine;
        set => stateMachine = value;
    }

    public StateFactory Factory
    {
        get => factory;
        set => factory = value;
    }

    public Animator Animator
    {
        get => animator;
        set => animator = value;
    }

    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }

    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    public bool IsNearGround
    {
        get => isNearGround;
        set => isNearGround = value;
    }

    public InputHandler InputHandler
    {
        get => inputHandler;
        set => inputHandler = value;
    }

    public float DirectionY
    {
        get => directionY;
        set => directionY = value;
    }

    public float DownForce
    {
        get => downForce;
        set => downForce = value;
    }

    public float WaterDownForce
    {
        get => waterDownForce;
        set => waterDownForce = value;
    }

    public float CurrentGravity
    {
        get => currentGravity;
        set => currentGravity = value;
    }

    public float Gravity
    {
        get => gravity;
        set => gravity = value;
    }

    public float JumpFallModifier
    {
        get => jumpFallModifier;
        set => jumpFallModifier = value;
    }

    public float JumpHeigh
    {
        get => jumpHeigh;
        set => jumpHeigh = value;
    }

    public CharacterController CharacterController
    {
        get => characterController;
        set => characterController = value;
    }

    public Vector3 RotatedMovement
    {
        get => rotatedMovement;
        set => rotatedMovement = value;
    }

    public float AccelarationClamp
    {
        get => accelarationClamp;
        set => accelarationClamp = value;
    }

    public float CurrentAccelaration
    {
        get => currentAccelaration;
        set => currentAccelaration = value;
    }

    public float JumpFallGravity
    {
        get => jumpFallGravity;
        set => jumpFallGravity = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public bool AttackEnded1
    {
        get => attackEnded;
        set => attackEnded = value;
    }

    public bool NextAttack
    {
        get => nextAttack;
        set => nextAttack = value;
    }

    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    public bool CanClimb
    {
        get => canClimb;
        set => canClimb = value;
    }

    public TrackWalls TrackWalls
    {
        get => trackWalls;
        set => trackWalls = value;
    }

    public bool CanGoBackToGround
    {
        get => canGoBackToGround;
        set => canGoBackToGround = value;
    }

    public AnimancerComponent Animancer
    {
        get => animancer;
        set => animancer = value;
    }

    public bool Equiped
    {
        get => equiped;
        set => equiped = value;
    }

    public Float1ControllerState State
    {
        get => state;
        set => state = value;
    }

    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = value;
    }

    public Vector3 LastDirection
    {
        get => lastDirection;
        set => lastDirection = value;
    }

    public float LastAcceleration
    {
        get => lastAcceleration;
        set => lastAcceleration = value;
    }

    public float CurrentAngle
    {
        get => currentAngle;
        set => currentAngle = value;
    }


    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Init State machine
        factory = new StateFactory(this);
        stateMachine = new StateMachine(this);

        //Init components
        inputHandler = GetComponent<InputHandler>();
        CharacterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        trackWalls = GetComponent<TrackWalls>();
        animancer = GetComponent<AnimancerComponent>();
        maxSpeed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        StateMachine.currentState.OnStatesEnter();

        DirectionY = DownForce;
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.currentState.OnStatesUpdate();
        HandleAccelaration();
        if (CanMove)
        {
            DirectionInput();
            MovementHandler();
            DirectionYHandler();
        }
        else if (canClimb)
        {
            Climb();
        }

        AnimationUpdate();
        isGrounded = CharacterController.isGrounded;
    }

    public void MovementHandler()
    {
        //Velocity
        currentPos = transform.position;
        velocity = (currentPos - previousPos) / Time.deltaTime;

        //Movement
        CharacterController.Move(RotatedMovement * (Speed * CurrentAccelaration * Time.deltaTime));

        previousPos = currentPos;
    }

    public void DirectionYHandler()
    {
        CharacterController.Move(new Vector3(0f, DirectionY, 0f) * Time.deltaTime);
    }

    private void AnimationUpdate()
    {
        state.Parameter = currentAccelaration;
    }

    float xAgle;
    float zAgle;
    float refVelocityx;
    float refVelocityz;

    private void DirectionInput()
    {
        if (inputHandler.RawInput.magnitude > 0f)
        {
            targetAngle = Mathf.Atan2(InputHandler.RawInput.x, InputHandler.RawInput.z) * Mathf.Rad2Deg +
                          Camera.main.transform.eulerAngles.y;
            currentAngle =
                Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            xAgle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.x, 0, ref refVelocityx, 0.1f);
            zAgle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, 0, ref refVelocityz, 0.1f);
            transform.rotation = Quaternion.Euler(xAgle, currentAngle, zAgle);

            RotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            //RotatedMovement = Vector3.ClampMagnitude(RotatedMovement, 1);
            lastDirection = rotatedMovement;
        }
    }

    private void HandleAccelaration()
    {
        currentAccelaration = Mathf.SmoothDamp(currentAccelaration, inputHandler.RawInput.magnitude * accelarationClamp,
            ref smoothVelocityAcceleration, smoothAcceleration);


        if (currentAccelaration <= 0.001f && inputHandler.RawInput.magnitude <= 0f)
        {
            currentAccelaration = 0f;
        }

        lastAcceleration = currentAccelaration;
    }

    public void AttackEnded()
    {
        //nextAttack = false;
        //AttackEnded1 = true;
    }

    Vector3 climpRef;
    Vector3 climpPosRef;

    public void Climb()
    {
        Vector3 DirectionUp = Vector3.Cross(trackWalls.HitMid.normal, -transform.right);
        Vector3 DirectionRight = Vector3.Cross(trackWalls.HitMid.normal, transform.up);

        //transform.forward = Vector3.SmoothDamp(transform.forward, -trackWalls.HitMid.normal, ref climpRef, 0.1f);
        //transform.position = Vector3.SmoothDamp(transform.position, trackWalls.HitMid.point + trackWalls.HitMid.normal * 3f,ref climpPosRef , 0.1f);

        characterController.Move(DirectionRight *
                                 (inputHandler.RawInput.x * speed * CurrentAccelaration * Time.deltaTime));
        characterController.Move(DirectionUp *
                                 (inputHandler.RawInput.z * speed * CurrentAccelaration * Time.deltaTime));
    }

    public Vector3 CalculateAngledDirection()
    {
        float target = Mathf.Atan2(InputHandler.RawInput.x, InputHandler.RawInput.z) * Mathf.Rad2Deg +
                       Camera.main.transform.eulerAngles.y;
        Vector3 direction = Quaternion.Euler(0, target, 0) * Vector3.forward;
        return Vector3.ClampMagnitude(direction, 1);
    }
}