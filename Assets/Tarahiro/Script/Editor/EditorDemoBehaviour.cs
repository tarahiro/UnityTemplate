using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{
    [CreateAssetMenu(menuName = "Tarahiro/EditorDemoBehaviour")]
    public class EditorDemoBehaviour : DemoBehaviour
    {
        [SerializeField] DemoBehaviour _demoBehaviour;
        public override bool IsDemo => _demoBehaviour.IsDemo;
    }
}
