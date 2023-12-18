using Cinemachine;
using UnityEngine;

public class MapCamController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float moveSpeed = 5.0f;
    public float zoomRate = 1.0f;

    Camera mainCam;
    float FOVvalue;
    private void Start()
    {
        
        
        mainCam = Camera.main;
    }
    void Update()
    {
        /*float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");

        freeLookCamera.m_XAxis.Value += xAxisValue;*/
       
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
       
        
        if (scrollWheelInput > 0f || Input.GetKey(KeyCode.S)) // Scrolling up
        {
            Debug.Log("up");
            ChangeFOV(zoomRate);

        }
        else if (scrollWheelInput < 0f || Input.GetKey(KeyCode.W)) // Scrolling down
        {
            Debug.Log("Down");
            ChangeFOV(-zoomRate);

        }
    }
    
    private void ChangeFOV(float value)
    {
        Debug.Log(value);
        
        if (freeLookCamera.m_Lens.FieldOfView >= 60)
        {
            freeLookCamera.m_Lens.FieldOfView = 59f;
        }
        else if (freeLookCamera.m_Lens.FieldOfView <= 20)
        {
            freeLookCamera.m_Lens.FieldOfView = 21f;
        }
        else if ( freeLookCamera.m_Lens.FieldOfView > 20 && freeLookCamera.m_Lens.FieldOfView < 60 )
        {
            freeLookCamera.m_Lens.FieldOfView += value;
        }
       
    }
}


