using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _lanes;

    [SerializeField]
    private List<GameObject> _enemies;

    [SerializeField]
    private TMP_Text _lives;

    [SerializeField]
    private TMP_Text _score;

    [SerializeField]
    private TMP_Text _highscore;

    [SerializeField]
    private int lives;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private AudioSource _damageSound;

    [SerializeField]
    private AudioSource _enemyDeathSound;

    private int randLane;

    private int randEnemy;

    private int score;

    public bool IsPlaying;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
        _lives.text = "Lives: " + lives.ToString();
        _score.text = "Score: " + score.ToString();
        IsPlaying = true;
    }


    private IEnumerator spawnEnemy()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(3);
        }
        
    }

    public void SpawnEnemy()
    {
        randLane = Random.Range(0, _lanes.Count);
        randEnemy = Random.Range(0, _enemies.Count);
        GameObject enemy = Instantiate(_enemies[randEnemy], (_lanes[randLane]).transform);
        enemy.transform.position = _lanes[randLane].transform.position;

    }

    public void GameOver()
    {
        StopCoroutine(spawnEnemy());
        CheckScore();
        gameOverScreen.SetActive(true);

    }

    public void IncreaseScore()
    {
        if(IsPlaying == true)
        {
            _enemyDeathSound.Play();
            score += 100;
            _score.text = "Score: " + score.ToString();
        }

    }

    public void LoseLife()
    {
        if(lives > 0)
        {
            lives--;
            _lives.text = "Lives: " + lives.ToString();
            _damageSound.Play();
        }
        if(lives == 0)
        {
            GameOver();
            IsPlaying = false;
        }
    }

    private void CheckScore()
    {
        int oldScore = PlayerPrefs.GetInt("Highscore");
        if(oldScore > score)
        {
            _highscore.text = "Highscore: " + oldScore.ToString();
            return;
        }
        if(score > oldScore)
        {
            SaveGame();
            _highscore.text = "Highscore: " + score.ToString() + " [NEW HIGH SCORE!]";
            return;
        }
    }
    private void SaveGame()
    {
        PlayerPrefs.SetInt("Highscore", score);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
