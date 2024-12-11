using Cysharp.Threading.Tasks;

namespace Assets._source._code_base.Meta.Services.Ads
{
    internal interface IAdsViewer
    {
        UniTask ShowInterstitial();
        UniTask<bool> ShowReward();
    }
}