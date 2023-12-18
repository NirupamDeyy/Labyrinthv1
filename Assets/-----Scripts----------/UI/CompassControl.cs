using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CompassControl : MonoBehaviour
{
    [SerializeField] private RawImage compassImage;
    [SerializeField] private Transform player;
    [SerializeField] private TMP_Text compassDirectionText;

    void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360, 0, 1 ,1);

        Vector3 forward = player.transform.forward;

        forward.y = 0;

        float  headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;
        headingAngle = 5 * Mathf.RoundToInt(headingAngle / 5.0f);

        int displayAngle;
        displayAngle = Mathf.RoundToInt(headingAngle);

        switch (displayAngle)
        {
            case 0: compassDirectionText.text = "N";
                break;
            case 360: compassDirectionText.text = "N";
                break;
            case 45: compassDirectionText.text = "NE";
                break;
            case 90: compassDirectionText.text = "E";
                break;
            case 135: compassDirectionText.text = "SE";
                break;
            case 180: compassDirectionText.text = "S";
                break;
            case 225: compassDirectionText.text = "SW";
                break;
            case 270: compassDirectionText.text = "W";
                break;
            case 315: compassDirectionText.text = "NW";
                break;
            default: compassDirectionText.text = headingAngle.ToString();
                break;

        }
    }
}
