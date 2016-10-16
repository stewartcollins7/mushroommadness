using UnityEngine;
using System.Collections;

public class MushroomScript : MonoBehaviour {

    private Color[] celShadingPalette;
    private MeshRenderer meshRenderer;
    public float moveSpeed = 5000f;
    public Color celShadingColor1;
    public Color celShadingColor2;
    public Color celShadingColor3;
    public Color celShadingColor4;
    public Color celShadingColor5;
    public Color celShadingColor6;
    public Color celShadingColor7;
    public Color celShadingColor8;

    private Transform lightsource;
    private Color lightColor;
    public enum MushroomType
    {
        EDIBLE, POISON, TRIPPY
    }

    public enum MushroomName
    {
        BUTTON, DEATHCAP, PANTHER
    }
    public MushroomType mushroomType = MushroomType.EDIBLE;
    public MushroomName mushroomName = MushroomName.BUTTON;

    // Use this for initialization
    void Start () {
        celShadingPalette = new Color[8];
        celShadingPalette[0] = celShadingColor1;
        celShadingPalette[1] = celShadingColor2;
        celShadingPalette[2] = celShadingColor3;
        celShadingPalette[3] = celShadingColor4;
        celShadingPalette[4] = celShadingColor5;
        celShadingPalette[5] = celShadingColor6;
        celShadingPalette[6] = celShadingColor7;
        celShadingPalette[7] = celShadingColor8;

        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        //renderer.material.shader = grassShader;
        LightSource sun = FindObjectOfType<LightSource>();
        lightsource = sun.gameObject.transform;
        lightColor = sun.lightColor;
        PassArrayToShader.Color(meshRenderer.material, "_CelShadingColors", celShadingPalette);
    }
	
	// Update is called once per frame
	void Update () {
        meshRenderer.material.SetColor("_PointLightColor", lightColor);
        meshRenderer.material.SetVector("_PointLightPosition", lightsource.position);
    }

    public void Rotate(Vector3 rotateDirection)
    {
        this.transform.Rotate(rotateDirection);
    }

    public void TossItAnimation()
    {
        StartCoroutine(MoveOffCamera(new Vector3(20, -0.5f, 4f)));
    }

    public void KeepItAnimation()
    {
        StartCoroutine(MoveOffCamera(new Vector3(-20, -0.5f, 4f)));
    }

    public void EatItAnimation()
    {
        StartCoroutine(MoveOffCamera(new Vector3(0, 20f, 4f)));
    }

    public IEnumerator MoveOffCamera(Vector3 endPosition)
    {
        float seconds = 2f;
        float elapsedTime = 0;
        Vector3 startingPos = this.transform.localPosition;
        while (elapsedTime < seconds)
        {
            this.transform.localPosition = Vector3.Lerp(startingPos, endPosition, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        this.transform.localPosition = endPosition;
        Destroy(this.gameObject);
    }


    public void BringToCamera()
    {
        //Vector3 endPosition = Camera.main.transform.position;
        //endPosition.y -= 2;
        //endPosition.z += 20;
        Vector3 endPosition = new Vector3(0, -0.5f, 4f);
        this.transform.parent = Camera.main.transform;
        StartCoroutine(MoveToCamera(endPosition));
    }

    public IEnumerator MoveToCamera(Vector3 endPosition)
    {
        float seconds = 0.75f;
        float elapsedTime = 0;
        Vector3 startingPos = this.transform.localPosition;
        Quaternion startRotation = this.transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(-60, 180, 180);
        while (elapsedTime < seconds)
        {
            this.transform.localPosition = Vector3.Lerp(startingPos, endPosition, (elapsedTime / seconds));
            this.transform.localRotation = Quaternion.Slerp(transform.localRotation, endRotation, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        this.transform.localPosition = endPosition;
        this.transform.localRotation = endRotation;
        /*
        while(this.gameObject.transform.position != endPosition)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, endPosition, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }*/
    }
}
