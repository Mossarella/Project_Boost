using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{ enum State { Alive, Dying, Transcend }
    Rigidbody rigidBody;
    AudioSource audiosource;
    [SerializeField] float rThrust = 100f;
    [SerializeField] float accelerateThrust = 100f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip explode;
    [SerializeField] AudioClip loadLevel;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem explodeParticle;
    [SerializeField] ParticleSystem loadLevelParticle;

    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Spawn",0.8f);
    }

    void Spawn()
    {

        if (state == State.Alive)
        {
            Thrust();
            Rotating();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }
        
        switch (collision.gameObject.tag)
        {
            case "Friendly":           
            break;
            case "Finish":
                audiosource.Stop();
                mainEngineParticle.Stop();
                loadLevelParticle.Play();
                state = State.Transcend; 
                audiosource.PlayOneShot(loadLevel);                 
                Invoke("LoadNextLevel", 1f); 
                break;
            
            case "Deadly":
                audiosource.Stop();
                mainEngineParticle.Stop();
                explodeParticle.Play();
                state = State.Dying;            
                audiosource.PlayOneShot(explode);                 
                print("Ded");
                Invoke("LoadFirstLevel", 1f);
                break;
        }
        
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    void Thrust()
    {
        float thrustThisFrame = accelerateThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            mainEngineParticle.Play();
            if (!audiosource.isPlaying) // so it doesn't layer
            {                
                audiosource.PlayOneShot(mainEngine);
            }
        }
       
        else
        {
            mainEngineParticle.Stop();
            audiosource.Stop();
        }
    }

   

    void Rotating()
    {
        rigidBody.freezeRotation = true; // pause physics on press
        
        
        float rotationThisFrame = rThrust * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; //resume physics when not pressed
        
    }
}
