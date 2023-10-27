using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

public class FlashEffect
{
    private Sequence _flashSequence;
    private Settings _settings;

    // have to do method injection because of circular dependency
    [Inject]
    public void Inject(SpriteRenderer sr, Settings settings)
    {
        _settings = settings;
        _flashSequence = DOTween.Sequence();

        var flashMat = sr.material;

        _flashSequence.Append(flashMat.DOFloat(1, "_FlashAmount", _settings.InitialFlashDuration));
        _flashSequence.AppendInterval(_settings.FullFlashDuration);
        _flashSequence.Append(flashMat.DOFloat(0, "_FlashAmount", _settings.FadeOutDuration));

        _flashSequence.SetAutoKill(false);
        _flashSequence.SetLink(sr.gameObject);

        _flashSequence.Pause();
    }

    public void DoFlash()
    {
        _flashSequence.Restart();
    }

    [Serializable]
    public class Settings
    {
        public float InitialFlashDuration;
        public float FullFlashDuration;
        public float FadeOutDuration;
    }
}