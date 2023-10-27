using UnityEngine;
using DG.Tweening;
using Zenject;

public class PickableWeapon : MonoBehaviour
{
    public IWeapon Weapon { get; private set; }

    private SpriteRenderer sr;

    [Inject]
    public void Inject(IWeapon weapon)
    {
        Weapon = weapon;
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = Weapon.Sprite;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOBlendableLocalMoveBy(new Vector3(0, 0.1f, 0), 1f).SetEase(Ease.InSine));
        seq.Append(transform.DOBlendableLocalMoveBy(new Vector3(0, 0.1f, 0), 1f).SetEase(Ease.OutSine));
        seq.SetLoops(-1, LoopType.Yoyo);
        seq.SetLink(gameObject);
    }

    public void DoHighlight() => sr.material.SetFloat("_Alpha", 1);

    public void StopHighlight() => sr.material.SetFloat("_Alpha", 0);

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public class Factory : PlaceholderFactory<IWeapon, PickableWeapon> { }
}