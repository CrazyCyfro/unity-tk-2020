using UnityEngine;
 

 
[RequireComponent(typeof(Camera))]
public class ThirdPersonCameraScript : MonoBehaviour {
 
    //public float moveSpeed;
    //public float shiftAdditionalSpeed;
    public float mouseSensitivity;
    public bool invertMouse;
    public bool autoLockCursor;
	
	public GameObject launcher;

 
    //private Camera cam;
 
    void Awake () {
        Cursor.lockState = (autoLockCursor)?CursorLockMode.Locked:CursorLockMode.None;
    }
   
    void Update () {
		this.gameObject.transform.RotateAround(launcher.transform.position, Vector3.up, Input.GetAxis("Mouse X"));
		this.gameObject.transform.RotateAround(launcher.transform.position, transform.right, Input.GetAxis("Mouse Y"));
		
		launcher.transform.Rotate(0,Input.GetAxis("Mouse X"),0);
		//is it bad practice to change rotation of launcher here?
		
		
        if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
 