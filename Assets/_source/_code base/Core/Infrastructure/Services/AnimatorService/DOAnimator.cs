using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._source._code_base.Core.Infrastructure.Services.AnimatorService
{
    internal class DOAnimator
    {
        public void FadeOut(CanvasGroup view)
        {
            view.DOFade(0, 0.5f);
        }

        public void FadeIn(CanvasGroup view)
        {
            view.alpha = 0;
            view.DOFade(1, 2.5f);
        }

        public void ShowBounds(Transform obj)
        {
            obj.DOScale(1, 1.3f)
                .From(0).SetEase(Ease.OutBounce);
        }
    }
}