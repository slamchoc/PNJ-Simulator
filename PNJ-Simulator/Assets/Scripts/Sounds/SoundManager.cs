using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;

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

    private string absolutePath = "Assets/Sound";

    private List<string> validExtensions = new List<string> { ".ogg", ".wav" }; 


    // Use this for initialization
    void Start ()
    {
        actualSounds = new Dictionary<string, Sound>();
        translateAudioToString = new Dictionary<string, AudioClip>();

        DontDestroyOnLoad(this.gameObject);


        // Loading of the sounds 
        sources = new List<AudioClip>();
        var info = new DirectoryInfo(absolutePath);
        FileInfo[] sourcesFiles = info.GetFiles().Where(f => IsValidFileType(f.Name)).ToArray();
        foreach (FileInfo s in sourcesFiles)
            StartCoroutine(LoadFile(s.FullName));

        foreach (AudioClip clip in sources)
        {
            translateAudioToString.Add(clip.name, clip); 
        }

        EventManager.addActionToEvent<Sounds>(EventType.PLAY_SOUND, playSound);
	}

    bool IsValidFileType(string fileName)
    {
        return validExtensions.Contains(Path.GetExtension(fileName));
    }

    IEnumerator LoadFile(string path)
    {
        WWW www = new WWW("file://" + path);
        print("loading " + path);

        AudioClip clip = www.GetAudioClip(false);
        while (clip.loadState != AudioDataLoadState.Loaded)
            yield return www;

        print("done loading");
        clip.name = Path.GetFileName(path);
        sources.Add(clip);
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
