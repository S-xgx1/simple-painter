using Cysharp.Threading.Tasks;
using Painter;
using UnityEngine;

static class UILoader
{
    public static async UniTask<T> LoadPrefab<T>(T prefab, RectTransform parent = null) where T : Component
    {
        var async = Object.InstantiateAsync(prefab, parent ?? MainManager.Instance.UIRoot);
        await async;
        var instance = async.Result[0];
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localRotation = Quaternion.identity;
        return instance;
    }
}