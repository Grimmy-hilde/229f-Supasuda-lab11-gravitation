using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

public class Gravitational: MonoBehaviour
{
    public static List<Gravitational> otherObj;
    private Rigidbody rb;
    const float G = 0.006674f;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObj == null)
        {
            otherObj = new List<Gravitational>();
        }
        otherObj.Add(this);

        if (!planet)
        { rb.AddForce(Vector3.left * orbitSpeed); }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Gravitational obj in otherObj)
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }

    void Attract(Gravitational other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;

        float distance = direction.magnitude;
        if (distance == 0.0f) return;

        float forceMagnitude = G * (rb.mass * otherRb.mass) / math.pow(distance , 2);
        Vector3 gravitationForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(gravitationForce);
    }
}
