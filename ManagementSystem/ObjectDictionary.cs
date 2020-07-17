using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Management
{
    /// <summary>
    /// Class that creates and contains object dictionary. Being used to makes Generator Manager's life easier.
    /// </summary>
    public class ObjectDictionary : MonoBehaviour
    {
        [SerializeField] List<GameObject> enemiesList = new List<GameObject>();
        [SerializeField] List<GameObject> groundList = new List<GameObject>();
        [SerializeField] List<GameObject> staticObjsList = new List<GameObject>();
        [SerializeField] List<GameObject> triggerObjList = new List<GameObject>();
        [SerializeField] List<GameObject> railsObjList = new List<GameObject>();
        [SerializeField] List<GameObject> carsObjList = new List<GameObject>();
        [SerializeField] List<GameObject> locoObjList = new List<GameObject>();
        public Dictionary<string, GameObject> ObjectsDictionary { get; set; }

        // Change this to Init?
        void Start()
        {
            ObjectsDictionary = new Dictionary<string, GameObject>();
            SetupDictionary("E.", enemiesList, ObjectsDictionary);
            SetupDictionary("G.", groundList, ObjectsDictionary);
            SetupDictionary("S.", staticObjsList, ObjectsDictionary);
            SetupDictionary("T.", triggerObjList, ObjectsDictionary);
            SetupDictionary("R.", railsObjList, ObjectsDictionary);
            SetupDictionary("Cars.", carsObjList, ObjectsDictionary);
            SetupDictionary("Loco.", locoObjList, ObjectsDictionary);
        }

        void SetupDictionary(string prefix, List<GameObject> list, Dictionary<string,GameObject> dict)
        {
            var i = 0;
            dict.Add(prefix + i, null);
            foreach (var obj in list)
            {
                i++;
                dict.Add(prefix + i, obj);
            }
        }
    }
}

