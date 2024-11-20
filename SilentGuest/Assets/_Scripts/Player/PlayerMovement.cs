using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Player_Speed = 4f;
    public float MouseSens = 1.5f;
    public GameObject interactionPanel;
    private float CameraUpDownMov = 0;
    private CharacterController Character;

    void Start()
    {
        Character = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float forwardMovement = Input.GetAxis("Vertical") * Player_Speed;
        float sideMovement = Input.GetAxis("Horizontal") * Player_Speed;
        Vector3 movement = ((transform.forward * forwardMovement) + (transform.right * sideMovement));

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
                interactionPanel.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E)){
                    obj.typeEvent();
                }
            }
        }
        else{
            interactionPanel.SetActive(false);
        }
    }

    public void DisableMovement()
    {
        enabled = false;
    }
}