using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Board board;
    [SerializeField] Bot bot;
    [SerializeField] GameObject botpanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject perderPanel;
    [SerializeField] GameObject ganarPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Ganar()
    {
        ganarPanel.SetActive(true);
    }
    public void Perder()
    {
        perderPanel.SetActive(true);
    }
    public void Reiniciar()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Jugador()
    {
        board.IniciarTablero();
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }
    private void Bot()
    {
        board.IniciarTablero();
        botpanel.SetActive(true);
        menuPanel.SetActive(false);
        Time.timeScale = 1;
        
    }
    public void BotFacil()
    {
        Bot();
        bot.IniciarBot(1);
    }
    public void BotMedio()
    {
        Bot();
        bot.IniciarBot(2);
    }
    public void BotDificil()
    {
        Bot();
        bot.IniciarBot(3);
    }
}
