using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public PlayerPickup player;
    // Start is called before the first frame update
    void Start()
    {
        TokenManager.Instance.init();
        ItemManager.Instance.init();
        ItemTokenCombination.Instance.init();
        QuestManager.Instance.init();
    }

        // Update is called once per frame
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
