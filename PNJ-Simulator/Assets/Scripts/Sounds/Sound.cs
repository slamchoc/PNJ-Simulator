using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour
{

    /// <summary>
    /// Loop the sound, infinitly
    /// </summary>
    /// <param name="audioClip"></param>
    public void playLoop(AudioClip audioClip)
    {
        this.GetComponent<AudioSource>().clip = audioClip;
        this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// Play the sound once then destroy this gameobject
    /// </summary>
    /// <param name="audioClip"></param>
    public void playOnce(AudioClip audioClip)
    {
        StartCoroutine(PlaySoundCoroutine(audioClip));
    }

    IEnumerator PlaySoundCoroutine(AudioClip audioClip)
    {
        this.GetComponent<AudioSource>().PlayOneShot(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Stop this sound 
    /// </summary>
    public void stopPlaying()
    {
        this.GetComponent<AudioSource>().Stop();
    }
}
