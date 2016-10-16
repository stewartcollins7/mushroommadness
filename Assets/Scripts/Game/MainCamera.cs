using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class MainCamera : MonoBehaviour {

    public Transform[] cameraLoopTransforms;
    private GameManager gameManager;

    void Start()
    {
        StartCoroutine(CameraLoop());
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartTripping()
    {
        this.GetComponent<BloomAndFlares>().enabled = true;
        
    }

    public void StopTripping()
    {
        this.GetComponent<BloomAndFlares>().enabled = false;
    }

    public IEnumerator CameraLoop()
    {
        float seconds = 3f;
        float elapsedTime = 0;
        Vector3 startingPos;
        Vector3 endPosition;
        Quaternion startRotation;
        Quaternion endRotation;

        for (int i = 1; i < cameraLoopTransforms.Length; i++)
        {
            //Don't know what is going on with quaternion slerp but it is very sensitive, this is within a small window of working values for it
            float rotationSpeed = 0.012f;
            startingPos = this.transform.position;
            startRotation = this.transform.rotation;
            endRotation = cameraLoopTransforms[i].rotation;
            endPosition = cameraLoopTransforms[i].position;
            while (elapsedTime < seconds)
            {
                this.transform.localPosition = Vector3.Lerp(startingPos, endPosition, (elapsedTime / seconds));
                this.transform.localRotation = Quaternion.Lerp(transform.rotation, endRotation, rotationSpeed);
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            this.transform.position = endPosition;
            this.transform.rotation = endRotation;
            elapsedTime = 0;
        }gameManager.StartPicking();
    }

}
