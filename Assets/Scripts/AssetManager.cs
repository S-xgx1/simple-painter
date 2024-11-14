using PunctualSolutions.Tool.Singleton;
using UnityEngine;

namespace Painter
{
    [SingletonMono]
    public partial class AssetManager : MonoBehaviour
    {
        public void OnSingletonInit()
        {
        }

        public void Dispose()
        {
        }

        static void InAwake()
        {
        }

        [field: SerializeField]
        public DrawPanel DrawPanelPrefab { get; private set; }

        [field: SerializeField]
        public GuessPanel GuessPanelPrefab { get; private set; }

        [field: SerializeField]
        public LoginPanel LoginPanelPrefab { get; private set; }

        [field: SerializeField]
        public TaskSelectPanel TaskSelectPanelPrefab { get; private set; }

        [field: SerializeField]
        public WordStatisticsPanel WordStatisticsPanelPrefab { get; private set; }

        [field: SerializeField]
        public WordStatisticsItem WordStatisticsItemPrefab { get; private set; }

        [field: SerializeField]
        public UserStatisticPanel UserStatisticPanelPrefab { get; private set; }

        [field: SerializeField]
        public UserStatisticItem UserStatisticItemPrefab { get; private set; }

        [field: SerializeField]
        public DrawDetailWindow DrawDetailWindowPrefab { get; private set; }

        [field: SerializeField]
        public DrawDetailItem DrawDetailItemPrefab { get; private set; }

        [field: SerializeField]
        public GuessDetailWindow GuessDetailWindowPrefab { get; private set; }

        [field: SerializeField]
        public GuessDetailItem GuessDetailItemPrefab { get; private set; }

        [field: SerializeField]
        public UserDrawDetailWindow UserDrawDetailWindowPrefab { get; private set; }

        [field: SerializeField]
        public UserDrawDetailItem UserDrawDetailItemPrefab { get; private set; }

        [field: SerializeField]
        public UserGuessDetailWindow UserGuessDetailWindowPrefab { get; private set; }

        [field: SerializeField]
        public UserGuessDetailItem UserGuessDetailItemPrefab { get; private set; }

        [field: SerializeField]
        public AddWordPanel AddWordPanelPrefab { get; set; }
    }
}