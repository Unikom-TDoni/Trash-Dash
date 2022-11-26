using Group8.TrashDash.Event;
using Lnco.Unity.Module.Layout;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpLayoutGroupItem : LayoutGroupItem<PowerUpSO>
{
    [SerializeField]
    private Image _imgIcon = default;

    public override void UpdateContent(PowerUpSO content)
    {
        if (content == null)
        {
            _imgIcon.enabled = false;
            return;
        }
        _imgIcon.enabled = true;
        _imgIcon.sprite = content.Sprite;
    }
}
