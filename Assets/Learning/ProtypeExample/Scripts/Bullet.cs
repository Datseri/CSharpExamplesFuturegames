using System;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed = 20f;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        rb.linearVelocity = transform.right * bulletSpeed;
    }
}
