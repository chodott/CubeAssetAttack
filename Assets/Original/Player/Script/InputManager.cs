using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] 
    private InputActionAsset inputActions;
    private InputAction pointerPositionAction;
    [SerializeField]
    private GameObject _buildListUI;

    int buildLayerMask;

    protected void Start()
    {
        // Action Map과 Action을 가져옴
        var playerInputMap = inputActions.FindActionMap("Player");
        pointerPositionAction = playerInputMap.FindAction("Touch");
        buildLayerMask = LayerMask.GetMask("InteractObject");

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

    private void Update()
    {
    }

    private void OnPointerClick(InputAction.CallbackContext context)
    {
        //First Check UI Click
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //After Check BuildPlatform Click
        Vector2 screenPosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, buildLayerMask))
        {
            GameObject clickedObject = hit.collider.gameObject;

            switch(clickedObject.tag)
            {
                case "BuildPlatform":
                    BuildManager.Instance.BuildPlatformTransform = hit.transform;
                    _buildListUI.SetActive(true);
                    break;

                case "UI":
                    break;
            }   
        }
        else _buildListUI.SetActive(false);
    }
}
