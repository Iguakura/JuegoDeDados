using JetBrains.Annotations;
using UnityEngine;

public class FollowDice : MonoBehaviour
{
    public Transform DadoTransform;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    void Start()
    {
        if (DadoTransform != null)
        {
            Vector3 desiredPosition = DadoTransform.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }
    }


    void Update()
    {
        
    }
}
