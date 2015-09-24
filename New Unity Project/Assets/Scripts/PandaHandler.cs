using UnityEngine;
using System.Collections;

public class PandaHandler : MonoBehaviour
{
    const int GAME_STATE_WAITING = 0;
    const int GAME_STATE_PLAYING = 1;
    const int GAME_STATE_ENDED = 2;
    const string PANDA_STATE = "state";

    Animator animator;

    [Header("Panda Settings")]
    public float MovementSpeed = 1f;
    [Tooltip("Time between increasing the speed")]
    public float
        increaseInterval = 4f;
    public float IncreaseBy = 0.5f;
    public float Angle = 90f;
    public float MaxSpeed = 60f;


    //  private RunLeftManager.GameState gameState = RunLeftManager.GameState.Waiting;
    private bool _turnLeft = false;
    private float _tempMovementSpeed = 0f;
    private float _targetSpeed = 0f;



    private RunLeftManager.GameState GameState
    {
        get
        {
            return RunLeftManager.Instance.State;
        }
        set
        {

            RunLeftManager.Instance.State = value;
        }
    }


    float time = 0f;
    void Awake()
    {
        //  RunLeftManager.Instance.CleanUp();
        animator = GetComponent<Animator>();
        _targetSpeed = MovementSpeed;
    }
    // Use this for initialization
    void Start()
    {
        
        OnStart();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameState == RunLeftManager.GameState.Waiting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameState = RunLeftManager.GameState.Playing;
                OnPlaying();
            }
        }
        HandleInput();


        Movement();
    }
    void HandleInput()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            _turnLeft = true;
        }
        else
        {
            _turnLeft = false;
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            _turnLeft = true;
        }
        else
        {
            _turnLeft = false;
        }
#else
  if (Input.GetMouseButton(0))
        {
            _turnLeft = true;
        }
        else
        {
            _turnLeft = false;
        }
#endif

    }
    void Movement()
    {
        if (GameState != RunLeftManager.GameState.Playing)
            return;
        _tempMovementSpeed = _turnLeft ? MovementSpeed : 0f;

        transform.Translate(Vector3.up * MovementSpeed * Time.smoothDeltaTime);
        transform.Rotate(Vector3.forward, Time.deltaTime * Angle * _tempMovementSpeed);
        time += Time.deltaTime;


        if (time > increaseInterval)
        {
            time = 0f;
            _targetSpeed += IncreaseBy;
        }


        if (MovementSpeed < _targetSpeed)
        {
            MovementSpeed += Time.smoothDeltaTime;

            MovementSpeed = Mathf.Min(MovementSpeed, _targetSpeed);
        }
        MovementSpeed = Mathf.Min(MaxSpeed, MovementSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "InnerCircle")
        {
            print("GameOver " + other.tag);
            GameOver();
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "OuterCircle")
        {
            print("GameOver " + other.tag);
            GameOver();
        }
    }

    private void GameOver()
    {
        GameState = RunLeftManager.GameState.Ended;
        NavigationUtil.ShowGameOverMenu();
        OnEnd();
        //ScoreText.StopCounting();
    }

    void OnStart()
    {
        GameState = RunLeftManager.GameState.Waiting;
        animator.SetInteger(PANDA_STATE, GAME_STATE_WAITING);
    }
    void OnPlaying()
    {
        animator.SetInteger(PANDA_STATE, GAME_STATE_PLAYING);
    }
    void OnEnd()
    {
        animator.SetInteger(PANDA_STATE, GAME_STATE_ENDED);

    }

}
