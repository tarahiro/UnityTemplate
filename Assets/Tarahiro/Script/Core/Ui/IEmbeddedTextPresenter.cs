using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using Tarahiro.Ui;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro.Ui
{
    public interface IEmbeddedTextPresenter : IPostInitializable
    {
        void OnFind(EmbeddedTranslationTextView findedView);
    }
}
