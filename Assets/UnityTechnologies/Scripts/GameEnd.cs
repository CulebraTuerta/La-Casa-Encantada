using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public GameObject player; //con esto le decimos al objeto que le pongamos al script, que necesita interactuar con un objeto player
    public CanvasGroup backgroundWin;
    public CanvasGroup backgroundLose;
    public float fadeDuration;
    public float displayImageDuration; //nos da un poco mas de tiempo para terminar la app

    public AudioSource winSound;
    public AudioSource loseSound;
    private bool hasAudioPlayed;

    private float timer;
    private bool isPlayer;
    private bool isCaugth;
    void Start()
    {
            
    }

    void Update()
    {
        if (isPlayer)
        {
            EndLevel(backgroundWin,false,winSound); //false porque no queremos que reinicie el nivel
        }
        else if(isCaugth)
        {
            EndLevel(backgroundLose,true,loseSound); //aqui el true es simplemente para repetir el nivel
            CaughtPlayer();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();  //esto cierra el juego, solo funciona en el juego exportado.
        }
    }

    private void OnTriggerEnter(Collider other) //si otro objeto entra en colision
    {
        if (other.gameObject==player)
        {
            isPlayer = true;
        }
    }

    public void EndLevel(CanvasGroup imgCanvas,bool doRestart, AudioSource sound) //Con este canvasgroup puedo elegir cualquera de las dos situaciones.
    {
        if (!hasAudioPlayed) //si no esta sonando nada
        {
            sound.Play();
            hasAudioPlayed = true;
        }

        timer += Time.deltaTime; //intervalo en segundos del ultimo frame al siguiente.
        imgCanvas.alpha = timer/fadeDuration; //modificando el valor podemos determinar cuanto se demorara

        if(timer>fadeDuration+displayImageDuration)
        {
            
            if(doRestart)
            {
                //necesitamos recargar la misma escena donde estamos.., necesitaremos una libreria unityengine.scenemanagement;
                SceneManager.LoadScene(0); //no  se por que el cero...
            }
            else
            {
                SceneManager.LoadScene(0); //si tuviesemos otro nivel, deberia cargarse aqui... por ahora solo lo reiniciaremos.
            }
        }
    }

    public void CaughtPlayer()
    {
        isCaugth= true;
    }
}
