using Cysharp.Threading.Tasks;

namespace Assets._Source.CodeBase.Meta.Services.Ads
{
    internal interface IAdsViewer
    {
        UniTask ShowInterstitial();
        UniTask<bool> ShowReward();
    }
}