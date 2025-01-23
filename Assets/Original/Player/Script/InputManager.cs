using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] 
    private InputActionAsset inputActions;
    private InputAction pointerPositionAction;
    [SerializeField]
    private GameObject _buildListUI;

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

    private void Update()
    {
    }

    private void OnPointerClick(InputAction.CallbackContext context)
    {

        Vector2 screenPosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject clickedObject = hit.collider.gameObject;

            switch(clickedObject.tag)
            {
                case "BuildPlatform":
                    BuildManager.Instance.buildTransform = hit.transform;
                    _buildListUI.SetActive(true);
                    break;

                case "UI":
                    break;

                case "Default":
                    _buildListUI.SetActive(false);
                    break;

            }   
        }
        else _buildListUI.SetActive(false);
    }
}
