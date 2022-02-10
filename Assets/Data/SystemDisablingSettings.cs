using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class SystemDisablingSettings : ScriptableObject
    {
        public List<string> deactivatedSystems;
        public List<string> systemsToDeactivate;
        public bool         foldout;
    }
}