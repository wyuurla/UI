using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToolDraw
{
    public enum eDRAW_TYPE
    {
        TOOLBAR,
        LIST,
        INFO,
    }
    protected UITool m_tool;
    public eDRAW_TYPE drawType = eDRAW_TYPE.LIST;

    public UIToolDraw(UITool _tool)
    {
        m_tool = _tool;
    }

    public virtual void Draw()
    {
    }

    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
    }
}
