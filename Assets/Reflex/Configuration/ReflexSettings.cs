using System.Collections.Generic;
using Reflex.Core;
using Reflex.Logging;
using UnityEngine;
using UnityEngine.Assertions;

namespace Reflex.Configuration
{
    internal sealed class ReflexSettings : ScriptableObject
    {
        private static ReflexSettings _instance;
        private static ResourceRequest _settingsRequest;

        public static ReflexSettings Instance
        {
            get
            {
                TryGetInstance(out var settings);

                Assert.IsNotNull(settings, "ReflexSettings not found in Resources folder.\n" +
                                           "Please create ReflexSettings using right mouse button over Resources folder, Create > Reflex > Settings.");
                return settings;
            }
        }

        internal static bool TryGetInstance(out ReflexSettings settings)
        {
            if (_instance == null)
            {
                if (_settingsRequest == null ||
                    (_settingsRequest.isDone && _settingsRequest.asset == null))
                {
                    _settingsRequest = Resources.LoadAsync<ReflexSettings>("ReflexSettings");
                }

                // Reading ResourceRequest.asset waits for an in-progress request to finish.
                _instance = (ReflexSettings)_settingsRequest.asset;
            }

            settings = _instance;
            return settings != null;
        }
        
        [field: SerializeField] public LogLevel LogLevel { get; private set; }
        [field: SerializeField] public List<ContainerScope> RootScopes { get; private set; }

        private void OnValidate()
        {
            _instance = this;
            ReflexLogger.UpdateLogLevel(LogLevel);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void InitializeReflex()
        {
            _settingsRequest = Resources.LoadAsync<ReflexSettings>("ReflexSettings");
        }
    }
}
