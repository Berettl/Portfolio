using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header(" --- Audio Source ---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header(" --- Audio Clip ---")]
    public AudioClip title;
    public AudioClip destroyedCity;
    public AudioClip graveyard1;
    public AudioClip graveyard2;
    public AudioClip end;
    public AudioClip credits;


    public static AudioManager instance;


    private void Start()
    {
        musicSource.clip = title;
        musicSource.Play();

    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    //Keeps the Audio Manager in every scene
    void Awake()
    {
        if (instance == null) 
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

            DontDestroyOnLoad(gameObject);
    }

}

///use this code on other scripts to play sounds

// FindObjectOfType<AudioManager>().Play("Name of Audio Clips above");

