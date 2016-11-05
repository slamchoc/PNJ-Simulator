using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;

public enum Sounds
{
    AMBIANCE_FORET,
    AMBIANCE_FORGE,
    AMBIANCE_VILLAGE,
    COUP_EPEE,
    COUP_EPEE_ECLAIR,
    COUP1,
    COUP2,
    DEFONCAGE,
    EPEE_RANGEE,
    MUSIQUE_CINEMATIQUE,
    MUSIQUE_CINEMATIQUE2,
    MUSIQUE_CONNE,
    MUSIQUE_DONJON,
    MUSIQUE_HEROS,
    MUSIQUE_VILLAGE,
    PETIT_COUP1,
    PETIT_COUP2,
    PETIT_COUP3,
    PETIT_COUP4,
    PETIT_COUP5,
    PETIT_COUP6,
    PETIT_COUP7,
    PETIT_COUP8,
    PORTE_CASSEE,
    PORTE_OUVERTE,
    POUIT_ACCEPTATION,
    POUIT_DEPLACEMENT,
    POUIT_RETOUR,
    REUSSITE_EPIQUE,
    SON_FOULE
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

        EventManager.addActionToEvent<Sounds>(EventType.PLAY_SOUND_ONCE, playSound);
        EventManager.addActionToEvent<Sounds>(EventType.PLAY_SOUND_LOOP, playSoundLoop);
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
        GameObject audio = Instantiate(audioPrefab);
        audio.transform.parent = this.transform;

        Debug.Log(soundToPlay+" to "+ translateAudioToString.Keys.ElementAt((int)soundToPlay));

        audio.GetComponent<Sound>().playOnce(translateAudioToString.Values.ElementAt((int)soundToPlay));
    }

    public void playSoundLoop(Sounds soundToPlayLoop)
    {
        GameObject audio = Instantiate(audioPrefab);
        audio.transform.parent = this.transform;

        Debug.Log(soundToPlayLoop + " to " + translateAudioToString.Keys.ElementAt((int)soundToPlayLoop));

        audio.GetComponent<Sound>().playLoop(translateAudioToString.Values.ElementAt((int)soundToPlayLoop));
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<Sounds>(EventType.PLAY_SOUND_ONCE, playSound);
        EventManager.removeActionFromEvent<Sounds>(EventType.PLAY_SOUND_LOOP, playSoundLoop);
    }
}
