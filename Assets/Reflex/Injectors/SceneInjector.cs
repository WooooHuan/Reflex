using Reflex.Core;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace Reflex.Injectors
{
    internal static class SceneInjector
    {
        internal static void Inject(Scene scene, Container container)
        {
            using var pooledObject1 = ListPool<GameObject>.Get(out var rootGameObjects);
            scene.GetRootGameObjects(rootGameObjects);

            for (var i = 0; i < rootGameObjects.Count; i++)
            {
                InjectHierarchy(rootGameObjects[i].transform, container);
            }
        }

        private static void InjectHierarchy(Transform current, Container container)
        {
            if (current == null)
                return;

            if (current.TryGetComponent<InjectionBoundary>(out _))
                return;

            GameObjectInjector.InjectObject(current.gameObject, container);

            for (var i = 0; i < current.childCount; i++)
            {
                InjectHierarchy(current.GetChild(i), container);
            }
        }
    }
}
