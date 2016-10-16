using UnityEngine;
using System.Collections;

public class River : MonoBehaviour {

    public float riverScrollSpeedX = 0.5f;
    public float riverScrollSpeedY = 0.5f;
    public LightSource sun;


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    float offsetX = Time.time * riverScrollSpeedX;
        float offsetY = Time.time * riverScrollSpeedY;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
