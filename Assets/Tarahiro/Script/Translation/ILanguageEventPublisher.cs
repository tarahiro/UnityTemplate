using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using MessagePipe;

namespace Tarahiro
{
    public interface ILanguageEventPublisher
    {
        void PublishEvent(int languageIndex);
    }
}
