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
        position = 0;
        text = _text;
        options = _options;
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
    }
    public void decrementPosition()
    {
        position--;
        if (position < 0)
            position = options.Count - 1;
    }
}
