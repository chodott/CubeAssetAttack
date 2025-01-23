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
        // Action Map�� Action�� ������
        var playerInputMap = inputActions.FindActionMap("Player");
        pointerPositionAction = playerInputMap.FindAction("Touch");

        // �Է� Ȱ��ȭ
        pointerPositionAction.Enable();

        // Ŭ�� �̺�Ʈ ����
        pointerPositionAction.performed += OnPointerClick;
    }

    private void OnDisable()
    {
        // �Է� ��Ȱ��ȭ
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
