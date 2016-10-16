using UnityEngine;
using System.Collections;

public class LightSource : MonoBehaviour {

    public Color lightColor;
    //The speed of the rotation
    public float rotationAngle = 0.25f;
    //The position the sun rotates around
    public Transform sunPivot;

    //Returns the position of the sun
    public Vector3 GetWorldPosition()
    {
        return this.transform.position;
    }

    //Rotates the sun around the pivot
    public void Update()
    {
        this.transform.RotateAround(sunPivot.position, Vector3.back, rotationAngle * Time.deltaTime);
    }
}
