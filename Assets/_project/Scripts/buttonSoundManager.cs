using _project.Scripts;
using JetBrains.Annotations;
using UnityEngine;

public class buttonSoundManager : MonoBehaviour
{
    public AudioSource source;
    void Start()
    {
        source= GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
    
    public void playSong(AudioClip son)
    {
        source.PlayOneShot(son);
    }
}
