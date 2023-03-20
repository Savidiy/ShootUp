using AudioModule;
using Bootstrap;
using LevelWindowModule;
using MainModule;
using MvvmModule;
using Progress;
using Savidiy.Utils;
using SettingsModule;
using SettingsWindowModule;
using StartWindowModule;
using UiModule;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public GameSettings GameSettings;
        public EnemyPrefabProvider EnemyPrefabProvider;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Bootstrapper>().AsSingle();

            Container.BindInterfacesTo<PrefabFactory>().AsSingle();
            Container.BindInterfacesTo<ViewFactory>().AsSingle();
            Container.BindInterfacesTo<ViewModelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<TickInvoker>().AsSingle();

            Container.Bind<WindowsRootProvider>().AsSingle();
            Container.BindInterfacesTo<StartWindowPresenter>().AsSingle();
            Container.BindInterfacesTo<SettingsWindowPresenter>().AsSingle();
            Container.BindInterfacesTo<LevelWindowPresenter>().AsSingle();
            Container.BindInterfacesTo<SelectControlsWindowPresenter>().AsSingle();

            Container.Bind<MainStateMachine>().AsSingle();
            Container.Bind<StartMainState>().AsSingle();
            Container.Bind<LevelPlayMainState>().AsSingle();
            Container.Bind<SelectControlMainState>().AsSingle();

            Container.Bind<LevelHolder>().AsSingle();
            Container.Bind<ProgressProvider>().AsSingle();
            Container.Bind<LevelModelFactory>().AsSingle();

            Container.Bind<PlayerShooter>().AsSingle();
            Container.Bind<PlayerHolder>().AsSingle();
            Container.Bind<PlayerMover>().AsSingle();
            Container.Bind<BulletHolder>().AsSingle();
            Container.Bind<BulletMover>().AsSingle();
            Container.Bind<BulletAtBorderKiller>().AsSingle();
            Container.Bind<BulletAtEnemyChecker>().AsSingle();
            
            Container.Bind<EnemyAttackExecutor>().AsSingle();
            Container.Bind<EnemyAtLivesKiller>().AsSingle();
            Container.Bind<EnemiesHolder>().AsSingle();
            Container.Bind<EnemyMover>().AsSingle();
            Container.Bind<EnemyFactory>().AsSingle();
            Container.BindInterfacesTo<EnemyRombMover>().AsSingle();
            Container.BindInterfacesTo<EnemyCircleMover>().AsSingle();
            Container.BindInterfacesTo<EnemySquareMover>().AsSingle();
            
            Container.Bind<BorderController>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<AudioSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<MusicVolumeController>().AsSingle();

            Container.Bind<GameSettings>().FromInstance(GameSettings);
            Container.Bind<EnemyPrefabProvider>().FromInstance(EnemyPrefabProvider);
        }
    }
}