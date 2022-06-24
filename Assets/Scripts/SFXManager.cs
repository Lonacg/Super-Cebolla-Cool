using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class SFXManager : MonoBehaviour
{
    public AudioClip on2BlockDestroyed;
    public AudioClip on3BlockBouncing;
    public AudioClip on4Shot;
    public AudioClip on5EnemyDyingByShut;
    public AudioClip on6EnemyDyingByJump;
    public AudioClip on7PowerupLeaving; //power up saliendo
    public AudioClip on8Jump;
    public AudioClip on9PlayerWinning;
    public AudioClip on10PlayerDying; // -> ya estaba el evento OnPlayerDie, asi que uso ese
    public AudioClip on11Fireworks;
    public AudioClip on12CoinDestroying;
    public AudioClip on13BulletDestoyingByWall; //disparo al chocar contra una pared
    //public AudioClip on14 MainMenu;
    public AudioClip on15PlayerGrowingUp; //Player cogiendo un Powerup 
    public AudioClip on16PlayerDecreasing; //Player recibiendo daño
    public AudioClip on17Last100Seg; //ultimos 100 segundos

    public AudioClip on18PlayerRunning; // cuando pulsa shift
    public AudioClip on19PressingPauseButton;
    public AudioClip on20PressingContinueButton;
    public AudioClip on21VictoryTurn;
    public AudioClip on22VictoryJump;






    public AudioSource audioSource; //un audio source solo puede reproducir un sonido
    private AudioClip previousAudioClip;
    private float previousACTimeStamp;

    private void OnEnable()
    {

        Player.OnShot+=OnShot;
        Player.OnJump+=OnJump;
        Player.OnPlayerDying+=OnPlayerDying; //On10PlayerDying
        Player.OnPlayerGrowingUp+=OnPlayerGrowingUp;
        Player.OnPlayerDecreasing+=OnPlayerDecreasing;
        Player.OnPlayerRunning+=OnPlayerRunning; 
        FinalFlag.OnPlayerWinning+=OnPlayerWinning;  //On9Victory;
        Carapace.OnEnemyDyingByJump+=OnEnemyDyingByJump;
        Enemy.OnEnemyDyingByJump+=OnEnemyDyingByJump;
        Enemy.OnEnemyDyingByShut+=OnEnemyDyingByShut;
        Coin.OnCoinDestroying+=OnCoinDestroying;    
        Block.OnBlockBouncing+=OnBlockBouncing;  
        DestBlock.OnBlockDestroyed+=OnBlockDestroyed;  
        CoinBlock.OnCoinDestroying+=OnCoinDestroying;
        PowerupBlock.OnPowerupLeaving+=OnPowerupLeaving;
        Bullet.OnBulletDestoyingByWall+=OnBulletDestoyingByWall;
        StageManager.OnLast100Seg+=OnLast100Seg;
        StageManager.OnFireworks+=OnFireworks;
        StageManager.OnVictoryTurn+=OnVictoryTurn;
        StageManager.OnVictoryJump+=OnVictoryJump;
        UIManager.OnPressingPauseButton+=OnPressingPauseButton;
        UIManager.OnPressingContinueButton+=OnPressingContinueButton;


    }
    private void OnDisable()
    {
        
        Player.OnShot-=OnShot;
        Player.OnJump-=OnJump;
        Player.OnPlayerDying-=OnPlayerDying; //On10PlayerDying
        Player.OnPlayerGrowingUp-=OnPlayerGrowingUp;
        Player.OnPlayerDecreasing-=OnPlayerDecreasing;
        Player.OnPlayerRunning-=OnPlayerRunning;
        FinalFlag.OnPlayerWinning-=OnPlayerWinning; //On9Victory;
        Carapace.OnEnemyDyingByJump-=OnEnemyDyingByJump;
        Enemy.OnEnemyDyingByJump-=OnEnemyDyingByJump;
        Enemy.OnEnemyDyingByShut-=OnEnemyDyingByShut;
        Coin.OnCoinDestroying-=OnCoinDestroying;
        Block.OnBlockBouncing-=OnBlockBouncing;
        DestBlock.OnBlockDestroyed-=OnBlockDestroyed;
        CoinBlock.OnCoinDestroying-=OnCoinDestroying;
        PowerupBlock.OnPowerupLeaving-=OnPowerupLeaving;
        Bullet.OnBulletDestoyingByWall-=OnBulletDestoyingByWall;
        StageManager.OnLast100Seg-=OnLast100Seg;
        StageManager.OnFireworks-=OnFireworks;
        StageManager.OnVictoryTurn-=OnVictoryTurn;
        StageManager.OnVictoryJump-=OnVictoryJump;

        UIManager.OnPressingPauseButton-=OnPressingPauseButton;
        UIManager.OnPressingContinueButton-=OnPressingContinueButton;
    }


    private void OnShot()
    {
        PlaySFX(on4Shot,0.25f);
    }
    private void OnJump()
    {
        PlaySFX(on8Jump,0.09f);
    }
    private void OnPlayerDying() // On10PlayerDying
    {
        PlaySFX(on10PlayerDying,0.25f);
    }
    private void OnPlayerGrowingUp() 
    {
        PlaySFX(on15PlayerGrowingUp,0.4f);
    }
    private void OnPlayerDecreasing() 
    {
        PlaySFX(on16PlayerDecreasing,0.5f);
    }
    private void OnPlayerRunning() 
    {
        PlaySFX(on18PlayerRunning,0.08f);
    }
    private bool OnPlayerWinning() 
    {
        PlaySFX(on9PlayerWinning,0.5f);
        bool hasWon=true; //lo pongo porque StageManager necesita el bool, y si no lo pongo aqui se queja
        return hasWon;
    }
    private void OnEnemyDyingByJump() 
    {
        PlaySFX(on6EnemyDyingByJump,0.5f);
    }
    private void OnEnemyDyingByShut() 
    {
        PlaySFX(on5EnemyDyingByShut,0.25f);
    }
    private void OnCoinDestroying() 
    {
        PlaySFX(on12CoinDestroying,0.5f);
    }
    private void OnBlockDestroyed()
    {
        PlaySFX(on2BlockDestroyed,0.5f);
    }
    private void OnBlockBouncing()
    {
        PlaySFX(on3BlockBouncing,0.5f);
    }
    private void OnPowerupLeaving()
    {
        PlaySFX(on7PowerupLeaving,0.1f);
    }
    private void OnBulletDestoyingByWall()
    {
        PlaySFX(on13BulletDestoyingByWall,0.5f);
    }
    private void OnLast100Seg()
    {
        PlaySFX(on17Last100Seg,0.1f);
    }
    private void OnFireworks()
    {
        PlaySFX(on11Fireworks,0.5f);
    }
    private void OnVictoryTurn()
    {
        PlaySFX(on21VictoryTurn,0.5f);
    }
    private void OnVictoryJump()
    {
        PlaySFX(on22VictoryJump,0.5f);
    }
    private void OnPressingPauseButton()
    {
        PlaySFX(on19PressingPauseButton,0.1f);
    }
    private void OnPressingContinueButton()
    {
        PlaySFX(on20PressingContinueButton,0.1f);
    }


    /* PEGGLE

    public AudioClip onBumperHit;
    //Spublic AudioClip[] onBumperActivated;
    public AudioClip onBumperDestroyed;
    
    
    private void OnEnable()
    {
        Bumper.OnBumperHit+= OnBumperHit;
        Bumper.OnBumperActivated+= OnBumperActivated;
        Bumper.OnBumperDestroyed+=OnBumperDestroyed;
    }

    private void OnBumperHit(Bumper bumper)
    {
        audioSource.pitch=Random.Range(0.9f,1.1f); //con el pitch hacemos un cambio de volumen a los sonidos para que parezcan diferentes
        PlaySFX(onBumperHit,0.5f);
    }
    private void OnBumperActivated(Bumper bumper)
    {
        int randomIndex=Random.Range(0, onBumperActivated.Length);
        PlaySFX(onBumperActivated[randomIndex], 0.5f);
    }
    private void OnBumperDestroyed(Bumper bumper)
    {
        PlaySFX(onBumperDestroyed);
    }

    private void OnDisable()
    {
        Bumper.OnBumperHit-=OnBumperHit;
        Bumper.OnBumperActivated-=OnBumperActivated;
        Bumper.OnBumperDestroyed-=OnBumperDestroyed;
    }
    void Start()
    {
        //Para cuando cambiamos al menu principal o para reiniciar la musica... pero para efectos no, porque se cortaria a medias si se produce otro
        //audioSource.clip= onBumperActivated;
        //audioSource.Play();
        
    }*/

    public void PlaySFX(AudioClip audioClip, float volume=1)
    {
        if (previousAudioClip==audioClip) //Para que 2 sonidos no puedan sonar en el mismo momento y se acople el sonido(se multiplicaria el volumen de ese sonido)
        {
            if(Time.time-previousACTimeStamp<0.05f)
            {
                return;
            }
        }
        previousAudioClip=audioClip;
        previousACTimeStamp=Time.time;

        audioSource.PlayOneShot(audioClip,volume);
    }
}

