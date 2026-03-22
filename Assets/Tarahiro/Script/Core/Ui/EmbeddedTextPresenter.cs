using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using MessagePipe;
using VContainer.Unity;
using Tarahiro.MasterData;

namespace Tarahiro.Ui
{
    public class EmbeddedTextPresenter : IEmbeddedTextPresenter
    {
        [Inject] ILanguageMessageMasterDataProvider _provider;
        [Inject] EmbeddedTextViewFinder _viewManager;
        [Inject] ITranslationTextViewPureFactory _factory;

        CompositeDisposable _disposable = new CompositeDisposable();



        public void PostInitialize()
        {
            _viewManager.Finded.Subscribe(OnFind).AddTo(_disposable);
            _viewManager.Initialize();
        }

        public void OnFind(IEmbeddedTextView findedView)
        {
            findedView.CoreCreate(_factory);

            var master = _provider.TryGetFromId(findedView.TextId);
            if(master != null)
            {
                findedView.SetTranslatableText(master.GetMaster().Message);
            }
        }
    }
}
