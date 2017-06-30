using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class snakeMovement : MonoBehaviour {

	public List<Transform> bodyParts = new List<Transform>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

	#region
	[Range(-1.0f,1.0f)]
	public float currentRotation = 0.0f;
	void Update () {
		if(Input.GetKey(KeyCode.A)){
			currentRotation += rotationSensitivity * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.D)){
			currentRotation -= rotationSensitivity * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.W)) {
			transform.position += transform.up * speed * Time.deltaTime;
		}
	}
	#endregion
	public float speed = 3.5f;

	public float rotationSensitivity = 50.0f;

	void FixedUpdate (){
		MoveForward ();
		Rotation ();
		CameraFollow ();
		ColorMySnake ();
	}

	void MoveForward()
	{
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void Rotation()
	{
		transform.rotation = Quaternion.Euler(new Vector3(transform.position.x,0.0f,currentRotation));
	}

	[Range(0.0f,1.0f)]
	public float smoothTime = 0.5f;
	void CameraFollow()
	{
		Transform camera = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.transform;
		Vector3 cameraVelocity = Vector3.zero;
		camera.position = Vector3.SmoothDamp (camera.position,
			new Vector3(transform.position.x,transform.position.y,-10), ref cameraVelocity, smoothTime);
	}

	public Transform bodyObject;
	void OnCollisionEnter (Collision col)
	{
		if (col.transform.tag == "Orb") {
			Destroy (col.gameObject);
			this.score++;
			this.UpdateScore();
			this.GetComponent<AudioSource> ().Play ();
			if (bodyParts.Count == 0) {
				Vector3 currentPos = transform.position;
				Transform newBodyPart = Instantiate (bodyObject, currentPos, Quaternion.identity) as Transform;
				bodyParts.Add (newBodyPart);
			} 
			else {
				Vector3 currentPos = bodyParts[bodyParts.Count-1].position;
				Transform newBodyPart = Instantiate (bodyObject, currentPos, Quaternion.identity) as Transform;
				bodyParts.Add (newBodyPart);
			}
		}

		if (col.transform.tag == "Finish") {
			Application.LoadLevel (0);

		}
		if (col.transform.tag == "Body") {
			if(bodyParts.Count >= 3)
			Application.LoadLevel (0);
		}
	}

	int score = 0;
	public void UpdateScore()
	{
		GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>().text = "Score: " + this.score;
	}

	public Material purble, orange;

	void ColorMySnake()
	{
		for (int i = 0; i < bodyParts.Count; i++) {
			if (i % 2 == 0) {
				bodyParts [i].GetComponent<Renderer> ().material = purble;
			} 
			else {
				bodyParts [i].GetComponent<Renderer> ().material = orange;
			}
		}
	}
}
