using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour {

    public GameObject MazeGenPrefab;
    bool hasSpawnedMazeGen = false;
    GameObject instance;
    public 
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);


	}

    // Update is called once per frame
    void Update()
    {
        if (hasSpawnedMazeGen == false && Application.loadedLevel == 1)
        {
            instance = Instantiate(MazeGenPrefab);
            instance.gameObject.SetActive(true);
            hasSpawnedMazeGen = true;
        }

        else if (hasSpawnedMazeGen == true && Application.loadedLevel == 2)
        {
            Destroy(instance);
            Destroy(GameObject.Find("LevelManager"));
            hasSpawnedMazeGen = false;
        }

        if(Application.loadedLevel==1 && Input.GetKey(KeyCode.Escape))
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
         Application.Quit();
        #endif
        }
    }

    public void StartPlay()
    {
        MazeGen tmp = MazeGenPrefab.GetComponent<MazeGen>();

        GameObject[] tmpSliders = GameObject.FindGameObjectsWithTag("Slider");

        tmp.width = (int)tmpSliders[0].GetComponent<Slider>().value;
        tmp.height = (int)tmpSliders[1].GetComponent<Slider>().value;

        Application.LoadLevel(1);
    }

    public void MainMenu()
    {
        Destroy(GameObject.Find("LevelManager"));
        hasSpawnedMazeGen = false;
        Application.LoadLevel(0);
    }

    public void QuitGame()
    {
        
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
         Application.Quit();
    #endif
    }
}
