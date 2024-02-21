using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static partial class UIUtil
{
    public static bool IsPointOverInUI()
    {
        int pointerId = -1;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        pointerId = -1;
#else
        pointerId = 0;
#endif

        UnityEngine.EventSystems.EventSystem _eventSystem = UnityEngine.EventSystems.EventSystem.current;
        if (null == _eventSystem)
            return false;

        return _eventSystem.IsPointerOverGameObject(pointerId);
    }

    #region - AddClickEvent
    public static void AddClickEvent(Button _btn, UnityEngine.Events.UnityAction _click)
    {
        if (null == _btn)
            return;

        _btn.onClick.AddListener(_click);
    }

    public static void AddClickEvent(Button[] _btns, UnityEngine.Events.UnityAction _click)
    {
        if (null == _btns)
            return;

        for( int i=0;i<_btns.Length; ++i )
        {
            AddClickEvent(_btns[i], _click);
        }
    }

    public static void AddClickEvent(Toggle _btn, UnityEngine.Events.UnityAction<bool> _click)
    {
        if (null == _btn)
            return;

        _btn.onValueChanged.AddListener(_click);
    }

    #endregion


    public static bool IsOpen(MonoBase _mono)
    {
        if (null == _mono)
            return false;

        return _mono.isOpen;
    }

    #region - SetText
    public static void SetText(Text _text, string _str)
    {
        if (null == _text)
            return;

        if( string.IsNullOrWhiteSpace(_str) == true )
        {
            _text.gameObject.SetActive(false);
            return;
        }

        _text.gameObject.SetActive(true);
        _text.text = _str;
    }

    public static void SetText(Text[] _texts, string _str)
    {
        if (null == _texts)
            return;
        for( int i=0; i<_texts.Length; ++i )
        {
            SetText(_texts[i], _str);
        }
    }

    public static void SetText(InputField _text, string _str)
    {
        if (null == _text)
            return;

        if (string.IsNullOrWhiteSpace(_str) == true)
        {
            _text.gameObject.SetActive(false);
            return;
        }

        _text.gameObject.SetActive(true);
        _text.text = _str;
    }

    public static void SetText(InputField[] _texts, string _str)
    {
        if (null == _texts)
            return;
        for (int i = 0; i < _texts.Length; ++i)
        {
            SetText(_texts[i], _str);
        }
    }

    #endregion

    #region - SetValue
    public static void SetValue(Slider _slider, float _value)
    {
        if (null == _slider)
            return;

        _slider.value = _value;
    }

    public static void SetValue(Toggle _slider, bool _value)
    {
        if (null == _slider)
            return;

        _slider.isOn = _value;
    }

    public static void SetValue(Image _image, float _value)
    {
        if (null == _image)
            return;

        _image.fillAmount = _value;
    }
    #endregion

    public static bool GetValue(Toggle _slider)
    {
        if (null == _slider)
            return false;

        return _slider.isOn;
    }

    

    #region - SetIcon
    public static void SetIcon(Image _image, string _path)
    {
        if (null == _image)
            return;

        if( string.IsNullOrWhiteSpace(_path) == true )
        {
            _image.gameObject.SetActive(false);
            return;
        }

        _image.gameObject.SetActive(true);
        _image.sprite = ResUtil.Load<Sprite>(_path);
    }
    public static void SetIcon(Image[] _image, string _path)
    {
        if (null == _image)
            return;

        for(int i=0;i<_image.Length;++i)
        {
            SetIcon(_image[i], _path);
        }
    }
    #endregion

    public static bool IsShow(GameObject _gameObject)
    {
        if (null == _gameObject)
            return false;

        return _gameObject.activeSelf;
    }
    public static bool IsShow(Graphic _gameObject)
    {
        if (null == _gameObject)
            return false;

        return _gameObject.gameObject.activeSelf;
    }

    public static void SetEnable(Button _button, bool _enable)
    {
        if (null == _button)
            return;
        _button.interactable = _enable;
    }

    public static void SetEnable(Button[] _button, bool _enable)
    {
        if (null == _button)
            return;

        for (int i = 0; i < _button.Length; ++i)
        {
            SetEnable(_button[i], _enable);
        }
    }

    #region - SetShow
    public static void SetShow(Transform _object, bool _isActive)
    {
        if (null == _object)
            return;

        SetShow(_object.gameObject, _isActive);
    }
    public static void SetShow(Transform[] _object, bool _isActive)
    {
        if (null == _object)
            return;

        for (int i = 0; i < _object.Length; ++i)
        {
            SetShow(_object[i], _isActive);
        }
    }
    
    public static void SetShow(GameObject _gameObject, bool _isActive)
    {
        if (null == _gameObject)
            return;
        _gameObject.SetActive(_isActive);
    }

    public static void SetShow(GameObject[] _gameObject, bool _isActive)
    {
        if (null == _gameObject)
            return;

        for (int i = 0; i < _gameObject.Length; ++i)
        {
            SetShow(_gameObject[i], _isActive);
        }
    }

    public static void SetShow(Graphic _gameObject, bool _isActive)
    {
        if (null == _gameObject)
            return;
        _gameObject.gameObject.SetActive(_isActive);
    }

    public static void SetShow(Graphic[] _gameObject, bool _isActive)
    {
        if (null == _gameObject)
            return;
        for (int i = 0; i < _gameObject.Length; ++i)
        {
            SetShow(_gameObject[i], _isActive);
        }
    }
    public static void SetShow(Button _object, bool _isActive)
    {
        if (null == _object)
            return;

        _object.gameObject.SetActive(_isActive);
    }
    public static void SetShow(Button[] _object, bool _isActive)
    {
        if (null == _object)
            return;

        for( int i=0; i<_object.Length; ++i )
        {
            if (_object[i] == null)
                continue;

            _object[i].gameObject.SetActive(_isActive);
        }
    }
    #endregion

    public static void SetAlpha(Graphic _graphic, float _alpha)
    {
        if (null == _graphic)
            return;

        Color _color = _graphic.color;
        _color.a = _alpha;
        _graphic.color = _color;
    }

    

    public static void SetColor(Graphic _graphic, Color _color)
    {
        if (null == _graphic)
            return;

        _graphic.color = _color;
    }

    public static void SetColor(Graphic[] _graphic, Color _color)
    {
        if (null == _graphic)
            return;

        for (int i = 0; i < _graphic.Length; ++i)
        {
            SetColor(_graphic[i], _color);
        }
    }
}
