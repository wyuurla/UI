using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief UISound
 * @detail 유아이에서 사용하는 사운드 출력을 위한 클래스. 
 * @date 2024-02-20
 * @version 1.0.0
 * @author kij
 */
public class UISound : MonoBehaviour
{
    public enum eSOUND_ID
    {
        none = 0,
        dialog_open     = 10001,
        dialog_close    = 10002,
        button_click    = 20001,
    }

    static public void PlaySound(eSOUND_ID _id)
    {
        if (_id == eSOUND_ID.none)
            return;

        //사운드 출력코드.
    }

    public eSOUND_ID sound_enable;
    public eSOUND_ID sound_disable;

    private void OnEnable()
    {
        PlaySound(sound_enable);
    }

    private void OnDisable()
    {
        PlaySound(sound_disable);
    }
}
