using Assets._source._code_base.Core.Infrastructure.Services.AnimatorService;
using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base
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