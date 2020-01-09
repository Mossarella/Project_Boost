using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0.0f, 1.0f)] float movementFactor; //0=not move 1=move
    [SerializeField] float loopFinishTime = 2f;
    
    Vector3 startingPos; //must have for absolut movement

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (loopFinishTime <= Mathf.Epsilon)
        {
            return;
        }

        float cycles = Time.time / loopFinishTime;
        const float tau = Mathf.PI * 2f;
        float oneCycle = Mathf.Sin(cycles * tau);
        movementFactor = oneCycle / 2f +0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
