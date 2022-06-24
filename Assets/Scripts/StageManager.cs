using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class StageManager : MonoBehaviour
{

    public delegate void Last100Seg(); 
    public static event Last100Seg OnLast100Seg;

    public delegate void Fireworks(); 
    public static event Fireworks OnFireworks;

    public delegate void VictoryTurn(); 
    public static event VictoryTurn OnVictoryTurn;

    public delegate void VictoryJump(); 
    public static event VictoryJump OnVictoryJump;


    private float timeToPlay=400;
    private int restOfTime;

    public TextMeshProUGUI timeLabel;



    public GameObject player;
    public GameObject playerModel;
    public GameObject finalFlag;
    public GameObject confetiParticlesPrefab;

    private bool hasWon=false;
    //public bool hasDied=false;
    private bool finalAnimationActive=false;

    private float heightPlayer=0.5f;
    private bool warning=false;



    void Start()
    {
        
    }

    void Update()
    {
        if(hasWon==false)
        {
            restOfTime= TimerCountdown();
            if(restOfTime==100f && warning==false)
            {
                warning=true;
                if (OnLast100Seg!=null)
                    OnLast100Seg();
            }
        }
        timeLabel.text= $"{restOfTime}";
        if (player.transform.position.x>=263) 
            SceneManager.LoadScene("MainMenu");

    }
    

    private void OnEnable()
    {
        FinalFlag.OnPlayerWinning += OnPlayerWinning;
        Player.OnPlayerDying+=OnPlayerDying;
    }

    private void OnDisable()
    {
        FinalFlag.OnPlayerWinning -= OnPlayerWinning;
        Player.OnPlayerDying-=OnPlayerDying;

    }

    public void OnPlayerDying()
    {
        /*CORRUTINA: 
            - PARAR EL TIEMPO
            - OSCURECER PANTALLA HACIA EL CENTRO
            - PONER MUSICA DE DERROTA
            - CARGAR LA ESCENA: SceneManager.LoadScene("SCCGameScene");
            - REANUDAR EL TIEMPO

        */
    StartCoroutine(WaitToDie());

    }
    IEnumerator WaitToDie()
    {
        player.transform.gameObject.GetComponent<Player>().enabled=false;
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene("SCCGameScene");
        Time.timeScale=1;
        yield return 0;
    }

    public bool OnPlayerWinning()
    {
        hasWon=true;
        player.GetComponent<Player>().enabled = false;
        player.GetComponent<Rigidbody>().velocity=Vector3.zero;
        player.GetComponent<Rigidbody>().useGravity=false;

        finalFlag.transform.gameObject.GetComponent<Collider>().enabled = false;

        //LANZAR DESDE AQUI EL SONIDO DE VICTORIA
        heightPlayer=player.transform.localScale.y;
        if(finalAnimationActive==false)
        {
            finalAnimationActive=true;
            StartCoroutine(PlayerGoingDown());
        }
        return hasWon;
    }


    public int TimerCountdown()
    {
        timeToPlay=timeToPlay - Time.deltaTime;
        restOfTime= Mathf.RoundToInt(timeToPlay);
        return restOfTime;
    }



    IEnumerator PlayerGoingDown() //En la palmera de victoria
    {
        Vector3 desiredPosition = new Vector3(236.75f,1+heightPlayer,0);
        float elapsedTime=0;
        while(Mathf.Round(player.transform.position.y*100f)>Mathf.Round(desiredPosition.y*100f))
        {
            player.transform.position=Vector3.Lerp(player.transform.position, desiredPosition,elapsedTime/20);
            elapsedTime+=Time.deltaTime;
            yield return 0; 
        }

        StartCoroutine(Turning());
        yield return 0;
    }

    IEnumerator Turning()
    {
        if (OnVictoryTurn!=null)
            OnVictoryTurn();
        Vector3 desiredPosition = new Vector3(238.25f,1+heightPlayer,0);
        Quaternion desiredRotation= Quaternion.Euler(0,180,0);
        float elapsedTime=0;
        while(Mathf.Round(player.transform.position.x*100f)<Mathf.Round(desiredPosition.x*100f))
        {
            player.transform.position=Vector3.Lerp(player.transform.position, desiredPosition,elapsedTime/20);
            player.transform.rotation=Quaternion.Lerp(player.transform.rotation, desiredRotation,elapsedTime/20);
            elapsedTime+=Time.deltaTime;
            yield return 0; 
        }
        player.transform.rotation=desiredRotation;
        StartCoroutine(Jumping());
        yield return 0;
    }

    IEnumerator Jumping()
    {
        if (OnVictoryJump!=null)
            OnVictoryJump();
        Rigidbody rigidbody= player.GetComponent<Rigidbody>();
        rigidbody.useGravity=true;
        rigidbody.AddForce(new Vector3(2,3.5f,0)*2, ForceMode.Impulse);
        float elapsedTime=0;
        while(Mathf.Round(player.transform.position.y*100f)>heightPlayer*100f)
        {
            if(player.transform.position.y<3.11f+heightPlayer&& rigidbody.velocity.y>0)
                playerModel.transform.rotation=Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Euler(180,0,0),elapsedTime/3);
            else if(player.transform.position.y<3.41f+heightPlayer&& rigidbody.velocity.y>0)
                playerModel.transform.rotation=Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Euler(0,0,-180),elapsedTime/3);
            else
                playerModel.transform.rotation=Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Euler(0,180,0),elapsedTime/3);

            elapsedTime+=Time.deltaTime;
            yield return 0;
        }
        player.GetComponent<Rigidbody>().velocity=Vector3.zero;

        StartCoroutine(LaunchFireworks());
        yield return 0;
    }

    IEnumerator LaunchFireworks()
    {
        yield return new WaitForSeconds(0.5f); 
        GameObject fireworks=Instantiate(confetiParticlesPrefab, player.transform.position, Quaternion.identity);
        if (OnFireworks!=null)
            OnFireworks();
        yield return new WaitForSeconds(2); 
        StartCoroutine(GoingOut());
        yield return 0;       
    }

    IEnumerator GoingOut()
    {
        playerModel.transform.rotation=Quaternion.Euler(0,90,0);
        float elapsedTime=0;
        Vector3 desiredPosition=new Vector3(268f,heightPlayer,0);
        while(player.transform.position.x<268f)
        {
            player.transform.position=Vector3.Lerp(player.transform.position,desiredPosition,elapsedTime/53);
            elapsedTime+=Time.deltaTime;
            yield return 0;
        }
        //LLAMAR AQUI A LA UI PARA EL CAMBIO DE PANTALLA
        Debug.Log("Fin");
        yield return 0;
    }



}
