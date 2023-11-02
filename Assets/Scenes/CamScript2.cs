using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript2 : MonoBehaviour
{


	private IEnumerator TakeScreenshotAndSave()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		// Save the screenshot to Gallery/Photos
		NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));

		Debug.Log("Permission result: " + permission);

		// To avoid memory leaks
		Destroy(ss);
	}

	public void PickImage(int maxSize)
	{
		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				// Create Texture from selected image
				Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}

				// Assign texture to a temporary quad and destroy it after 5 seconds
				GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                
				quad.transform.position = GameObject.FindWithTag("Quad").transform.position;
				quad.transform.forward = Camera.main.transform.forward;
				Vector3 newScale = transform.localScale;
				newScale *= 6f;
				quad.transform.localScale = newScale;

				Material material = quad.GetComponent<Renderer>().material;
				//if (!material.shader.isSupported) // happens when Standard shader is not included in the build
					material.shader = Shader.Find("UI/Default");

				material.mainTexture = texture;

				//Destroy(quad, 5f);

				// If a procedural texture is not destroyed manually, 
				// it will only be freed after a scene change
				//Destroy(texture, 5f);
			}
		});

		Debug.Log("Permission result: " + permission);
	}

}
