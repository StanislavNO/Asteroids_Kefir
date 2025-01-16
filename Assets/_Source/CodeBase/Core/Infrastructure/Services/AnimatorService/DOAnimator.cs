using DG.Tweening;
using UnityEngine;

namespace Assets._Source.CodeBase.Core.Infrastructure.Services.AnimatorService
{
    public class DOAnimator : IAnimatorService
    {
        public void ShowBounds(Transform obj, float endValue)
        {
            obj.DOScale(endValue, 1.3f)
                .From(0).SetEase(Ease.OutBounce);
        }
    }
}