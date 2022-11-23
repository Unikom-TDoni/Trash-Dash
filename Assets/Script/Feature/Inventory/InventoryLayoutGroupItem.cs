using System;
using UnityEngine;
using UnityEngine.UI;
using Group8.TrashDash.Event;
using Lnco.Unity.Module.Layout;
using UnityEngine.EventSystems;
using Lnco.Unity.Module.EventSystems;
using Group8.TrashDash.TrashBin;

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
            trashBinLayoutController.dragTrash = true;
            if (eventData.button is not PointerEventData.InputButton.Left) return;
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

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_imgIcon.raycastTarget) return;
            _imgIcon.raycastTarget = true;
        }

        public override void UpdateContent(TrashContentInfo content)
        {
            _imgIcon.sprite = content.Sprite;
            Data = new DropableData(content, this);
        }

        private void SnapToPosition()
        {
            if (_imgIcon.transform.parent == transform) return;
            _imgIcon.transform.SetParent(transform);
            _imgIcon.rectTransform.anchoredPosition = default;
            _originalImageIconPosition = default;
        }

        private void MoveToTheOriginalPosition() =>
            _imgIcon.rectTransform.anchoredPosition = Vector2.MoveTowards(_imgIcon.rectTransform.anchoredPosition, _originalImageIconPosition, _speed * Time.deltaTime);
    }
}