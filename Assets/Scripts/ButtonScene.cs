using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    [SerializeField]
    public string newScene;


    public void SceneTransition()
    {
        SceneManager.LoadScene(newScene);
    }
}
