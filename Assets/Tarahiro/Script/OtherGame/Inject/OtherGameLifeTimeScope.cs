using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.TInput;
using Tarahiro.Ui;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Tarahiro.OtherGame.MasterData;

namespace Tarahiro.OtherGame.Inject
{
    public class OtherGameLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Log.Comment("OtherGameLifetimeScopeでRegister開始");
            //OtherGame
            builder.RegisterFactory<Sprite, IOtherGameIcon>(container =>
            {

                return sprite =>
                {
                    var prefab = ResourceUtil.GetResource<OtherGameIcon>("Prefab/OtherGameIcon");
                    OtherGameIcon instance = container.Instantiate(prefab);
                    instance.Construct(sprite);
                    return instance;
                };
            }, Lifetime.Scoped);
            //通常はゲームコードを入れるが、このゲームは特例
            builder.Register<OtherGameModel>(Lifetime.Singleton).WithParameter("Temp").AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<OtherGameAbstructView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<OtherGameMenuView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<OtherGameDetailView>().AsImplementedInterfaces();
            builder.Register<OtherGameMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterFactory<IOtherGameMenuItemViewArgs, IOtherGameMenuItemView>(container =>
            {
                return args =>
                {
                    var prefab = ResourceUtil.GetResource<OtherGameMenuItemView>("Prefab/OtherGameMenuItemView");
                    OtherGameMenuItemView instance = container.Instantiate(prefab);
                    instance.Construct(args);
                    return instance;
                };
            }, Lifetime.Scoped);

            builder.RegisterFactory<IOtherGameMaster, IOtherGameMenuItemViewArgs>(m => new OtherGameMenuItemViewArgs(m.Id, m.IconPathJp));

            builder.RegisterFactory<IOtherGameMaster, IOtherGameDetailViewArgs>(x => new OtherGameDetailViewArgs(x.Id, x.TitleNameJp, x.GenreNameJp, x.DescriptionJp, x.ScreenShotCenterPathJp, x.ScreenShotRightTopPathJp, x.ScreenShotRightBottomPathJp));


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                //OtherGame
                entryPoints.Add<OtherGamePresenter>();

            });


        }
    }
}