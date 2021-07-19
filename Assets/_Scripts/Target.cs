using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float minFoce = 14, maxForce = 18, torque = 10, xRange = 4, yRange = 6;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomPosition();
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
        }
    }
}
