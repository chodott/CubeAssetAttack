using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] 
    private InputActionAsset inputActions;
    private InputAction pointerPositionAction;
    [SerializeField]
    private GameObject _buildListUI;

    private int _buildLayerMask;
    private bool _OnMouseClick = false;

    public UnityEvent<Vector2> ClickUI;

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        // Action Map과 Action을 가져옴
        var playerInputMap = inputActions.FindActionMap("Player");
        pointerPositionAction = playerInputMap.FindAction("Touch");
        _buildLayerMask = LayerMask.GetMask("BuildPlatform");

        // 입력 활성화
        pointerPositionAction.Enable();

        // 클릭 이벤트 구독
        pointerPositionAction.performed += OnPointerClick;
    }

    private void OnDisable()
    {
        // 입력 비활성화
        pointerPositionAction.Disable();

        pointerPositionAction.performed -= OnPointerClick;
    }

    protected void LateUpdate()
    {
        if (_OnMouseClick == false) return;
        _OnMouseClick = false;
        //First Check UI Click
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        if (EventSystem.current.IsPointerOverGameObject())
        {
            ClickUI.Invoke(screenPosition);
            return;
        }

        else
        {
            //After Check BuildPlatform Click
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _buildLayerMask))
            {
                GameObject clickedObject = hit.collider.gameObject;
                SpawnManager.Instance.BuildPlatformTransform = hit.transform;
                SpawnManager.Instance.Build();
            }
        }

    }

    private void OnPointerClick(InputAction.CallbackContext context)
    {
        _OnMouseClick = true;
    }
}
