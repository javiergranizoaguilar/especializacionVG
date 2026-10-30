using TMPro;
using UnityEngine;
using Vuforia;
public class Reconocimiento : MonoBehaviour
{
    public ObserverBehaviour observer;
    public GameControl Gamecontroler;
    [SerializeField] string planeta;
    private void Awake()
    {
        observer=GetComponent<ObserverBehaviour>();
    }
    private void OnEnable()
    {
        if (observer != null) {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }
    private void OnDisable()
    {
        if (observer != null)
        {
            observer.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status) 
    {
        //Game Logic
        Gamecontroler.Chek(planeta);
    }

}
