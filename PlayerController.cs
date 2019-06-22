using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    [Header ("General")]
    [Tooltip ("In ms^-1")][SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 20f;
    [Tooltip("In m")] [SerializeField] float y1Range = 13f;
    [Tooltip("In m")] [SerializeField] float y2Range = 15f;

    [Header ("Screen-position based")]
    [SerializeField] float positionpitchfactor = -5f;
    [SerializeField] float controlpitchfactor = -20f;

    [Header ("Control-throw based")]
    [SerializeField] float positionyawfactor = 5f;
    [SerializeField] float controlrollfactor = -20f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Update is called once per frame
    void Update() {

        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }

    }

    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

     private void ProcessRotation()
       {
        float pitchduetoposition = transform.localRotation.y * positionpitchfactor;
        float pitchduetocontrolflow = yThrow * controlpitchfactor;
        float pitch = pitchduetoposition + pitchduetocontrolflow;
        float yaw = transform.localRotation.x * positionyawfactor;
        float roll = xThrow * controlrollfactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
       }
     private void ProcessTranslation()
    { 
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * controlSpeed * Time.deltaTime;

        float rawXpos = transform.localPosition.x + xOffset;

        float clampedXpos = Mathf.Clamp(rawXpos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawYpos = transform.localPosition.y + yOffset;

        float clampedYpos = Mathf.Clamp(rawYpos, -y2Range, y1Range);

        transform.localPosition = new Vector3(clampedXpos, clampedYpos, transform.localPosition.z);


    }
}
