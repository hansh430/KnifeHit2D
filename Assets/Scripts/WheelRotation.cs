using System;
using System.Collections;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    #region Dependencies
    [SerializeField] private RotationElements[] rotationPatterns;
    private WheelJoint2D wheelJoint;
    private JointMotor2D motor;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        StartCoroutine(PlayRotationPatterns());
    }
    #endregion

    #region Coroutines
    private IEnumerator PlayRotationPatterns()
    {
        int rotationIndex = 0;
        while (true)
        {
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = rotationPatterns[rotationIndex].speed;
            motor.maxMotorTorque = 10000;
            wheelJoint.motor = motor;

            yield return new WaitForSecondsRealtime(rotationPatterns[rotationIndex].duration);
            rotationIndex++;
            rotationIndex = rotationIndex < rotationPatterns.Length ? rotationIndex : 0;
        }
    }
    #endregion


}

[Serializable]
class RotationElements
{
    public float speed;
    public float duration;
}