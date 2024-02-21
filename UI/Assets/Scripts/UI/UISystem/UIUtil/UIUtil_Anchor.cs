using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static partial class UIUtil
{
    public enum eANCHOR
    {
        TopLeft = 1, TopCenter, TopRight,
        MiddleLeft, MiddleCenter, MiddleRight,
        BottomLeft, BottonCenter, BottomRight, BottomStretch,
        VertStretchLeft, VertStretchRight, VertStretchCenter,
        HorStretchTop, HorStretchMiddle, HorStretchBottom,
        StretchAll,
    }

    public static void SetAnchor(RectTransform source, eANCHOR _anchorType)
    {
        switch (_anchorType)
        {
            case eANCHOR.TopLeft:
                source.anchorMin = new Vector2(0, 1);
                source.anchorMax = new Vector2(0, 1);
                break;

            case eANCHOR.TopCenter:
                source.anchorMin = new Vector2(0.5f, 1);
                source.anchorMax = new Vector2(0.5f, 1);
                break;

            case eANCHOR.TopRight:
                source.anchorMin = new Vector2(1, 1);
                source.anchorMax = new Vector2(1, 1);
                break;

            case eANCHOR.MiddleLeft:
                source.anchorMin = new Vector2(0, 0.5f);
                source.anchorMax = new Vector2(0, 0.5f);
                break;

            case eANCHOR.MiddleCenter:
                source.anchorMin = new Vector2(0.5f, 0.5f);
                source.anchorMax = new Vector2(0.5f, 0.5f);
                break;

            case eANCHOR.MiddleRight:
                source.anchorMin = new Vector2(1, 0.5f);
                source.anchorMax = new Vector2(1, 0.5f);
                break;

            case eANCHOR.BottomLeft:
                source.anchorMin = new Vector2(0, 0);
                source.anchorMax = new Vector2(0, 0);
                break;

            case eANCHOR.BottonCenter:
                source.anchorMin = new Vector2(0.5f, 0);
                source.anchorMax = new Vector2(0.5f, 0);
                break;

            case eANCHOR.BottomRight:
                source.anchorMin = new Vector2(1, 0);
                source.anchorMax = new Vector2(1, 0);
                break;

            case eANCHOR.HorStretchTop:
                source.anchorMin = new Vector2(0, 1);
                source.anchorMax = new Vector2(1, 1);
                break;

            case eANCHOR.HorStretchMiddle:
                source.anchorMin = new Vector2(0, 0.5f);
                source.anchorMax = new Vector2(1, 0.5f);
                break;

            case eANCHOR.HorStretchBottom:
                source.anchorMin = new Vector2(0, 0);
                source.anchorMax = new Vector2(1, 0);
                break;

            case eANCHOR.VertStretchLeft:
                source.anchorMin = new Vector2(0, 0);
                source.anchorMax = new Vector2(0, 1);
                break;

            case eANCHOR.VertStretchCenter:
                source.anchorMin = new Vector2(0.5f, 0);
                source.anchorMax = new Vector2(0.5f, 1);
                break;

            case eANCHOR.VertStretchRight:
                source.anchorMin = new Vector2(1, 0);
                source.anchorMax = new Vector2(1, 1);
                break;

            case eANCHOR.StretchAll:
                source.anchorMin = new Vector2(0, 0);
                source.anchorMax = new Vector2(1, 1);
                source.sizeDelta = Vector2.zero;
                break;
        }
    }
}
