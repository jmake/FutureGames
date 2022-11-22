using UnityEngine;

// A simple FPP (First Person Perspective) camera rotation script.
// Like those found in most FPS (First Person Shooter) games.
public class FirstPersonCameraRotation : MonoBehaviour 
{
	public float Sensitivity 
  {
		get { return sensitivity; }
		set { sensitivity = value; }
	}

	[Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
	[Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
	[Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

  //Strings in direct code generate garbage, storing and re-using them creates no garbage
	Vector2 rotation = Vector2.zero;
	const string xAxis = "Mouse X"; 
	const string yAxis = "Mouse Y";


	void Update()
  {
    // top - bottom 
		rotation.y += Input.GetAxis(yAxis) * sensitivity;
		rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
		var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);
    //transform.localRotation = yQuat;

    // left - right 
    rotation.x += Input.GetAxis(xAxis) * sensitivity;
    var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
    transform.localRotation = xQuat;

    // Quaternions seem to rotate more consistently than EulerAngles. 
    // Sensitivity seemed to change slightly at certain degrees using Euler. 
    // transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
    //transform.localRotation = xQuat * yQuat; 
	}

}  // FirstPersonCameraRotation 
