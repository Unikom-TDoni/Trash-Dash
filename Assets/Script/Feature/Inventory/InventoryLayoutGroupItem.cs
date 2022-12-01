using System;
using UnityEngine;
using UnityEngine.UI;
using Group8.TrashDash.Event;
using Lnco.Unity.Module.Layout;
using UnityEngine.EventSystems;
using Group8.TrashDash.TrashBin;
using Lnco.Unity.Module.EventSystems;

namespace Group8.TrashDash.Inventory
{
    public sealed class InventoryLayoutGroupItem : LayoutGroupItem<TrashContentInfo>, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropable<DropableData>
    {
        [SerializeField]
        [Range(100, 1000)]
        private float _speed = default;

        [SerializeField]
        private Image _imgIcon = default;

        private Transform _topParent = default;

        private Vector2 _originalImageIconPosition = default;

        private TrashBinLayoutController trashBinLayoutController;

        private bool IsInTheOriginalPositionRange => Vector2.Distance(_imgIcon.rectTransform.anchoredPosition, _originalImageIconPosition) < 1;

        public DropableData Data { get; private set; } = default;

        private void Awake()
        {
            _topParent = transform.parent.parent;

            GameObject trashBin = GameObject.Find("IMG_Trash-Bin");
            trashBinLayoutController = trashBin.GetComponent<TrashBinLayoutController>();
        }

        private void Update()
        {
            if (!_imgIcon.raycastTarget) return;
            if (!IsInTheOriginalPositionRange) MoveToTheOriginalPosition();
            else SnapToPosition();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button is not PointerEventData.InputButton.Left) return;
            if (eventData.pointerPressRaycast.gameObject != _imgIcon.gameObject) return;
            trashBinLayoutController.dragTrash = true;
            _imgIcon.transform.SetParent(_topParent);
            _imgIcon.raycastTarget = default;
            _originalImageIconPosition = _imgIcon.rectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_imgIcon.raycastTarget) return;
            _imgIcon.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_imgIcon.raycastTarget) return;
            _imgIcon.raycastTarget = true;
            trashBinLayoutController.AnimClose();
        }

        public override void UpdateContent(TrashContentInfo content)
        {
            _imgIcon.enabled = content is not null && content.Sprite is not null;
            _imgIcon.sprite = content is null ? default : content.Sprite;
            Data = new DropableData(content, this);
        }

        public void SnapToPosition()
        {
            if (_imgIcon.transform.parent == transform) return;
            _imgIcon.transform.SetParent(transform);
            _imgIcon.rectTransform.anchoredPosition = default;
            _originalImageIconPosition = default;
        }

        public bool IsImageEnabled() =>
            _imgIcon.enabled;

        private void MoveToTheOriginalPosition() =>
            _imgIcon.rectTransform.anchoredPosition = Vector2.MoveTowards(_imgIcon.rectTransform.anchoredPosition, _originalImageIconPosition, _speed * Time.deltaTime);
    }
}