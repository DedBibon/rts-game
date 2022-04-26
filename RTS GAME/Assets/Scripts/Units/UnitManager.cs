using System.Collections.Generic;
using ObjectSelection;
using UnityEngine;

namespace Units
{
    public class UnitManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> unitSelected = new List<GameObject>();
        [SerializeField] private List<GameObject> allUnitsPlayer = new List<GameObject>();

        private void Awake()
        {
            //All units are controlled by the Player
            foreach (var obj in FindObjectsOfType<UnitComponent>())
            {
                if ((int) obj.gameObject.GetComponent<UnitComponent>().unitySetting.GetTeam ==
                    (int) gameObject.GetComponent<UnitsSelection>().GetTeam)
                    allUnitsPlayer.Add(obj.gameObject);
            }
        }


        public void AddUnits(GameObject obj)
        {
            if (unitSelected.Contains(obj))
                return;

            unitSelected.Add(obj);
        }

        public void DelUnits(GameObject obj)
        {
            if (!unitSelected.Contains(obj))
                return;

            unitSelected.Remove(obj);
        }

        public List<GameObject> AllUnitsPlayer => allUnitsPlayer;

        public void InfoSelection()
        {
            Debug.Log("Now selected: " + unitSelected.Count);
        }
    }
}