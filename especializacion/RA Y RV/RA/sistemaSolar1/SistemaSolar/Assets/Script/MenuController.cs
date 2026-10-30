using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject explicacion;
    public void iniciar() 
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void salir() 
    {
        Application.Quit();
    }
    public void explicacionAparecer()
    {
        explicacion.SetActive(true);
    }
    public void explicacionDesaparecer()
    {
        explicacion.SetActive(false);
    }
}
