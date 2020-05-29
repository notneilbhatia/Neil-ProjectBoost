
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour {
    Rigidbody rigidBody;
	AudioSource thrust;
   [SerializeField] float rcsThrust = 100f;
   [SerializeField] float mainThrust = 100f;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		thrust = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Fly();
        Rotate();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Respawn":
                {
                    print("Good Luck");
                    break;
                }

            case "Finish":
                {
                    SceneManager.LoadScene(1);
                    break;
                }
                
            default:
                { 
                SceneManager.LoadScene(0);
                break;
        }
        }
    }

    private void Fly()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);

            if (!thrust.isPlaying)
            {
                thrust.Play();
            }
        }
        else
        {
            thrust.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        float rotationthisframe = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward*rotationthisframe);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotationthisframe);
        }
        rigidBody.freezeRotation = false;
    }
}
