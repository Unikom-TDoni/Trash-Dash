using UnityEngine;
using UnityEngine.UI;
using Lnco.Unity.Module.Layout;
using UnityEngine.EventSystems;
using Group8.TrashDash.TrashBin;

namespace Group8.TrashDash.Inventory
{
    public sealed class InventoryLayoutGroupItem : LayoutGroupItem<TrashContentInfo>, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private Image _imgIcon = default;

        [SerializeField]
        private Image _imgBackground = default;

        [SerializeField]
        [Range(100, 1000)]
        private float _lerpSpeed = default;

        private TrashBinTypes _trashBinTypes = default;

        private Transform _topParent = default;

        private Vector2 _originalImageIconPosition = default;

        private bool IsInTheOriginalPositionRange
        {
            get => Vector2.Distance(_imgIcon.rectTransform.anchoredPosition, _originalImageIconPosition) < 1;
        }

        private void Awake()
        {
            _topParent = transform.parent.parent;
        }

        private void Update()
        {
            if (!_imgIcon.raycastTarget) return;
            if (!IsInTheOriginalPositionRange)
                MoveToTheOriginalPosition();
            else
            {
                _imgIcon.transform.SetParent(transform);
                _imgIcon.rectTransform.anchoredPosition = default;
                _originalImageIconPosition = default;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.pointerPressRaycast.gameObject != _imgIcon.gameObject) return;
            _imgIcon.transform.SetParent(_topParent);
            _imgIcon.raycastTarget = default;
            _originalImageIconPosition = _imgIcon.rectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_imgIcon.raycastTarget) return;
            _imgIcon.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData) =>
            _imgIcon.raycastTarget = true;

        public override void UpdateContent(TrashContentInfo data)
        {
            if (data is null) return;
            _imgIcon.sprite = data.Sprite;
            _trashBinTypes = data.TrashBinType;
        }

        public void Reset()
        {
            _trashBinTypes = default;
            _imgIcon.sprite = default;
        }

        public TrashBinTypes GetTrashBinTypes() =>
            _trashBinTypes;

        private void MoveToTheOriginalPosition() =>
            _imgIcon.rectTransform.anchoredPosition = Vector2.MoveTowards(_imgIcon.rectTransform.anchoredPosition, _originalImageIconPosition, _lerpSpeed * Time.deltaTime);
    }
}