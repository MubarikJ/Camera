using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamScript1 : MonoBehaviour
{
    public RawImage rawimage;
    private WebCamTexture webcamTexture;

    void Start()
    {
        //Obtain camera devices available
        WebCamDevice[] cam_devices = WebCamTexture.devices;
        //Set a camera to the webcamTexture
        webcamTexture = new WebCamTexture(cam_devices[0].name, 480, 640, 30);
        //Set the webcamTexture to the texture of the rawimage
        rawimage.texture = webcamTexture;
        rawimage.material.mainTexture = webcamTexture;
        //Start the camera
        webcamTexture.Play();
    }
    private IEnumerator SaveImage()
    {
        //Create a Texture2D with the size of the rendered image on the screen.
        Texture2D texture = new Texture2D(rawimage.texture.width, rawimage.texture.height, TextureFormat.ARGB32, false);
        //Save the image to the Texture2D
        texture.SetPixels(webcamTexture.GetPixels());
        //texture = RotateTexture(texture, -90);
        texture.Apply();
        yield return new WaitForEndOfFrame();
        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(texture, "CameraTest", "CaptureImage.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));
        // To avoid memory leaks
        //Destroy(texture);
    }
    public void clickCapture()
    {
        StartCoroutine(SaveImage());
    }

}
