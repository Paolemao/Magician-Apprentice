using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampfirePanel : UiPanel<CampfirePanel> {

    public Button Eat;
    public Button Sleep;
    public Button ListenToMusic;
    public Button SetSkills;
    public Button Save;
    public Button Leave;


    private void OnEnable()
    {

        //为按钮设置监听者，监听事件
        Eat.onClick.AddListener(EatEvent);
        Sleep.onClick.AddListener(SleepEvent);
        ListenToMusic.onClick.AddListener(MusicEvent);
        SetSkills.onClick.AddListener(SetSkillEvent);
        Save.onClick.AddListener(SaveEvent);
        Leave.onClick.AddListener(LeveEvent);
    }
    private void OnDisable()
    {
        Eat.onClick.RemoveListener(EatEvent);
        Sleep.onClick.RemoveListener(SleepEvent);
        ListenToMusic.onClick.RemoveListener(MusicEvent);
        SetSkills.onClick.RemoveListener(SetSkillEvent);
        Save.onClick.RemoveListener(SaveEvent);
        Leave.onClick.RemoveListener(LeveEvent);
    }
    //与背包系统关联=》打开背包=》高亮食物=》选择食物=》加血
    void EatEvent()
    {

    }
    //睡觉的动画=》UI（按钮）（白天，黄昏，晚上）=》切换天空盒/主光源的亮度
    void SleepEvent()
    {
        
        
    }
    //音乐管理器=》调取音乐播放
    void MusicEvent()
    {

    }

    //与魔法书系统关联=》打开魔法书=》合成魔法
    void SetSkillEvent()
    {

    }

    //与本地数据保存关联
    void SaveEvent()
    {

    }
    //关闭
    void LeveEvent()
    {
        GameController.Instance.Player.leaveCampfire = true;
    }


}
