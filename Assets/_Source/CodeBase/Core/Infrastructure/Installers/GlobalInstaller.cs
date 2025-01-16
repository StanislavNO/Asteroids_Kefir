using Assets._Source.CodeBase.Core.Common.Configs;
using Assets._Source.CodeBase.Core.Infrastructure.Services.AnimatorService;
using Assets._Source.CodeBase.Core.Infrastructure.Services.Input;
using Assets._Source.CodeBase.Core.Infrastructure.Services.SceneSwitcher;
using Assets._Source.CodeBase.Core.Infrastructure.Services.TimeManager;
using UnityEngine;
using Zenject;

namespace Assets._Source.CodeBase.Core.Infrastructure.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private PrefabsConfig _config;
        [SerializeField] private CharacterConfig _characterConfig;

        public override void InstallBindings()
        {
            BindPauseController();
            BindInput();
            BindPrefabs();
            BindSceneSwitcher();
            BindCharacterConfig();
            BindServices();
        }

        private void BindServices()
        {
            Container
                .Bind<IAnimatorService>()
                .To<DOAnimator>()
                .AsSingle();
        }

        private void BindCharacterConfig()
        {
            Container.Bind<CharacterConfig>()
                .FromInstance(_characterConfig)
                .AsSingle();
        }

        private void BindSceneSwitcher()
        {
            Container.BindInterfacesAndSelfTo<SceneSwitcher>().AsSingle();
        }

        private void BindPrefabs()
        {
            Container.Bind<PrefabsConfig>().FromInstance(_config).AsSingle();
        }

        private void BindInput()
        {
            Container.BindInterfacesTo<StandaloneInput>().AsSingle();
        }

        private void BindPauseController()
        {
            Container.BindInterfacesAndSelfTo<PauseController>().AsSingle();
        }
    }
}