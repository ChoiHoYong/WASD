using UnityEngine;
using System.Collections;

public class AutoTransparent : MonoBehaviour
{
    private Shader m_OldShader = null;
    private Color m_OldColor = Color.black;

    private Renderer renderer;

    private Coroutine coroutine;

    void Start()
    {
        //init
        this.renderer = this.GetComponent<Renderer>();
        this.m_OldShader = renderer.material.shader;
        this.m_OldColor = renderer.material.color;
        //this.set_obj_transparent_and_change_gamelayer();
    }

    //reset gameobject
    private IEnumerator reset_gameobject()
    {
        yield return new WaitForSeconds(1f);

        //set the shader and layer back
        renderer.material.shader = m_OldShader;
        renderer.material.color = m_OldColor;
        this.gameObject.layer = 0;
    }

    //set the gameobject 
    public void set_obj_transparent_and_change_gamelayer(float alpha)
    {
        if (this.gameObject.layer != 1 && this.renderer != null)
        {
            renderer.material.shader = Shader.Find("Transparent/Diffuse");

            Color C = renderer.material.color;
            C.a = alpha;
            renderer.material.color = C;

            this.gameObject.layer = 1;
        }

        if (this.coroutine != null)
        {
            StopCoroutine(this.coroutine);
        }

        this.coroutine = StartCoroutine(this.reset_gameobject());
    }
}
