using UnityEngine;

namespace Painter
{
    public class AssetManager : MonoSingleton<AssetManager>
    {
        [field: SerializeField] public DrawPanel       DrawPanelPrefab       { get; private set; }
        [field: SerializeField] public GuessPanel      GuessPanelPrefab      { get; private set; }
        [field: SerializeField] public LoginPanel      LoginPanelPrefab      { get; private set; }
        [field: SerializeField] public TaskSelectPanel TaskSelectPanelPrefab { get; private set; }
    }
}