using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI OBJECTS")]
    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject[] spawns;
    [SerializeField] private Image[] _image;
    [SerializeField] private Sprite _sprite;

    [Header("OTHER OBJECTS")]
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private Transform _transform;
    [SerializeField] private int need;
    private Scene _scene;
    private float vector_left = -1.5f;
    private float vector_right = 1.5f;
    private int start_time = 7;
    private int count = 0;
    private float fingermove;
    void Start()
    {
        _scene = SceneManager.GetActiveScene();
    }
    void FixedUpdate()
    {
        if (Input.touchCount>0)
        {
            Touch _touch = Input.GetTouch(0);
            Vector3 touchpos = Camera.main.ScreenToWorldPoint(new Vector3(_touch.position.x, _touch.position.y, 10));
            if (_touch.phase==TouchPhase.Began)
            {
                fingermove = touchpos.x - _transform.position.x;
            }
            if (_touch.phase==TouchPhase.Moved)
            {
                if (touchpos.x - fingermove < vector_right && touchpos.x - fingermove > vector_left)
                {
                    _transform.position = Vector3.Lerp(_transform.position, new Vector3(touchpos.x- fingermove, _transform.position.y, _transform.position.z), 5f);
                } 
            }
        }
    }
    public void Basket()
    {
        count++;
        AudioSource.PlayClipAtPoint(sounds[0], transform.position);
        _image[count - 1].sprite = _sprite;
        if (count==1)
        {
            PowerUp();
        }
        if (count==need)
        {
            AudioSource.PlayClipAtPoint(sounds[1], transform.position);
            panels[1].SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void PowerUp()
    {
        int a = Random.Range(0, 3);
        int b = Random.Range(0, 2);
        if (b == 0)
        {
            items[0].SetActive(true);
            items[0].transform.position = spawns[a].transform.position;
            StartCoroutine(Timer());
        }
        if (b == 1)
        {
            items[1].SetActive(true);
            items[1].transform.position = spawns[a].transform.position;
            StartCoroutine(Timer());
        }
    }
    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            start_time--;
            if (start_time == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    items[i].SetActive(false);
                }
                break;
            }
        }
    }
    public void PauseGame()
    {
        panels[0].SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        panels[0].SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_scene.buildIndex + 1);
    }
    public void TryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_scene.buildIndex);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}