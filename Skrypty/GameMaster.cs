using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameMaster : MonoBehaviour {

    public static GameMaster gm;

    [SerializeField]
    private GameObject gameOverUI;


    public void EndGame()
    {
        Debug.Log("koniec");
        gameOverUI.SetActive(true);
    }

  
    public static void KillPlayer(Player player)
    {
        Destroy (player.gameObject);
        gm.EndGame();
    }
}
