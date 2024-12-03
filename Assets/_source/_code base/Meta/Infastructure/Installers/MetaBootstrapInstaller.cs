using Zenject;

namespace Assets._source._code_base.Meta
{
    internal class MetaBootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFirebaseInitializer();
        }

        private void BindFirebaseInitializer()
        {
            Container.Bind<FirebaseInitializer>().AsSingle();
        }
    }
}