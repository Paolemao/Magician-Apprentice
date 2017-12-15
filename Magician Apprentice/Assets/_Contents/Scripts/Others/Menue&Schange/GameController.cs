using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//保证只有一个GameController存在
public class GameController : SingletonBehaviour<GameController> {

    public event Action BeforSceneUnload;
    public event Action AfterSceneLoad;

    public string startScene;

    private AudioSource audioSource2D;

    private PlayerCharacter _player;

    public PlayerCharacter Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
            }
            return _player;
        }
    }

    public Fader Fader
    {
        get
        {
            if (_fader == null)
            {
                _fader = GameObject.FindObjectOfType<Fader>();
            }
            return _fader;
        }

    }

    Fader _fader;

    public void Play2D(AudioClip clip,float volume)
    {
        if (audioSource2D)
        {
            audioSource2D.PlayOneShot(clip,volume);
        }
    }
    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadScene(string SceneName)
    {
        if (!Fader.isFading)
        {
            StartCoroutine(FadeAndSwitchScences(SceneName));
        }
    }

    private IEnumerator Start()
    {
        audioSource2D = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name== "Persistent")
        {
            //加载初始场景并等待加载完成
            yield return StartCoroutine(LoadSceneAndSetActive(startScene));

            //加载完成，淡入
            StartCoroutine(Fader.Fade(0f));
            MainMenuPanel.Show();
        }
    }
    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        //异步加载场景
        yield return SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);

        //获取最后一个场景
        Scene newLoadScene = SceneManager.GetSceneAt(SceneManager.sceneCount-1);
        //激活
        SceneManager.SetActiveScene(newLoadScene);
    }

    private IEnumerator FadeAndSwitchScences(string sceneName)
    {
        //淡出场景
        yield return StartCoroutine(Fader.Fade(1f));
        if (BeforSceneUnload!=null)
        {
            BeforSceneUnload();
        }

        //卸载当前场景
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        //记载新场景
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        //淡入场景
        yield return StartCoroutine(Fader.Fade(0f));
        if (AfterSceneLoad!=null)
        {
            AfterSceneLoad();
        }


    }













}
