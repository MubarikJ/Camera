using UnityEngine;
using Wilberforce;

public class ChangeColorblindTypeButton : MonoBehaviour
{
    public Colorblind colorblindScript;

    public void ChangeColorblindType(int newType)
    {
        // Update the Type parameter of the Colorblind script
        colorblindScript.Type = newType;
    }
}
