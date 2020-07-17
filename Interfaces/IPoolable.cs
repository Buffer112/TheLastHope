using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Interfaces
{
    public interface IPoolable
    {
        void OnPopulate();
        void OnDepopulate();
    }
}

