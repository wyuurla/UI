using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICreateGameObject
{
    public enum eSLOTLIST_TYPE
    {
        VERTICAL,
        HORIZONTAL,
        GRID_VERTICAL,
    }

    /**
     * UIManager를 생성한다.
     */
    public void CreateUIManager(float _base_resolution_width = 720f, float _base_resolution_height = 1280f)
    {
        DeleteUIManager();

        Transform _transform_uiManager = CreateTransform("UIManager", null);
        Transform _transform_uiCamera = CreateTransform("UICamera", _transform_uiManager);
        Transform _transform_eventSystem = CreateTransform("EventSystem", _transform_uiManager);
        RectTransform _transform_canvas = CreateRectTransform("Canvas", _transform_uiManager, UIUtil.eANCHOR.StretchAll);

        Transform _transform_panel = CreateRectTransform("Panel", _transform_canvas, UIUtil.eANCHOR.StretchAll);
        Transform _transform_dialog = CreateRectTransform("Dialog", _transform_panel, UIUtil.eANCHOR.StretchAll);
        Transform _transform_hud = CreateRectTransform("Hud", _transform_panel, UIUtil.eANCHOR.StretchAll);
        Transform _transform_tutorial = CreateRectTransform("Tutorial", _transform_panel, UIUtil.eANCHOR.StretchAll);
        Transform _transform_fade = CreateRectTransform("Fade", _transform_panel, UIUtil.eANCHOR.StretchAll);
        Transform _transform_popup = CreateRectTransform("Popup", _transform_panel, UIUtil.eANCHOR.StretchAll);

        _transform_uiCamera.localPosition = new Vector3(0f, 0f, -1000f);

        UIManager _uiManager = _transform_uiManager.gameObject.AddComponent<UIManager>();
        Canvas _canvas = _transform_canvas.gameObject.AddComponent<Canvas>();
        CanvasScaler _canvasScaler = _transform_canvas.gameObject.AddComponent<CanvasScaler>();
        GraphicRaycaster _graphicRaycaster = _transform_canvas.gameObject.AddComponent<GraphicRaycaster>();
        Camera _uiCamera = _transform_uiCamera.gameObject.AddComponent<Camera>();
        _transform_eventSystem.gameObject.AddComponent<UnityEngine.EventSystems.EventSystem>();
        _transform_eventSystem.gameObject.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        UIDialogGroup _dialog = _transform_dialog.gameObject.AddComponent<UIDialogGroup>();
        UIHudGroup _hud = _transform_hud.gameObject.AddComponent<UIHudGroup>();
        UIDialogGroup _tutorial = _transform_tutorial.gameObject.AddComponent<UIDialogGroup>();
        UIFadeGroup _fade = _transform_fade.gameObject.AddComponent<UIFadeGroup>();
        UIDialogGroup _popup = _transform_popup.gameObject.AddComponent<UIDialogGroup>();
        UIAutoScreen _autoScreen = _transform_uiManager.gameObject.AddComponent<UIAutoScreen>();
        _autoScreen.baseScreen_width = _base_resolution_width;
        _autoScreen.baseScreen_height = _base_resolution_height;
        _autoScreen.canvasScaler = _canvasScaler;



        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = _uiCamera;
        _canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        _canvasScaler.referenceResolution = new Vector2(_base_resolution_width, _base_resolution_height);
        _uiCamera.clearFlags = CameraClearFlags.Depth;
        _uiCamera.cullingMask = 1 << LayerMask.NameToLayer("UI");
        _uiCamera.orthographic = true;
        _uiCamera.orthographicSize = 5f;
        _uiCamera.farClipPlane = 3000f;
        _uiManager.uiCamera = _uiCamera;
        _uiManager.dialog = _dialog;
        _uiManager.hud = _hud;
        _uiManager.tutorial = _tutorial;
        _uiManager.fade = _fade;
        _uiManager.popup = _popup;
    }

    /**
     * 모든 UIManager 찾아 삭제한다.
     */
    protected void DeleteUIManager()
    {
        UIManager[] _finds = Object.FindObjectsOfType<UIManager>();
        for(int i=0; i<_finds.Length; ++i)
        {
           GameObject.DestroyImmediate(_finds[i].gameObject);
        }
    }
    
    public UIDialog CreateDialog( System.Type _type, string _name, Transform _attach, float _width, float _height)
    {
        if (null == _type)
        {
            UnityEditor.EditorUtility.DisplayDialog("error", $"UIDialog [System.Type == null] name : {_name}", "OK");            
            return null;
        }

        RectTransform _transform_dialog = CreateRectTransform(_name, _attach, UIUtil.eANCHOR.StretchAll);
        RectTransform _transform_back = CreateRectTransform("Back", _transform_dialog, UIUtil.eANCHOR.StretchAll);
        RectTransform _transform_panel = CreateRectTransform("Panel", _transform_dialog, UIUtil.eANCHOR.MiddleCenter);

        UIDialog _dialog = _transform_dialog.gameObject.AddComponent(_type) as UIDialog;
        _dialog.backImage = _transform_back.gameObject.AddComponent<Image>();
        _dialog.panel = _transform_panel;
        _dialog.panel.gameObject.AddComponent<Image>();

        _dialog.backImage.color = new Color(0f, 0f, 0f, 0.5f);
        _dialog.backImage.rectTransform.sizeDelta = new Vector2(10f, 10f);
        _dialog.panel.sizeDelta = new Vector2(_width, _height);

        return _dialog;
    }

    public UISlot CreateSlot(System.Type _type, string _name, Transform _attach, float _width, float _height)
    {
        if (null == _type)
        {
            UnityEditor.EditorUtility.DisplayDialog("error", $"UISlot [System.Type == null] name : {_name}", "OK");
            return null;
        }

        RectTransform _transform_Slot = CreateRectTransform(_name, _attach, UIUtil.eANCHOR.MiddleCenter);
        RectTransform _transform_panel = CreateRectTransform("panel", _transform_Slot, UIUtil.eANCHOR.StretchAll);

        _transform_Slot.sizeDelta = new Vector2(_width, _height);
        UISlot _slot = _transform_Slot.gameObject.AddComponent(_type) as UISlot;
        _transform_Slot.gameObject.AddComponent<Image>();
        UIButton _button = _transform_Slot.gameObject.AddComponent<UIButton>();
        _slot.btnClick = new Button[1];
        _slot.btnClick[0] = _button;
        _slot.panel = _transform_panel;
        return _slot;
    }


    public UIHud CreateHud(System.Type _type, string _name, Transform _attach, float _width, float _height)
    {
        if (null == _type)
        {
            UnityEditor.EditorUtility.DisplayDialog("error", $"UIHud [System.Type == null] name : {_name}", "OK");
            return null;
        }
        RectTransform _transform_Slot = CreateRectTransform(_name, _attach, UIUtil.eANCHOR.MiddleCenter);
        RectTransform _transform_panel = CreateRectTransform("panel", _transform_Slot, UIUtil.eANCHOR.StretchAll);
        _transform_Slot.sizeDelta = new Vector2(_width, _height);
        UIHud _hud = _transform_Slot.gameObject.AddComponent(_type) as UIHud;
        _hud.panel = _transform_panel;

        return _hud;
    }

    public UIButton CreateButton(UIDialog _dialog)
    {
        RectTransform _transform_button = CreateRectTransform("Button", _dialog.panel, UIUtil.eANCHOR.MiddleCenter);
        _transform_button.gameObject.AddComponent<Image>();
        UIButton _button = _transform_button.gameObject.AddComponent<UIButton>();
        return _button;
    }

    public UIButton CreateTextButton(UIDialog _dialog)
    {
        RectTransform _transform_button = CreateRectTransform("Button", _dialog.panel, UIUtil.eANCHOR.MiddleCenter);
        RectTransform _transform_text = CreateRectTransform("Text", _transform_button, UIUtil.eANCHOR.StretchAll);
        _transform_button.gameObject.AddComponent<Image>();
        UIButton _button = _transform_button.gameObject.AddComponent<UIButton>();
        Text _text = _transform_text.gameObject.AddComponent<Text>();
        _text.color = Color.black;
        _text.resizeTextForBestFit = true;
        _text.alignment = TextAnchor.MiddleCenter;
        return _button;
    }

    public UIButton CreateCloseButton(UIDialog _dialog)
    {
        RectTransform _transform_button = CreateRectTransform("Button", _dialog.panel, UIUtil.eANCHOR.TopRight);
        _transform_button.anchoredPosition = Vector2.zero;
        _transform_button.gameObject.AddComponent<Image>();
        UIButton _button = _transform_button.gameObject.AddComponent<UIButton>();

        Button[] _temp = _dialog.btnCloses;
        _dialog.btnCloses = new Button[_temp.Length + 1];
        for( int i=0; i<_temp.Length; ++i )
        {
            _dialog.btnCloses[i] = _temp[i];
        }
        _dialog.btnCloses[_dialog.btnCloses.Length - 1] = _button;
        return _button;
    }


    public UISlotList CreateSlotList(UIDialog _dialog, eSLOTLIST_TYPE _slotListType, float _size)
    {
        RectTransform _transform_slotList = CreateRectTransform("SlotList", _dialog.panel, UIUtil.eANCHOR.MiddleCenter);
        RectTransform _transform_viewport = CreateRectTransform("Viewport", _transform_slotList, UIUtil.eANCHOR.StretchAll);
        RectTransform _transform_content = CreateRectTransform("Content", _transform_viewport, UIUtil.eANCHOR.StretchAll);
        _transform_content.pivot = new Vector2(0f, 1f);

        ScrollRect _scrollRect = _transform_slotList.gameObject.AddComponent<ScrollRect>();
        _scrollRect.content = _transform_content;
        _scrollRect.viewport = _transform_viewport;
      
        UISlotList _slotList = _transform_slotList.gameObject.AddComponent<UISlotList>();
        _slotList.scrollRect = _scrollRect;
        _slotList.attach = _transform_content;

        _transform_viewport.gameObject.AddComponent<Image>();
        Mask _mask = _transform_viewport.gameObject.AddComponent<Mask>();
        _mask.showMaskGraphic = false;

        ContentSizeFitter _sizeFitter = _transform_content.gameObject.AddComponent<ContentSizeFitter>();


        switch (_slotListType)
        {
            case eSLOTLIST_TYPE.HORIZONTAL:
                _scrollRect.horizontal = true;
                _scrollRect.vertical = false;          
                HorizontalLayoutGroup _horizontalLayoutGourp = _transform_content.gameObject.AddComponent<HorizontalLayoutGroup>();
                _horizontalLayoutGourp.spacing = 10f;
                _horizontalLayoutGourp.padding = new RectOffset(10, 10, 10, 10);
                _horizontalLayoutGourp.childForceExpandWidth = false;
                _horizontalLayoutGourp.childControlHeight = true;
                _horizontalLayoutGourp.childControlWidth = false;
                _sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                break;

            case eSLOTLIST_TYPE.VERTICAL:
                _scrollRect.horizontal = false;
                _scrollRect.vertical = true;
                VerticalLayoutGroup _verticalLayoutGourp = _transform_content.gameObject.AddComponent<VerticalLayoutGroup>();
                _verticalLayoutGourp.spacing = 10f;
                _verticalLayoutGourp.padding = new RectOffset(10, 10, 10, 10);
                _verticalLayoutGourp.childForceExpandHeight = false;
                _verticalLayoutGourp.childControlHeight = false;
                _verticalLayoutGourp.childControlWidth = true;
                _sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                break;

            case eSLOTLIST_TYPE.GRID_VERTICAL:
                _scrollRect.horizontal = false;
                _scrollRect.vertical = true;

                GridLayoutGroup _gridLayouGroup = _transform_content.gameObject.AddComponent<GridLayoutGroup>();
                _gridLayouGroup.spacing = new Vector2(10, 10);
                _gridLayouGroup.padding = new RectOffset(10, 10, 10, 10);
                _gridLayouGroup.cellSize = new Vector2(_size, _size);
                _sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                break;
        }

        return _slotList;
    }

    /**
     * RectTransform 을 생성한다.
     */
    protected RectTransform CreateRectTransform( string _name, Transform _attach, UIUtil.eANCHOR _anchor)
    {
        GameObject _object = new GameObject(_name);
        _object.layer = LayerMask.NameToLayer("UI");
        RectTransform _rectTransform = _object.AddComponent<RectTransform>();
        ResUtil.SetAttach(_attach, _rectTransform, null);
        UIUtil.SetAnchor(_rectTransform, _anchor);
        return _rectTransform;
    }

    /**
     * Transform을 생성한다.
     */
    protected Transform CreateTransform(string _name, Transform _attach )
    {
        GameObject _object = new GameObject(_name);
        _object.layer = LayerMask.NameToLayer("UI");
        ResUtil.SetAttach(_attach, _object.transform, null);
        return _object.transform;
    }
}
