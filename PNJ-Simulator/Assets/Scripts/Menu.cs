using UnityEngine;
using System;
using System.Collections.Generic;

public class Menu
{

    public string text { get; private set; }
    public List<Pair<Callback, String>> options { get; private set; }
    public int position { get; private set; }
    
    public Menu(List<Pair<Callback, String>> _options, String _text)
    {
        position = _options.Count - 1;
        text = _text;
        options = _options;
    }

    public Menu(Menu _nextOne, String _text)
    {
        position = 0;
        text = _text;
        options = new List<Pair<Callback, string>> { new Pair<Callback,String>(()=>
                                                                            {
                                                                                EventManager.raise<Menu>(EventType.MENU_ENTERED,_nextOne);
                                                                            },"Continue...")};
    }

	public void call()
    {
        options[position].First();
    }
    public void incrementPosition()
    {
        position++;
        if (position >= options.Count)
            position = 0;
        EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE,SoundsType.POUIT_DEPLACEMENT );
    }
    public void decrementPosition()
    {
        position--;
        if (position < 0)
            position = options.Count - 1;
        EventManager.raise<SoundsType>(EventType.PLAY_SOUND_ONCE, SoundsType.POUIT_DEPLACEMENT);
    }
}
