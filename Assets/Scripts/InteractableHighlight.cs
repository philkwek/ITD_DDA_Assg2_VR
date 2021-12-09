using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnHover()
    {
        //Get all the renderers of this object
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            //Enables the Emission property of the renderer's material
            renderer.material.EnableKeyword("-EMISSION");
        }
    }

    // Update is called once per frame
    public void ExitHover()
    {
        //get all MeshRenderers and store them
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        //Look through all MeshRenderers and turn off emission property
        foreach (MeshRenderer renderer in meshRenderers)
        {
            //turn off emission property of each redenrer
            renderer.material.DisableKeyword("_EMISSION");
        }
    }
}
