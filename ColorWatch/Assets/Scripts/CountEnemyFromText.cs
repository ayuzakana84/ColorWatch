using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountEnemyFromText : MonoBehaviour
{
    public TextMeshProUGUI normalText;
    public TextMeshProUGUI shyText;
    public TextMeshProUGUI boarText;
    public TextMeshProUGUI octopusText;
    public TextMeshProUGUI tutorialText;

    private string normal;
    private string shy;
    private string boar;
    private string octopus;
    private string tutorial;

    // Update is called once per frame
    void Update()
    {
        normal= normalText.text;
        shy= shyText.text;
        boar= boarText.text;
        octopus= octopusText.text;
        tutorial= tutorialText.text;

        if (normal == "�~0" && shy == "�~0" && boar == "�~0" && octopus == "�~0" && tutorial == "�~0")
        {
            SceneManager.LoadScene("ClearScene");
        }
    }
}
