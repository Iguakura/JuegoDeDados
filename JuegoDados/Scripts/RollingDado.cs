using Mono.Cecil.Cil;
using UnityEngine;

public class RollingDado : MonoBehaviour

{
    public Rigidbody dadoRigidbody;
    void Start()
    {
        dadoRigidbody.useGravity = false;
    }

    void RollDado()
    {
        dadoRigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        dadoRigidbody.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 

        {
            RollDado();
        }

        dadoRigidbody.useGravity=true; 

        
    }
}
