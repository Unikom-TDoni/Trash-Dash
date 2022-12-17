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

        if (!tooltipController) tooltipController = Instantiate(Resources.Load<GameObject>("SimpleTooltip")).GetComponentInChildren<STController>();
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

        var lenght = trashContentInfo.Name.Length;
        if (lenght > 7 && lenght <= 10)
            lenght--;
        else if (lenght > 10)
            lenght -= 2;

        stcPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 22f * lenght);

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
