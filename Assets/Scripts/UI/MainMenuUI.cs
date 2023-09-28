using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startGame;
    [SerializeField] private Button endGame;

    private void Awake()
    {
        startGame.onClick.AddListener(() => 
        {
            Loader.Load(Scenes.Prototype);
        }
        );
        startGame.onClick.AddListener(() =>
        {
            Application.Quit();
        }
        );
    }



    
}
