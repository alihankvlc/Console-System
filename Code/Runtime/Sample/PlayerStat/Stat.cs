using System;
using System.Collections.Generic;
using System.Linq;
using Debugging.Runtime.Core;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Debugging.Runtime.Sample
{
    [System.Serializable]
    public class PlayerStat
    {
        public string Name;
        public int BaseValue;
        [ReadOnly] public int CurrentValue;

        public void Initialize() => CurrentValue = BaseValue;
        public void Modify(int value) => CurrentValue += value;
    }

    public class Stat : CommandMonoBehaviour
    {
        [SerializeField] private PlayerStat[] _stats;

        private void Start()
        {
            _stats.ForEach(r => r.Initialize());
        }

        [Command("/changestat")]
        private void ApplyModifier(string statName, int modifier)
        {
            PlayerStat stat = GetStat(statName);
            stat?.Modify(modifier);
        }

        private PlayerStat GetStat(string statName)
        {
            return _stats.FirstOrDefault(r => r.Name == statName);
        }
    }
}