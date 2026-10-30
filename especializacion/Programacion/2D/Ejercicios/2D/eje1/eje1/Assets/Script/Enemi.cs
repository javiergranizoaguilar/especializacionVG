using UnityEngine;

public class Enemi : MonoBehaviour
{
    [SerializeField] Vector2 puntoA;
    [SerializeField] Vector2 puntoB;
    [SerializeField] float velocidad;
    private Vector2 objetivoActual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Al empezar, nos aseguramos de que el objeto esté en el punto A
        transform.position = puntoA;

        // El primer objetivo será el punto B
        objetivoActual = puntoB;
    }

    void Update()
    {
        // 1. Movemos el objeto poco a poco hacia el objetivo actual usando una velocidad constante
        transform.position = Vector2.MoveTowards(transform.position, objetivoActual, velocidad * Time.deltaTime);

        // 2. Comprobamos si ya llegó al objetivo actual (con una pequeña tolerancia de 0.01f)
        if (Vector2.Distance(transform.position, objetivoActual) < 0.01f)
        {
            // Si llegó al punto B, cambiamos el objetivo al punto A. 
            // Si llegó al punto A, cambiamos el objetivo al punto B.
            if (objetivoActual == puntoB)
            {
                objetivoActual = puntoA;
            }
            else
            {
                objetivoActual = puntoB;
            }
        }
    }
}
