using Zenject;
using UnityEngine;

public class SceneObjectsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerHealthUIPresenter>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<CountDownUIPresenter>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<FingerPlayer>()
            .FromComponentInHierarchy()
            .AsSingle();


        Container.Bind<GameStateManager>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<StageInitializer>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}