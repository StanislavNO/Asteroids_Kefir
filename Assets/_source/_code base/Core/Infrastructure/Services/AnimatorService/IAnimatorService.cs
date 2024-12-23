using UnityEngine;

namespace Assets._source._code_base.Core.Infrastructure.Services.AnimatorService
{
    public interface IAnimatorService
    {
        void FadeOut(CanvasGroup view);
        void FadeIn(CanvasGroup view);
        void ShowBounds(Transform obj);
    }
}