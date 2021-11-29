using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Axel Bauer
/// </summary>
public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public AudioSource soundtrack;
    public AudioSource soundeffects;
    public GameObject firework;
    public GameObject endText;

    private GameObject[] hideables;
    private bool gameEnd;
    private bool showGameEndText;

    void Start()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        var isPlayableScene = false;

        foreach(var scene in StringConstants.LABYRINTH_SCENES)
        {
            if (scene == currentScene)
            {
                isPlayableScene = true;
            }
        }

        if (isPlayableScene)
        {
            GameObject player = GetPlayer();
            player.GetComponent<Character>().ResetCharacter();
        }

        //Intentionally left empty
        gameEnd = false;
        showGameEndText = true;
        Play();

        //GameWon();//TODO delete this
    }

    void Update()
    {
        if (gameEnd)
        {
            hideables = GetHideable();
            if (hideables == null)
            {
                Debug.LogError("GameController.cs: Not able to fetch hideables");
            } else
            {
                if (hideables != null && hideables[0].transform.position.y < 0 && showGameEndText)
                {
                    ShowGameEndText();
                }

                if (hideables != null && hideables[0].transform.position.y > -5)
                {
                    foreach (GameObject go in hideables)
                    {
                        go.transform.SetPositionAndRotation(new Vector3(go.transform.position.x, go.transform.position.y - 0.1f * Time.deltaTime, go.transform.position.z), go.transform.rotation);

                    }
                } 
            }
        }
    }

    public static GameController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameController>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public void Play()
    {
        if (soundtrack != null)
        {
            soundtrack.Play();
        }

        if (soundeffects != null) 
        {
            soundeffects.Play();
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(StringConstants.LABYRINTH_SCENES[0]);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(StringConstants.GAME_OVER_SCENE);

        GameObject player = GetPlayer();
        player.GetComponent<Character>().ResetCharacter();
    }

    public void GameWon()
    {
        Debug.Log("Congrats on winning the game!");
        hideables = GetHideable();
        gameEnd = true;

        soundeffects.Pause();

        GameObject[] enemies = GetEnemy();

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        Vector3 pos = new Vector3(-10, 0,0);
        Quaternion rot = Quaternion.Euler(0, 0 ,0);
        Instantiate(firework, pos, rot);

        soundeffects.Pause();
        soundtrack.Pause();
    }

    public static GameObject GetPlayer()
    {
        GameObject player = GameObject.Find(StringConstants.PLAYER);

        if (player == null)
        {
            player = GameObject.Find(StringConstants.PLAYER);
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(StringConstants.PLAYER);
        }
        if (player == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find player!");
        }

        return player;
    }

    public static GameObject GetMoveablePlayer()
    {
        GameObject moveablePlayer = GameObject.Find(StringConstants.VR_CAMERA);

        if (moveablePlayer == null)
        {
            moveablePlayer = GameObject.Find("FollowHead");
        }

        if (moveablePlayer == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find moveablePlayer!");
        }

        return moveablePlayer;
    }

    public GameObject[] GetHideable()
    {
        return GameObject.FindGameObjectsWithTag("HideableObject");
    }

    public GameObject[] GetEnemy()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemy == null)
        {
            enemy[0] = GameObject.Find("Enemy");
        }

        if (enemy == null)
        {
            enemy[0] = GameObject.Find("Red");
        }


        if (enemy == null)
        {
            enemy[0] = GameObject.Find("AI");
        }

        if (enemy == null)
        {
            Debug.LogError("Enemy.cs: Couldn't find player!");
        }

        return enemy;
    }

    private void ShowGameEndText()
    {
        Instantiate(endText, new Vector3(0, 2, 1.4f), new Quaternion(0,0,0,0));
        showGameEndText = false;
    }
}