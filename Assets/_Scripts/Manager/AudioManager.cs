using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [SerializeField] private AudioSource backGroundAudio;
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioSource weaponEffect;
    [SerializeField] private AudioClip backGroundClip;
    [SerializeField] private AudioClip swordClip;
    [SerializeField] private AudioClip swordSlideClip;
    [SerializeField] private AudioClip bowClip;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip itemClip;
    [SerializeField] private AudioClip clickClip;

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
                PlayClip(weaponEffect, swordClip);
                break;
            case SoundEffectType.SwordSlide:
                PlayClip(weaponEffect, swordSlideClip);
                break;
            case SoundEffectType.Bow:
                PlayClip(weaponEffect, bowClip);
                break;
            case SoundEffectType.Explosion:
                PlayClip(soundEffect, explosionClip);
                break;
            case SoundEffectType.Item:
                PlayClip(soundEffect, itemClip);
                break;
            case SoundEffectType.Click:
                PlayClip(soundEffect, clickClip);
                break;
        }
        
    }

    private void PlayClip(AudioSource audio, AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }
}


public enum SoundEffectType
{
    Sword,
    SwordSlide,
    Bow,
    Explosion,
    Item,
    Click
}
