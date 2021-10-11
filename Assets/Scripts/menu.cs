using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public MusicClass music; 
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.Find("Music").GetComponent<MusicClass>();
        music.PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
