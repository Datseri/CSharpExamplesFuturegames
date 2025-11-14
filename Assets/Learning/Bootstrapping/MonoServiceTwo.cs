using System;
using UnityEngine;

public class MonoServiceTwo : MonoBehaviour, IService {
    public int rotationSpeed;
    private bool serviceLocatorInitialized;

    private void Start() {
        Debug.Log("MonoServiceTwo started from Start()");
    }

    public void Initialize() {
        serviceLocatorInitialized = true;
        Debug.Log("MonoServiceTwo started from Initialize()");
    }

    private void Update() {
        SimpleStartExampleOne.Instance?.monoServiceOne.cubeTransform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        if(serviceLocatorInitialized) {
            ServiceLocator.Instance?.GetService<MonoServiceOne>()?.cubeTransform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }
        Debug.Log("MonoServiceTwo is updating");
    }

    public void Dispose() {
        serviceLocatorInitialized = false;
    }
}
