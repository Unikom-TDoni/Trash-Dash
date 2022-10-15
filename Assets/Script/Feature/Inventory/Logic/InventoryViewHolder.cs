using UnityEngine;
using UnityEngine.UI;
using Lnco.Unity.Module.Layout;

namespace Group8.TrashDash.Inventory
{
    public sealed class InventoryViewHolder : ViewHolder<TrashContentInfo>
    {
        [SerializeField]
        private Image _icon = default;

        private bool _isDragable = default;

        private void Update()
        {
            if (!_isDragable) return;
            MoveToTheOriginalPosition();
        }

        private void MoveToTheOriginalPosition()
        {
            var newPosition = Vector4.Lerp(transform.position, _icon.transform.position, 10 * Time.deltaTime);
            _icon.transform.position = newPosition;
            if (_icon.transform.position.Equals(transform.position))
                _isDragable = true;
        }

        /// <summary>
        /// For the input system
        /// </summary>
        /// <param name="positon"></param>
        public void OnDrag(Vector2 positon)
        {

        }

        public override void UpdateContent(TrashContentInfo data)
        {
            if (data is null) return;
            _icon.sprite = data.Sprite;
        }
    }
}