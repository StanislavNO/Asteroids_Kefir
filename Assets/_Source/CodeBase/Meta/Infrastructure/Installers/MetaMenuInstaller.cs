using _Source.CodeBase.Meta.Controllers;
using Assets._Source.CodeBase.Meta.Services.InApp;
using Assets._Source.CodeBase.Meta.View;
using UnityEngine;
using Zenject;

namespace Assets._Source.CodeBase.Meta.Infrastructure.Installers
{
    internal class MetaMenuInstaller : MonoInstaller
    {
        [SerializeField] private LeaderboardWindow _leaderboardWindow;
        
        public override void InstallBindings()
        {
            BindInAppController();
            BindLeaderboard();
        }

        private void BindLeaderboard()
        {
            Container.BindInstance(_leaderboardWindow);
            Container.BindInterfacesTo<LeaderboardController>().AsSingle().NonLazy();
        }

        private void BindInAppController()
        {
            Container
                .BindInterfacesTo<InAppController>()
                .AsSingle()
                .NonLazy();
        }
    }
}
