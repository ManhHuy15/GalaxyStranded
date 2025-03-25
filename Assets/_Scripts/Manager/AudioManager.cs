using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [SerializeField] private AudioSource backGroundAudio;
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioSource weaponEffect;
    [SerializeField] private AudioSource gameoverEffect;
    [SerializeField] private AudioClip backGroundClip;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private List<AudioClip> listAudioEffectClips;

    void Start()
    {
        instance = this;
        PlayBackGroundMusic();
    }

    public void PlayBackGroundMusic()
    {
        backGroundAudio.clip = backGroundClip;
        backGroundAudio.Play();
    }

    public void PlaySoundEffect(SoundEffectType effectType)
    {
        switch (effectType)
        {
            case SoundEffectType.Sword:
            case SoundEffectType.SwordSlide:
            case SoundEffectType.Bow:
            case SoundEffectType.Explosion:
                PlayClip(weaponEffect, listAudioEffectClips[effectType.GetHashCode()]);
                break;
            default:
                PlayClip(soundEffect, listAudioEffectClips[effectType.GetHashCode()]);
                break;
        }
    }

    private void PlayClip(AudioSource audio, AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }

    public void GameOverMusic()
    {
        backGroundAudio.Stop();
        gameoverEffect.clip = gameOverClip;
        gameoverEffect.Play();
    }
}


public enum SoundEffectType
{
    Sword,
    SwordSlide,
    Bow,
    Explosion,
    Item,
    Click,
    Hurt,
    Die
}
