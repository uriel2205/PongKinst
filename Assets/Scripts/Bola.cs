using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Bola : MonoBehaviour
{
    //Velocidad de la pelota
    public float velocidad = 30.0f;
    //Audio Source
    AudioSource fuenteDeAudio;
    //Clips de audio
    public AudioClip audioGol, audioRaqueta, audioRebote, audioInicio, audioFin;
    //Contadores de goles
    public int golesIzquierda = 0;
    public int golesDerecha = 0;
    //Cajas de texto de los contadores
    public Text contadorIzquierda;
    public Text contadorDerecha;

    // Use this for initialization
    void Start()
    {
        GetComponent<Timer>().iniciaTiempo();
        //Velocidad inicial hacia la derecha
        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
        //Recupero el componente audio source;
        fuenteDeAudio = GetComponent<AudioSource>();
        fuenteDeAudio.clip = audioInicio;
        fuenteDeAudio.Play();
        //Pongo los contadores a 0
        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();
    }
    //Se ejecuta si choco con la raqueta
    void OnCollisionEnter2D(Collision2D micolision)
    {
        //Si me choco con la raqueta izquierda
        if (micolision.gameObject.name == "Raqueta Izquierda")
        {
            //Valor de x
            int x = 1;
            //Valor de y
            int y = direccionY(transform.position,
            micolision.transform.position);
            //Vector de direcci�n
            Vector2 direccion = new Vector2(x, y);
            //Aplico la velocidad a la bola
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
            //Reproduzco el sonido de la raqueta
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }
        //Si me choco con la raqueta derecha
        else if (micolision.gameObject.name == "Raqueta Derecha")
        {
            //Valor de x
            int x = -1;
            //Valor de y
            int y = direccionY(transform.position,
            micolision.transform.position);
            //Vector de direcci�n
            Vector2 direccion = new Vector2(x, y);
            //Aplico la velocidad a la bola
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
            //Reproduzco el sonido de la raqueta
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }
        //Para el sonido del rebote
        if (micolision.gameObject.name == "Arriba" ||
        micolision.gameObject.name == "Abajo")
        {
            //Reproduzco el sonido del rebote
            fuenteDeAudio.clip = audioRebote;
            fuenteDeAudio.Play();
        }
    }
    //Calculo la direcci�n de Y
    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        if (posicionBola.y > posicionRaqueta.y)
        {
            return 1;
        }
        else if (posicionBola.y < posicionRaqueta.y)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    //Reinicio la posici�n de la bola
    public void reiniciarBola(string direccion)
    {
        //Posici�n 0 de la bola
        transform.position = Vector2.zero;
        //Vector2.zero es lo mismo que new Vector2(0,0);
        //Velocidad inicial de la bola
        velocidad = golesDerecha + golesIzquierda + 31;
        //Velocidad y direcci�n
        if (direccion == "Derecha")
        {
            //Incremento goles al de la derecha
            golesDerecha++;
            //Lo escribo en el marcador
            contadorDerecha.text = golesDerecha.ToString();
            //Reinicio la bola
            GetComponent<Rigidbody2D>().velocity = Vector2.right *
            velocidad;
            //Vector2.right es lo mismo que new Vector2(1,0)
        }
        else if (direccion == "Izquierda")
        {
            //Incremento goles al de la izquierda
            golesIzquierda++;
            //Lo escribo en el marcador
            contadorIzquierda.text = golesIzquierda.ToString();
            //Reinicio la bola
            GetComponent<Rigidbody2D>().velocity = Vector2.left *
            velocidad;
            //Vector2.right es lo mismo que new Vector2(-1,0)
        }
        if (!tryFin())
        {
            fuenteDeAudio.clip = audioGol;
            fuenteDeAudio.Play();
        }
        else
        {
            if (golesDerecha > golesIzquierda)
                GameManager.Ganador = "Derecha";
            else if (golesDerecha < golesIzquierda)
                GameManager.Ganador = "Izquierda";
            else
                GameManager.Ganador = "Empate";

            SceneManager.LoadScene("Inicio");
        }

    }
    bool tryFin()
    {
        if (golesDerecha == 5 || golesIzquierda == 5)
        {
            return true;
        }
        if (GetComponent<Timer>().Crono.text == "03:00")
        {
            return true;
        }

        return false;

    }
    void FixedUpdate()
    {
        //Incremento la velocidad de la bola
        velocidad = velocidad + 0.1f;
    }
}