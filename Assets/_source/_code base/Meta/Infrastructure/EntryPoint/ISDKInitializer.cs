using Cysharp.Threading.Tasks;

namespace Assets._source._code_base.Meta
{
    internal interface ISDKInitializer
    {
        UniTask Init();
    }
}