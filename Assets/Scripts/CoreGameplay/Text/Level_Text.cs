using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Level_Text : MonoBehaviour
{
    private TMP_Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        levelText = GetComponent<TMP_Text>();
        levelText.text = $"Level <color=green>{SceneManager.GetActiveScene().buildIndex}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
