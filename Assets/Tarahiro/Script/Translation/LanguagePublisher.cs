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
    public class LanguagePublisher : ILanguageEventPublisher
    {
        [Inject] private IPublisher<int> _publisher;

        public void PublishEvent(int languageIndex)
        {
            Log.Comment("言語コマンド発行");
            _publisher.Publish(languageIndex);
        }
    }
}
