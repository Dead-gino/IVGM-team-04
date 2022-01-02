using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainMenuController : MonoBehaviour
{

    public void startGame() {
        SceneManager.LoadScene("Assets/Scenes/Implementation 1.unity");
    }
    
    public void quitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
