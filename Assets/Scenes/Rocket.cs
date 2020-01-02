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
        if (state == State.Alive)
        {
            Rotating();
            Thrust();
            Sound();
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
                state = State.Transcend;
                Invoke("LoadNextLevel", 1f); //parameter this "time"
                break;
            default:
                state = State.Dying;
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
            rigidBody.AddRelativeForce(Vector3.up *thrustThisFrame);
        }           
    }

    void Sound()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audiosource.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
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
