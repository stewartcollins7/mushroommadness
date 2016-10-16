using UnityEngine;
using System.Collections;

public class GrassScript : MonoBehaviour {

    //public Shader grassShader;

    private MeshRenderer meshRenderer;

    private Transform lightsource;
    private Color lightColor;
    const float pullDistance = 0.5f;
    const float pullTime = 1f;
    float timer = pullTime;
    float opacity = 1f;

    bool dying = false;

	void Start () {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        //renderer.material.shader = grassShader;

        LightSource sun = FindObjectOfType<LightSource>();
        lightsource = sun.gameObject.transform;
        lightColor = sun.lightColor;
	}
	
	// Update is called once per frame
	void Update () {
        meshRenderer.material.SetColor("_PointLightColor", lightColor);
        meshRenderer.material.SetVector("_PointLightPosition", lightsource.position);
        meshRenderer.material.SetFloat("_Opacity", opacity);

        if (dying)
        {
            //Transform thisTransform = this.transform;
            Vector3 newPosition = this.transform.position;
            
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                newPosition.y += (pullDistance * (Time.deltaTime / pullTime));
                opacity -= Time.deltaTime / pullTime;
                this.transform.position = newPosition;
            }else
            {
                Destroy(this.gameObject);
                dying = false;
            }
        }

        
    }

    public void PullGrass()
    {
        dying = true;
    }

    private IEnumerator pullGrass()
    {
        const float pullDistance = 1f;
        const float pullTime = 3f;
        float timer = pullTime;
        while (timer > 0)
        {

        }yield return new WaitForSeconds(1f);
    }
}
