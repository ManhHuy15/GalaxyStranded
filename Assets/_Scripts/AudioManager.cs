using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [SerializeField] private AudioSource backGroundAudio;
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioSource swordEffect;
    [SerializeField] private AudioClip backGroundClip;
    [SerializeField] private AudioClip swordClip;
    [SerializeField] private AudioClip swordSlideClip;
    [SerializeField] private AudioClip bowClip;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip itemClip;

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
                swordEffect.clip = swordClip;
                break;
            case SoundEffectType.SwordSlide:
                swordEffect.clip = swordSlideClip;
                break;
            case SoundEffectType.Bow:
                soundEffect.clip = bowClip;
                break;
            case SoundEffectType.Explosion:
                soundEffect.clip = explosionClip;
                break;
            case SoundEffectType.Item:
                soundEffect.clip = itemClip;
                break;
        }
        soundEffect.Play();
    }

}


public enum SoundEffectType
{
    Sword,
    SwordSlide,
    Bow,
    Explosion,
    Item
}
