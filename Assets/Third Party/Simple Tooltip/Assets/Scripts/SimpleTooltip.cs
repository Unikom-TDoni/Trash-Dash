using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Group8.TrashDash.Inventory;

[DisallowMultipleComponent]
public class SimpleTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler
{
    public SimpleTooltipStyle simpleTooltipStyle;
    private STController tooltipController;
    private EventSystem eventSystem;
    private bool cursorInside = false;
    private bool showing = false;
    private RectTransform stcPanel;

    private InventoryLayoutGroupItem _inventoryLayoutGroupItem = default;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        tooltipController = FindObjectOfType<STController>();

        if (!tooltipController) tooltipController = Instantiate(Resources.Load<GameObject>("SimpleTooltip"), new Vector3(0,-1000000, 0), Quaternion.identity).GetComponentInChildren<STController>();
        stcPanel = tooltipController.GetComponent<RectTransform>();

        if (!simpleTooltipStyle)
            simpleTooltipStyle = Resources.Load<SimpleTooltipStyle>("STDefault");

        _inventoryLayoutGroupItem = GetComponent<InventoryLayoutGroupItem>();
    }

    private void Update()
    {
        if (!cursorInside)
            return;

        tooltipController.ShowTooltip();
    }

    private void OnMouseOver()
    {
        if (eventSystem)
        {
            if (!eventSystem.IsPointerOverGameObject()) return;
            HideTooltip();
            return;
        }
        ShowTooltip();
    }

    private void OnMouseExit()
    {
        HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        HideTooltip();
    }

    public void ShowTooltip()
    {
        showing = true;
        cursorInside = true;

        var trashContentInfo = _inventoryLayoutGroupItem.Data.TrashContentInfo;

        var length = trashContentInfo.Name.Length;
        if (length > 7 && length < 10) length--;
        else if (length >= 10) length -= 2;

        // TEMPORARY
        if (length == 12)
            stcPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20 * length);
        else if (length <= 13 || length > 18)
            stcPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 22 * length);
        else if (length == 18)
            stcPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 23 * length);
        else if (length == 17 || length == 14)
            stcPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 17 * length);
        else if (length == 16 || length == 15)
            stcPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20 * length);
        else
            stcPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 15 * length);

        tooltipController.SetCustomStyledText(trashContentInfo.Name, simpleTooltipStyle, STController.TextAlign.Left);

        tooltipController.ShowTooltip();
    }

    public void HideTooltip()
    {
        if (!showing)
            return;
        showing = false;
        cursorInside = false;
        tooltipController.HideTooltip();
    }

    private void Reset()
    {
        // Load the default style if none is specified
        if (!simpleTooltipStyle)
            simpleTooltipStyle = Resources.Load<SimpleTooltipStyle>("STDefault");

        // If UI, nothing else needs to be done
        if (GetComponent<RectTransform>())
            return;

        // If has a collider, nothing else needs to be done
        if (GetComponent<Collider>())
            return;

        // There were no colliders found when the component is added so we'll add a box collider by default
        // If you are making a 2D game you can change this to a BoxCollider2D for convenience
        // You can obviously still swap it manually in the editor but this should speed up development
        gameObject.AddComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        HideTooltip();
    }

    private void OnDisable()
    {
        HideTooltip();
    }
}
