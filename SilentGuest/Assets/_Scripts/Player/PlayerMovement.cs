using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    
    public float Player_Speed = 4f;
    public float MouseSens = 1.5f;
    private float CameraUpDownMov = 0;
    private CharacterController Character;
    private Vector3 movement = Vector3.zero;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    void Start()
    {
        Character = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        float verticalMov = Input.GetAxis("Vertical") * Player_Speed;
        float horizontalMov = Input.GetAxis("Horizontal") * Player_Speed;
        float movementDirectionY = movement.y;
        movement = (forward * verticalMov) + (right * horizontalMov);

        movement.y = movementDirectionY;

        if (!Character.isGrounded)
        {
            movement.y -= 10f * Time.deltaTime;
        }

        Character.Move(movement * Time.deltaTime);

        CameraUpDownMov += -(Input.GetAxis("Mouse Y") * MouseSens);
        CameraUpDownMov = Mathf.Clamp(CameraUpDownMov, -75f, 75f);

        Camera.main.transform.localRotation = Quaternion.Euler(CameraUpDownMov, 0, 0);

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * MouseSens);

        Interactions();
    }

    void Interactions()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if(Physics.Raycast(ray, out RaycastHit raycast, 2f)){
            if(raycast.collider.gameObject.TryGetComponent(out InteractableEvent obj)){
                InteractionPromptManager.instance.SetInteractionPrompt(obj.GetInteractionPrompt());
                if(Input.GetKeyDown(KeyCode.E)){
                    obj.typeEvent();
                }
            }
        }
        else{
            InteractionPromptManager.instance.DeactivateInteractionPrompt();
        }
    }

    public void DisableMovement()
    {
        enabled = false;
    }
}