using UnityEngine;
using System.Collections;

namespace Units.UnitSettings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Units/Settings")]
    public class Settings : ScriptableObject
    {
        //Main characteristics  
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float radiusAttack;
        [SerializeField] private float health;
        [SerializeField] private float armor;

        [SerializeField] private GameObject prefabs;
        [SerializeField] private Team team;

        [SerializeField]
        public enum Team
        {
            Green,
            Red
        }

        public GameObject Prefabs => prefabs;
        public Team GetTeam => team;

    }
}