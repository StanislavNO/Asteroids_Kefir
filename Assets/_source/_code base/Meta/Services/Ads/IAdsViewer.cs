using System.Threading.Tasks;

namespace Assets._source._code_base.Meta.Services.Ads
{
    internal interface IAdsViewer
    {
        void ShowInterstitial();
        Task ShowReward();
    }
}