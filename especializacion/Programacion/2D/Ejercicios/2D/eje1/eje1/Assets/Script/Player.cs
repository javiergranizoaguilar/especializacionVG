using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] int vidas = 3;
    [SerializeField] float velocidad = 5f;
    [SerializeField] float movimiento;
    [SerializeField] float salto;
    [SerializeField] float jumpForze=350;
    [SerializeField] bool suelo = true;
    [SerializeField] bool dobleSalto = true;
    [SerializeField] float coolDownJump = 0f;
    [SerializeField] float coolDownHit = 0f;
    [SerializeField] ParticleSystem VFX;
    [SerializeField] AudioSource audioMoneda;
    [SerializeField] AudioSource audioLLave;
    [SerializeField] AudioSource audioDanio;
    [SerializeField] AudioSource audioFin;
    [SerializeField] TextMeshProUGUI textcoins;
    [SerializeField] TextMeshProUGUI textpeligro;
    [SerializeField] TextMeshProUGUI textVidas;
    [SerializeField] TextMeshProUGUI textCoolDown;
    [SerializeField] UnityEngine.UI.Slider slider;
    [SerializeField] GameObject muerte;
    [SerializeField] GameObject llave;
    [SerializeField] GameObject puertaObjeto;
    [SerializeField] GameObject MenuFin;


    //[SerializeField] private InputActionProperty pulsacionAction;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (! dobleSalto)
        {
            TimerCooldown();
            
        }
        TimerHitCooldown();
        Movement();
        
    }
    /*public void OnEnable()
    {
        pulsacionAction.action.Enable();
        pulsacionAction.action.performed+= pulsacionTecla;
    }
    public void OnDisable() 
    {
        pulsacionAction.action.Disable();
        pulsacionAction.action.performed -= pulsacionTecla;
    }
    private void pulsacionTecla(InputAction.CallbackContext context)
    {
        Debug.Log("pulsa");
    }*/
    public void move() 
    {
    }
    void Movement()
    {

         movimiento = Input.GetAxis("Horizontal");
         salto = 0;
        
         if (Input.GetKeyDown(KeyCode.Space))
         {
            if (suelo)
            {
                salto = 1;
            }
            else
            {
                if(dobleSalto){
                    salto = 1;
                    dobleSalto=false;
                    coolDownJump = 5f;
                } 
            }           
         }
         rb.AddForce(new Vector2(movimiento * velocidad, salto * jumpForze));
    }
    public void saltoMatar()
    {
        rb.AddForce(new Vector2(0,800));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="suelo")
        {
            suelo = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "suelo")
        {
            suelo = false;
        }

    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Enemi"))
        {
            if(coolDownHit==0)
            TriggerEnemi();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("spikes"))
        {
            TriggerPinchos();
            audioDanio.Play();
        }
        if (collision.CompareTag("Enemi"))
        {
            if (coolDownHit == 0)
            {
                TriggerEnemi();
                coolDownHit = 5f;
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        if (collision.CompareTag("Coins"))
        {
            TriggerMonedas(collision);
            audioMoneda.Play();
        }
        if (collision.CompareTag("llave"))
        {
            puertaObjeto.SetActive(false);
            Destroy(collision.gameObject);
            audioLLave.Play();
        }
        if (collision.CompareTag("void"))
        {
            textpeligro.gameObject.SetActive(true);
        }
        if (collision.CompareTag("Fin")) 
        {
            Time.timeScale = 0;
            MenuFin.SetActive(true);
            audioFin.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("void"))
        {
            textpeligro.gameObject.SetActive(false);


        }
    }
    public void TriggerMonedas(Collider2D collision) {
        Destroy(collision.gameObject);
        coins++;
        textcoins.text = "Monedas: " + coins;
        VFX.transform.position = collision.gameObject.transform.position;
        VFX.Play();
        if (coins == 10)
        {
            llave.SetActive(true);
        }
    }
    public void TriggerPinchos()
    {
        transform.position = new Vector2(1, -2);
        Danio();
        
    }
    public void TriggerEnemi() 
    {
        Danio();
    }
    public void Danio() 
    {
        vidas--;
        slider.value = vidas;
        textVidas.text = "Vidas: " + vidas;
        if (vidas <= 0)
        {
            muerte.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void TimerCooldown()
    {

        if (coolDownJump > 0)
        {
            coolDownJump -= Time.deltaTime;
            

        }
        else
        {
            coolDownJump = 0;
            dobleSalto = true;
            
        }
        textCoolDown.text = "CoolDown: " + coolDownJump;
    }
    public void TimerHitCooldown()
    {

        if (coolDownHit > 0)
        {
            coolDownHit -= Time.deltaTime;

        }
        else
        {
            coolDownHit = 0;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    public void Reinicio() {
        SceneManager.LoadScene("Eje1");
    }
}
