using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class retryButton : MonoBehaviour
{
    public void PushRetryGame()
    {
        SceneManager.LoadScene("meiroScene");
    }
}