using Cysharp.Threading.Tasks;

namespace Assets._Source.CodeBase.Meta.Infrastructure.EntryPoint
{
    internal interface ISDKInitializer
    {
        UniTask Init();
    }
}