
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour {
    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip win;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem boost;
    [SerializeField] ParticleSystem succcess;
    [SerializeField] ParticleSystem boom;

    bool CollisionsDisabled = false;
    bool Debugger = true;

    bool isTransitioning = false;


    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Debugger == true)
        debugkeys();

        if (!isTransitioning)
        {
            Fly();
            Rotate();
        }
    }

    private void debugkeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CollisionsDisabled = !CollisionsDisabled;
        }
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
                    StartSuccessSequence();
                    break;
                }

            default:
                {
                    StartDeathSequence();
                    break;
                }
                if (isTransitioning || CollisionsDisabled) { return; }
        }
    }

    private void StartDeathSequence()
    {
        isTransitioning = true;
        Invoke("LoadFirstLevel", 1f);
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        boom.Play();
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        Invoke("LoadNextLevel", 1f);
        audioSource.Stop();
        audioSource.PlayOneShot(win);
        succcess.Play();
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(0);
        
    }

    private void Fly()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up*mainThrust*Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            boost.Play();
        }
        else
        {
            audioSource.Stop();
            boost.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.angularVelocity = Vector3.zero;
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
