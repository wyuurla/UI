using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(UIButton))]
public class UIButtonEditor : UnityEditor.UI.ButtonEditor
{
    UIButton m_owner;

    UIButton getOwner { get { if (null == m_owner) { m_owner = target as UIButton; } return m_owner; } }
   
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Draw(getOwner);
    }

    public void Draw(UIButton _button)
    {
        _button.sound_click = (UISound.eSOUND_ID)EditorGUILayout.EnumPopup("sound id : ", _button.sound_click);
        _button.actionType = (UIAction.eACTION_TYPE)EditorGUILayout.EnumPopup( "action type :", _button.actionType);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_button);
        }
    }
}
