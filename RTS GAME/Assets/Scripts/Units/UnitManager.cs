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
                var wayToComponent = obj.gameObject.GetComponent<UnitComponent>();
                var wayToSettings = gameObject.GetComponent<UnitsControl>();
                
                if ((int) wayToComponent.unitySetting.GetTeam == (int) wayToSettings.GetGroupControl)
                    allUnitsPlayer.Add(obj.gameObject);
            }
        }

        public bool ConntainsInfo(GameObject obj)
        {
            return unitSelected.Contains(obj);
        }
        
        public void AddUnits(GameObject obj)
        {
            if (unitSelected.Contains(obj))
                return;
            unitSelected.Add(obj);
            //Selected cirlce 
            obj.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        public void DelUnits(GameObject obj)
        {
            if (!unitSelected.Contains(obj))
                return;
            unitSelected.Remove(obj);
            obj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        public List<GameObject> AllUnitsPlayer => allUnitsPlayer;

        public void InfoSelection()
        {
            Debug.Log("Now selected: " + unitSelected.Count);
        }
    }
}