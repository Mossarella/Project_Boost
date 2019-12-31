using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotating();
        Thrust();
        Sound();
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            print("Thrusting");
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
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
            print("Rotating left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back);
            print("Rotating right");
        }
    }
}
