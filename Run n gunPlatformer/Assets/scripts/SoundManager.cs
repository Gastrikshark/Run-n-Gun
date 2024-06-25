using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFXSource;

    [Header("SFX")]
    public AudioClip Bullet;
    public AudioClip Jump;
    public AudioClip hit;


    public void Playsoundeffect(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}

