using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    [SerializeField] GameObject MenuGameCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Menu();
    }
    void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            MenuGameCanvas.SetActive(true);
        }
    }
    public void Continuar()
    {
        Time.timeScale = 1;
        MenuGameCanvas.SetActive(false);
    }
    public void Salir()
    {
        Application.Quit();
    }
    public void volver()
    {
        SceneManager.LoadScene("Menu_Inicio");
    }
    public void reiniciar() 
    {
        SceneManager.LoadScene("Eje1");
    }
}
