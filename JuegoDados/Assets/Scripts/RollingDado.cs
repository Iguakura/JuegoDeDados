using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RollingDado : MonoBehaviour
{
    public Rigidbody dadoRigidbody;
    public Text Textopuntaje;   
    private Vector3 initialPosition = new Vector3(0.3939533f, 4.12f, -1.688454f);
    private bool DadoLanzado = false;

    void Start()
    {
        dadoRigidbody.useGravity = false;
        transform.position = initialPosition;
        Debug.Log("Dado en inicio " + transform.position);

        if (Textopuntaje == null)
        {
            Textopuntaje = GameObject.Find("NombreDelObjetoText").GetComponent<Text>();
        }
        if (Textopuntaje == null)
        {
            Debug.LogError("No se encontró el objeto Text, asígnalo en el Inspector.");
        }
    }

    void RollDado()
    {
        dadoRigidbody.AddTorque(Random.Range(3000f, 50000f), Random.Range(3000f, 50000f), Random.Range(3000f, 50000f));
    }

    void AventarDado()
    {
        dadoRigidbody.useGravity = true; 
        dadoRigidbody.AddForce(Vector3.up * 20, ForceMode.Impulse);
        dadoRigidbody.AddTorque(Random.Range(3000f, 50000f), Random.Range(3000f, 50000f), Random.Range(3000f, 50000f));
        DadoLanzado = true;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetKey(KeyCode.Space) && !DadoLanzado)
        {
            RollDado();
        }

        if (Input.GetKeyUp(KeyCode.Space) && !DadoLanzado)
        {
            AventarDado();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            RestartGame();
        }

        if ((mouseX != 0 || mouseY != 0) && Input.GetMouseButton(0) && !DadoLanzado)
        {
            RollDado();
        }
        
        if ((mouseX != 0 || mouseY != 0) && Input.GetMouseButtonUp(0) && !DadoLanzado)
        {
            AventarDado();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
        {
            Debug.Log("El dado ha caído");
            DeterminarPuntaje();
        }
    }

void DeterminarPuntaje()
{
    RaycastHit hit;
    Vector3[] rayOrigins = new Vector3[]
    {
        transform.position + Vector3.up * 1.0f,
        transform.position + Vector3.up * 1.0f + Vector3.forward * 0.1f,
        transform.position + Vector3.up * 1.0f + Vector3.back * 0.1f,
        transform.position + Vector3.up * 1.0f + Vector3.left * 0.1f,
        transform.position + Vector3.up * 1.0f + Vector3.right * 0.1f
    };
    Vector3 rayDirection = Vector3.down;

    foreach (Vector3 rayOrigin in rayOrigins)
    {
        Debug.Log("Ray Origin: " + rayOrigin);
        Debug.Log("Ray Direction: " + rayDirection);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 1.0f))
        {
            Debug.Log("El rayo golpeó: " + hit.collider.name + " con etiqueta: " + hit.collider.tag);
            if (Physics.Raycast(transform.position, Vector3.up, out hit))
            {
                string puntaje = hit.collider.tag;
                Debug.Log("El puntaje es " + puntaje);
                if (Textopuntaje != null)
                {
                    Textopuntaje.text = "Puntaje: " + puntaje;
                    Debug.Log("Texto de puntaje actualizado: " + Textopuntaje.text);
                }
                else
                {
                    Debug.LogError("Textopuntaje es nulo. Asegúrate de asignarlo en el Inspector.");
                }
                return;
            }
        }
    }

    Debug.Log("No se ha detectado el puntaje");
}
}