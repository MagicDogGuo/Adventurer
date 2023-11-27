using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 音效部分都呼叫他撥放(音樂/音效)
/// </summary>
public class AudioManager : MonoSingleton<AudioManager>
{
    /// <summary>
    /// 音樂用
    /// </summary>
    private AudioSource mAudioSource;
    /// <summary>
    /// 音效用
    /// </summary>
    private AudioSource mVoiceSource;
    public AudioSource VoiceSource
    {
        get { return mVoiceSource; }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected void Awake()
    {
        mAudioSource = this.gameObject.AddComponent<AudioSource>();
        mVoiceSource = this.gameObject.AddComponent<AudioSource>();
        if (mAudioSource == null)
            Debug.LogError("===Audio source null! cannot play audio!");
        if (mVoiceSource == null)
            Debug.LogError("===Voice source null! cannot play voice!");

        mAudioSource.volume = 0.4f;
    }

    /// <summary>
    /// 塞AudioClip進去撥放音樂檔,可以設定是否循環撥放
    /// </summary>
    /// <param name="music"></param>
    /// <param name="isloop"></param>
    public void PlayMusic(AudioClip music)
    {
        if (mAudioSource != null && music != null)
        {
            if (mAudioSource.isPlaying)
                mAudioSource.Stop();
            mAudioSource.loop = false;
            mAudioSource.clip = music;
            mAudioSource.Play();
        }
    }

    /// <summary>
    /// 塞AudioClip進去撥放音樂檔,可以設定是否循環撥放
    /// </summary>
    /// <param name="music"></param>
    /// <param name="isloop"></param>
    public void PlayLoopMusic(AudioClip music)
    {
        if (mAudioSource != null && music != null)
        {
            if (mAudioSource.isPlaying)
                mAudioSource.Stop();
            mAudioSource.loop = true;
            mAudioSource.clip = music;
            mAudioSource.Play();
        }
    }

    public void PlayVoice(AudioClip voice)
    {
        if (mVoiceSource != null)
        {
            if (mVoiceSource.isPlaying)
                mVoiceSource.Stop();
            mVoiceSource.loop = false;
            mVoiceSource.clip = voice;
            mVoiceSource.Play();
        }
    }

    public void StopVoice()
    {
        if (mVoiceSource.isPlaying)
            mVoiceSource.Stop();
    }


    public void StopMusic()
    {
        if (mVoiceSource != null)
        {
            mVoiceSource.Stop();
        }
    }

    /// <summary>
    /// 使用PlayOneShot撥放音效檔
    /// </summary>
    /// <param name="fx"></param>
    public void PlaySoundFx(AudioClip fx)
    {
        if (mVoiceSource != null && fx != null)
        {
            mVoiceSource.PlayOneShot(fx);
        }
    }
}

