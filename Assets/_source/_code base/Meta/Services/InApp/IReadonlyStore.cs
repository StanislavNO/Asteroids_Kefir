using Assets._source._code_base.Meta.Services.InApp;

namespace Assets._source._code_base.Meta.Services
{
    public interface IReadonlyStore
    {
        AdsStatus AdsStatus { get; }
    }
}