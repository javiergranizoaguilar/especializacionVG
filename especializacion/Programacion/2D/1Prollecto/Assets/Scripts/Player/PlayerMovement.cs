using System;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float velocidad= 5f;
    [SerializeField] public float movimiento = 0f;
    [SerializeField] public float salto = 0f;
    [SerializeField] public float time = 0f;
    [SerializeField] public bool suelo = true;
    [SerializeField] TextMeshProUGUI panel_puntuacion;
    [SerializeField] TextMeshProUGUI panel_time;

    private Rigidbody2D rb;
    private int puntuacion = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        time += Time.deltaTime;
        panel_time.text =""+time;
    }
    void Movement() 
    {
        
        movimiento = Input.GetAxis("Horizontal");
        salto = suelo ? Input.GetAxis("Vertical") : 0;
        rb.AddForce(new Vector2(movimiento * velocidad, salto * 30));

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bola")
        {
            collision.gameObject.SetActive(false);
            puntuacion++;
            panel_puntuacion.text="Puntuacion: "+puntuacion;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("a");
        if (other.CompareTag("suelo"))
        {
            Debug.Log("a");
            suelo = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "suelo")
        {
            suelo = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
