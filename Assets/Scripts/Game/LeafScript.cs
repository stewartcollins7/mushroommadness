using UnityEngine;
using System.Collections;

public class LeafScript : MonoBehaviour {

    //public Shader grassShader;

    MeshRenderer meshRenderer;

    private Transform lightsource;
    private Color lightColor;
    const float pullDistance = 2f;
    const float pullTime = 1f;
    float timer = pullTime;
    float opacity = 1f;

    bool dying = false;
    bool brushLeft = false;

    void Start()
    {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        //renderer.material.shader = grassShader;

        LightSource sun = FindObjectOfType<LightSource>();
        lightsource = sun.gameObject.transform;
        lightColor = sun.lightColor;
    }

    // Update is called once per frame
    void Update()
    {
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
                if (brushLeft)
                {
                    newPosition.x += (pullDistance * (Time.deltaTime / pullTime));
                }else
                {
                    newPosition.x -= (pullDistance * (Time.deltaTime / pullTime));
                }
                
                opacity -= Time.deltaTime / pullTime;
                this.transform.position = newPosition;
            }
            else
            {
                Destroy(this.gameObject);
                dying = false;
            }
        }


    }

    public void BrushLeaf(bool brushRight)
    {
        dying = true;
        brushLeft = !brushRight;
    }

}
