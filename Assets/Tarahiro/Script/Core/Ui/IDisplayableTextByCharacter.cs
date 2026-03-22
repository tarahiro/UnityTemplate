using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Tarahiro.Ui
{
    public interface IDisplayableTextByCharacter
    {
        UniTask Enter(CancellationToken ct);
        void Skip();
        
    }
}