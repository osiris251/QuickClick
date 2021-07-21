using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float minFoce = 16, maxForce = 18, torque = 10, xRange = 4, yRange = 6;

    private GameManager gameManager;

    public int pointValue;

    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomPosition();

        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Genera un vector aleatorio en 3D
    /// </summary>
    /// <returns>Fuerza aleatoria hacia arriba</returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minFoce, maxForce);
    }

    /// <summary>
    /// Genera un nuero aleatorio
    /// </summary>
    /// <returns>Numero aleatorio entre -torque(-10) y torque(10)</returns>
    private float RandomTorque()
    {
        return Random.Range(-torque, torque);
    }

    /// <summary>
    /// Genera una posición aleatoria
    /// </summary>
    /// <returns>Posición aleatoria en 3d, con la cordenada z = 0</returns>
    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), -yRange);
    }

    private void OnMouseOver()
    {
        if (gameManager.gameStates == GameManager.GameStates.inGame)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Good"))
            {
                gameManager.GameOver();
            }
        }
    }
}
