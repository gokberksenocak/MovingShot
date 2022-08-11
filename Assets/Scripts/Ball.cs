using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager _gamemanager;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject block2;
    [SerializeField] private GameObject basket;
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private AudioClip[] voices;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Basket"))
        {
            block.SetActive(false);
            block2.SetActive(false);
            _gamemanager.Basket();
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            AudioSource.PlayClipAtPoint(voices[0], transform.position);
            panel.SetActive(true);
            Time.timeScale = 0;
        }
        if (other.gameObject.CompareTag("Item1"))
        {
            basket.transform.localScale = new Vector3(60f, 60f, 60f);
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(voices[2], transform.position);
            Invoke("Basket_Normal", 10f);
        }
        if (other.gameObject.CompareTag("Item2"))
        {
            transform.localScale = new Vector3(.4f, .4f, .4f);
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(voices[2], transform.position);
            Invoke("Ball_Normal", 10f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(voices[1], transform.position);
        if (collision.gameObject.CompareTag("Platform"))
        {
            block.SetActive(true);
            block2.SetActive(true);
            AudioSource.PlayClipAtPoint(voices[1], transform.position);
        }
    }
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, left.position.x, right.position.x);
        transform.position = pos;
    }
    void Basket_Normal()
    {
        basket.transform.localScale = new Vector3(50f, 50f, 50f);
    }
    void Ball_Normal()
    {
        transform.localScale = new Vector3(.55f, .55f, .55f);
    }
}