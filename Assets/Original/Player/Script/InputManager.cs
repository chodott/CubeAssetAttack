using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private GraphicRaycaster _graphicRaycaster;
    [SerializeField] private EventSystem _eventSystem;

    [SerializeField] 
    private InputActionAsset inputActions;
    private InputAction pointerPositionAction;
    [SerializeField]
    private GameObject _buildListUI;

    private ISelectable curSelectable;

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
        if (!Input.GetMouseButtonDown(0)) return;

        ISelectable newSelectable;

        if(TryGetSelectableUI(out newSelectable))
        {
            Select(newSelectable);
        }

        else if(TryGetSelectableObj(out newSelectable))
        {
            Select(newSelectable);
        }

        else
        {
            ClearSelection();
        }
    }

    private void Select(ISelectable newSelectable)
    {
        if (newSelectable != curSelectable)
        {
            if (curSelectable != null)
            {
                curSelectable.OnDeselected();
            }
            curSelectable = newSelectable;
            curSelectable.OnSelected();
        }
    }

    private void ClearSelection()
    {
        if (curSelectable != null)
        {
            curSelectable.OnDeselected();
        }
        curSelectable = null;
    }

    private bool TryGetSelectableUI(out ISelectable newSelectable)
    {
        newSelectable = null;

        PointerEventData eventData = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(eventData, results);
        foreach (var result in results)
        {
            if (result.gameObject.TryGetComponent(out newSelectable))
            {
                return true;
            }
        }
        return false;
    }

    private bool TryGetSelectableObj(out ISelectable newSelectable)
    {
        newSelectable = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.collider.TryGetComponent(out newSelectable);
        }

        return false;
    }

    private void OnPointerClick(InputAction.CallbackContext context)
    {
    }
}
