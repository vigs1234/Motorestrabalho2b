using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public int vidas = 3;
    public int tijolosR;

    public GameObject playerPrefab;
    public GameObject ballPrefab;
    public Transform playerSpawnPoint;
    public Transform ballSpawnPoint;

    public PlayerB playerAtual;
    public BallB ballAtual;

    public TextMeshProUGUI Vidas;
    public TextMeshProUGUI final;

    public bool segurando;
    private Vector3 offset;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        SpawnarNovoJogador();
        AtualizarVidas();
        tijolosR = GameObject.FindGameObjectsWithTag("Tijolo").Length;
    }

    public void AtualizarVidas()
    {
        Vidas.text = $"Vidas: {vidas}";
    }

    public void SpawnarNovoJogador()
    {
        GameObject playerObj = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        GameObject ballObj = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

        playerAtual = playerObj.GetComponent<PlayerB>();
        ballAtual = ballObj.GetComponent<BallB>();
        segurando = true;
        offset = playerAtual.transform.position - ballAtual.transform.position;
    }
    public void SubtrairTijolo()
    {
        tijolosR--;

        if(tijolosR<= 0)
        {
            Vitoria();
        }
    
    }
      public void SubtrairVida()
    {
        Vidas--;
         Destroy(ballAtual.gameObject);
         Destroy(playerAtual.gameObject);
         if(Vidas <= 0)
         {
            GameOver();
         }
         else
         {
             Invoke( nameof(SpawnarNovoJogador),2);
         }

    }
     public void Vitoria()
    {
        final.text = "Parabens!!!";
        Destroy(ballAtual.gameObject);
        Invoke( nameof(Reiniciar),2);
    }
     public void Gameover()
    {
        
    }
     public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void Update()
    {
        if (segurando){
            ballAtual.transform.position = playerAtual.transform.position - offset;

            if(Input.GetKeyDown(KeyCode.Space)){
                ballAtual.DispararBolinha(playerAtual.inputX);
                segurando = false;
            }
        }
    }
}
