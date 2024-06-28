using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class AxeUI : MonoBehaviour
    {
        [SerializeField] private float cycleLenght;
        private void Start()
        {
            transform.DORotate(new Vector3(0,-180,-23.539f), cycleLenght).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental).SetUpdate(UpdateType.Normal, true);;
        }
    }
}