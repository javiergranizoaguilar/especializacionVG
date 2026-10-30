using System;
using System.Security.Cryptography;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    private float time = 60.0f;
    private string[] opcionesMostrar = {"Rueda","Coche Abandonado","Furgoneta", "Coche Policia", "Coche Antiguo" };
    [SerializeField] int Score = 0;
    [SerializeField] TextMeshProUGUI textTime;
    [SerializeField] TextMeshProUGUI cosaAdivinar;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI ScoreTextEnd;
    [SerializeField] GameObject UIGame;
    [SerializeField] GameObject EndeGame;
    [SerializeField] AudioSource sonidoAcierto;
    [SerializeField] AudioSource sonidoFallo;
    private bool OnTime = true;
    private string PlanetaAnterior="";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eleguirNuevoPaneta();
        EndeGame.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        contorlTime();
    }
    private void contorlTime() {
        time-=Time.deltaTime;
        
        if (time <= 0)
        {
            textTime.text = "Time: 0";
            OnTime = false;
            ScoreTextEnd.text ="Puntuacion Total: "+Score;
            EndeGame.gameObject.SetActive(true);
            UIGame.SetActive(false);
        }
        else {
            textTime.text = "Time: " + time.ToString("F1");
        }
    }
    private void eleguirNuevoPaneta()
    {
        int posAleatoria = UnityEngine.Random.Range(0, opcionesMostrar.Length);
        if (PlanetaAnterior == opcionesMostrar[posAleatoria]) 
        {
            eleguirNuevoPaneta();
        }
        else
        {
            cosaAdivinar.text = opcionesMostrar[posAleatoria];
            PlanetaAnterior = opcionesMostrar[posAleatoria];
        }
    }
    public void Chek(string planetaEncontrado)
    {
        if ((planetaEncontrado == cosaAdivinar.text) & OnTime)
        {
            Score += 100;
            ScoreText.text="Score: "+Score;
            eleguirNuevoPaneta();
            sonidoAcierto.Play();
        }
        else
        {
            eleguirNuevoPaneta();
            sonidoFallo.Play();
        }
    }

    public void ReStart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        /*
        time = 100.0f;
        Score = 0;
        OnTime = true;
        eleguirNuevoPaneta();
        EndeGame.gameObject.SetActive(false);
        UIGame.gameObject.SetActive(true);
        */
    }
}
