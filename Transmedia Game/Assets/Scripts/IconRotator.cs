using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconRotator : MonoBehaviour
{
    private void Start()
    {
        BeginRotation();
    }

    public void BeginRotation()
    {
        StartCoroutine(RotateObject(90, Vector3.back, 0.5f));
    }

    IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
    {
        // calculate rotation speed
        float rotationSpeed = angle / inTime;

        while (true)
        {
            // save starting rotation position
            Quaternion startRotation = transform.rotation;

            float deltaAngle = 0;

            // rotate until reaching angle
            while (deltaAngle < angle)
            {
                deltaAngle += rotationSpeed * Time.deltaTime;
                deltaAngle = Mathf.Min(deltaAngle, angle);

                transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

                yield return null;
            }

            // delay here
            yield return new WaitForSeconds(1);
        }
    }
}
