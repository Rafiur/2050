using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;

    public TouchPad touchPad;
    //public TouchAreaButton areaButton;

    public GameObject shot;
	public Transform LeftshotSpawn;
    public Transform RightshotSpawn;
    public float fireRate;
	 
	private float nextFire;
    //private Quaternion calibrationQuaternion;

    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Update ()
	{
		if (Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, LeftshotSpawn.position, LeftshotSpawn.rotation);
            Instantiate(shot, RightshotSpawn.position, RightshotSpawn.rotation);
            GetComponent<AudioSource>().Play ();
		}
	}
    //void CalibrateAccelerometer()
    //{
    //    Vector3 accelerationSnapshot = Input.acceleration;
    //    Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
    //    calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    //}

    ////Get the 'calibrated' value from the Input
    //Vector3 FixAcceleration(Vector3 acceleration)
    //{
    //    Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
    //    return fixedAcceleration;
    //}


    void FixedUpdate ()
	{
        //float moveHorizontal = Input.GetAxis ("Horizontal");
        //float moveVertical = Input.GetAxis ("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector2 direction = touchPad.GetDirection();
        Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetXVelocityForShipTilt ()* -tilt);
	}

    private float GetXVelocityForShipTilt() {
        float maxVelocity = 10f;
        if (rigidbody2d.velocity.x >= maxVelocity)
        {
            return maxVelocity;
        }
        else {
            return rigidbody2d.velocity.x;
        }
    }
}
