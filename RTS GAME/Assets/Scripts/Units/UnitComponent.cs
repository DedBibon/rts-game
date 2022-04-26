using UnityEngine;
using Units.UnitSettings;

namespace Units
{
    public class UnitComponent : MonoBehaviour
    {
        [SerializeField] public Settings unitySetting;

        public static Settings UnitySetting => ScriptableObject.CreateInstance<Settings>();

    }
}