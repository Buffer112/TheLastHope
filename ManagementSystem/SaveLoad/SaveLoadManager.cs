using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using TheLastHope.Helpers;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Weapons.Software;
using TheLastHope.Hangar;

namespace TheLastHope.Management
{
    public static class SaveLoadManager
    {
        public static bool Load(out GameObject playerTrain, out Player player)
        {
            string dir = Application.persistentDataPath + "/saves";

			if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

			DirectoryInfo dir_i = new DirectoryInfo(dir);
            FileInfo[] info = dir_i.GetFiles("*.save");
            foreach (FileInfo f in info)
            {
                Debug.Log(dir);
            }

            if (info.Length >= 1)
            {

                BinaryFormatter bf = new BinaryFormatter();
                var createdir = Directory.CreateDirectory(Application.persistentDataPath + "/saves");
                FileStream stream = new FileStream(dir + "/player" + (info.Length) + ".save", FileMode.Open);
                stream.Position = 0;
                SerializedPlayerAndTrain sp = bf.Deserialize(stream) as SerializedPlayerAndTrain;
                stream.Close();
                playerTrain = sp.CreatePlayerTrain();
                player = sp.player;
                Debug.Log($"Player credit = {player.Credit}");
                return playerTrain != null;
            }
            else
            {
                Debug.Log("LOAD FROM RESOURCES. SAVE IS NOT FOUND.");
                string path = @"PlayerTrain";
                playerTrain =
                    GameObject.Instantiate(Resources.Load(path, typeof(GameObject)), new Vector3(-4.945748f, 0.3745093f, -0.979275f),
                                            Quaternion.LookRotation(Vector3.forward, Vector3.up)) as GameObject;
                player = new Player(2000);
                return playerTrain != null;
            }
        }

        public static void SavePlayer(GameObject playerTrain, Player player)
        {
            SerializedPlayerAndTrain sp = new SerializedPlayerAndTrain(playerTrain, player);
            BinaryFormatter bf = new BinaryFormatter();
            var createdir = Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            string dir = Application.persistentDataPath + "/saves";

            DirectoryInfo dir_i = new DirectoryInfo(dir);
            FileInfo[] info = dir_i.GetFiles("*.save");
            foreach (FileInfo f in info)
            {
                Debug.Log(f);
            }

            FileStream stream = new FileStream(dir + "/player" + (info.Length + 1) + ".save", FileMode.Create);
            stream.Position = 0;
            bf.Serialize(stream, sp);
            stream.Close();
        }

        

    }

    [Serializable]
    public class SerializedPlayerAndTrain
    {      
        [Serializable]
        public struct LoadableObject
        {
            public string pathInResource;
            public SerializableVector3 position;
            public SerializableQuaternion rotation;
        }

        [SerializeField]
        public List<LoadableObject> objects = new List<LoadableObject>(); 
        [SerializeField]
        public Player player;

        public SerializedPlayerAndTrain()
        {

        }

        
        public SerializedPlayerAndTrain(GameObject playerTrain, Player player)
        {
            this.player = player;
            LoadableObject parentObj = new LoadableObject();
            parentObj.pathInResource = playerTrain.GetComponent<ResourceLoadableObj>().Path;
            parentObj.position = playerTrain.transform.position;
            parentObj.rotation = playerTrain.transform.rotation;
            objects.Add(parentObj);
            List<GameObject> initList = new List<GameObject>();
            List<ResourceLoadableObj> trainScriptsToSerialize = GetAllObjectsOfTypeInChildren<ResourceLoadableObj>(playerTrain);

            foreach(var script in trainScriptsToSerialize)
            {
                LoadableObject objectToSerialize = new LoadableObject();
                objectToSerialize.pathInResource = script.Path;
                objectToSerialize.position = script.gameObject.transform.position;
                objectToSerialize.rotation = script.gameObject.transform.rotation;
                objects.Add(objectToSerialize);
                Debug.Log($"{objectToSerialize.pathInResource}");
            }
        }

        public GameObject CreatePlayerTrain()
        {
            GameObject parent = GameObject.Instantiate(Resources.Load(objects[0].pathInResource), objects[0].position,objects[0].rotation) as GameObject;

            List<GameObject> instantiatedObjects = new List<GameObject>();
            instantiatedObjects.Add(parent);
            for(var i=1; i< objects.Count;i++)
            {
                GameObject currentObject = GameObject.Instantiate(Resources.Load(objects[i].pathInResource), objects[i].position, objects[i].rotation) as GameObject;
                instantiatedObjects.Add(currentObject);
            }
            SetTrainStructure(instantiatedObjects);
            return parent;          
        }

        void SetTrainStructure(List<GameObject> instantiatedObjects)
        {
            GameObject parent = instantiatedObjects[0];
            List<GameObject> locos = Find<Loco>(instantiatedObjects);
            List<GameObject> cars = Find<ACarriage>(instantiatedObjects);
            List<GameObject> turrets = Find<ATurret>(instantiatedObjects);
            List<GameObject> soft = Find<ASoftware>(instantiatedObjects);
            List<GameObject> weapons = Find<AWeapon>(instantiatedObjects);
            List<GameObject> ammoContainers = Find<AmmoContainer>(instantiatedObjects);

            Debug.Log($"turrets length : {turrets.Count}");
            Debug.Log($"turrets soft : {soft.Count}");
            Debug.Log($"turrets weapons : {weapons.Count}");
            //Debug.Log($"turrets length : {turrets.Count}");
            for(var i=0;i<turrets.Count;i++)
            {
                turrets[i].GetComponentInChildren<ATurret>().soft = soft[i].GetComponent<ASoftware>();
                turrets[i].GetComponentInChildren<ATurret>().Weapon = weapons[i].GetComponent<AWeapon>();
                soft[i].transform.SetParent(turrets[i].GetComponentInChildren<ATurret>().transform);
                weapons[i].transform.SetParent(turrets[i].GetComponentInChildren<ATurret>().transform.Find("WeaponSlot"));
                foreach(var transform in GameObject.FindObjectsOfType<Transform>())
                {
                    if(turrets[i].transform.position.Equals(transform.position))
                    {
                        turrets[i].transform.SetParent(transform);
                    }
                }
            }
            for(var i=0;i< cars.Count;i++)
            {
                cars[i].gameObject.transform.SetParent(parent.transform);
                if (i>0)
                    cars[i].GetComponent<Carriage>().Connector = cars[i].transform.Find("Connector").gameObject;
            }
        }

        List<GameObject> Find<T>(List<GameObject> list)
        {
            List<GameObject> listToReturn = new List<GameObject>();
            foreach(var obj in list)
            {
                if (obj.GetComponentInChildren<T>() != null) //GetComponent
                    listToReturn.Add(obj);
            }
            return listToReturn;
        }

        /// <summary>
        /// Returns all objects of type T in children objects EXCLUDE objects in listitself.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        List<T> GetAllObjectsOfTypeInChildren<T>(GameObject obj)
        {
            List<T> objectsOfTypeToReturn = new List<T>();
            Debug.Log($"processing obj: {obj.name}");
            for (var i=0;i< obj.transform.childCount;i++)
            {
                if(obj.transform.GetChild(i).GetComponent<T>() != null)
                {
                    objectsOfTypeToReturn.Add(obj.transform.GetChild(i).gameObject.GetComponent<T>());
                    Debug.Log($"Added: {obj.transform.GetChild(i).gameObject.name}");
                }
                objectsOfTypeToReturn.AddRange(GetAllObjectsOfTypeInChildren<T>(obj.transform.GetChild(i).gameObject));
            }            
            return objectsOfTypeToReturn;
        }

    }
}

