using NUnit.Framework;
using System;
using System.Collections.Generic;

public interface IService {
    // Executed when the service is registered.
    public void Initialize();
    public void Dispose();
}

public class MyClass : IService {

    public void Initialize() {
    }
    public void Dispose() {
    }
}

public class MyOtherClass : IService {

    public void Initialize() {
    }
    public void Dispose() {
    }
}

public class TestClass{

    List<IService> services = new List<IService>();

    public void Test() {
        var instanceOne  = new MyClass();
        var instanceTwo = new MyOtherClass();

        services.Add(instanceOne);
        services.Add(instanceTwo);

        foreach(var service in services) {
            service.Initialize();
        }
    }

}

/// <summary>
/// Generic Service Locator which is a singleton.
/// </summary>
internal sealed class ServiceLocator : Singleton<ServiceLocator> {
    private ServiceLocator() { }

    private readonly Dictionary<Type, object> registry = new();

    public void RegisterService<T>(T serviceInstance) where T : IService {
        registry[typeof(T)] = serviceInstance;
        serviceInstance.Initialize();
    }

    public T GetService<T>() where T : IService {
        T serviceInstance = (T)registry[typeof(T)];
        return serviceInstance;
    }

    public void RemoveService<T>() where T : IService {
        T serviceInstance = (T)registry[typeof(T)];
        serviceInstance.Dispose();
        registry.Remove(typeof(T));
    }
}
