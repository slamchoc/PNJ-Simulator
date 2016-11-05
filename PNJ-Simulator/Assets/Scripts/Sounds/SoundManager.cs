using UnityEngine;
using System.Collections.Generic;

public enum Sounds
{
     PORTE_OUVERTE
}

public class SoundManager : MonoBehaviour {

    [SerializeField]
    public List<AudioClip> sources;

    public GameObject audioPrefab;

    public Dictionary<string, AudioClip> translateAudioToString;

    public Dictionary<string, Sound> actualSounds;

	// Use this for initialization
	void Start ()
    {
        actualSounds = new Dictionary<string, Sound>();
        DontDestroyOnLoad(this.gameObject);

        foreach(AudioClip clip in sources)
        {
            translateAudioToString.Add(clip.name, clip); 
        }

        EventManager.addActionToEvent<Sounds>(EventType.PLAY_SOUND, playSound);
	}

    public void playSound(Sounds soundToPlay)
    {
        switch(soundToPlay)
        {
            case Sounds.PORTE_OUVERTE:
                break;
            default:
                break;
        }
    }

    private void startSound(string name)
    {
        GameObject audio = Instantiate(audioPrefab);
        audio.transform.parent = this.transform;
        audio.GetComponent<Sound>().playOnce(translateAudioToString[name]);
    }
	
	
}
