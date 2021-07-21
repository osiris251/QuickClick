using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPoints : MonoBehaviour
{

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseOver()
    {
        Destroy(gameObject);
        gameManager.UpdateScore(Random.Range(-20, 20));
    }
}
