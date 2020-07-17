using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Management
{
    [ExecuteInEditMode]
    public class ResourceLoadableObj : MonoBehaviour
    {
        [Tooltip("Link to prefab in resources folder.")]
        [SerializeField] GameObject thisObj;
        [Tooltip("path to directory in the resources folder")]
        [SerializeField] string path;
        public GameObject PrefabObj { get { return thisObj; } set { thisObj = value; } }
        public string Path { get { return path + thisObj.name; } }

    }
}

