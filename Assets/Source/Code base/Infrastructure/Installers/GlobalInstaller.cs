﻿using UnityEngine;
using Zenject;

namespace Assets.Source.Code_base
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private PrefabsConfig _config;

        public override void InstallBindings()
        {
            BindPauseController();
            BindInput();
            BindPrefabs();
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