using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {

       
        public int maxHealth = 3;
        private int _currHealth;
        public int currHealth
        {
            get { return _currHealth; }
            set { _currHealth = Mathf.Clamp(value, 0, maxHealth); }
        }
        
        public void Init()
        {
            currHealth = maxHealth;
        }
    }

    public PlayerStats stats = new PlayerStats();

    public int KillZoneDown = -20;

    public string deathSound="death";

    private AudioManager audioManager;

    [SerializeField]
    private GameObject gameOverUI;




    private void Start()
    {
        stats.Init();

        audioManager = AudioManager.instance;
        if (audioManager==null)
        {
            Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }
    }

    private void Update()
    {
        if (transform.position.y <= KillZoneDown)
            DamagePlayer(6);
    }

    public void DamagePlayer(int damage)
    {
        stats.currHealth -= damage;
        if (stats.currHealth <= 0)
        {
            audioManager.PlaySound(deathSound);
            gameOverUI.SetActive(true);
            GameMaster.KillPlayer(this);
        }
    }
}
