using System;
using UnityEngine;

public class SimpleStartExampleOne : MonoBehaviour {
    public MonoServiceOne monoServiceOne;
    public MonoServiceTwo monoServiceTwo;
    public static SimpleStartExampleOne Instance { get; set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start() { }

    private void Update() { }
}
