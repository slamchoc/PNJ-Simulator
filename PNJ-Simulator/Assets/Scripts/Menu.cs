using UnityEngine;
using System;

public class Menu : MonoBehaviour {

    private string text;
    private Pair<Callback,String>[] options;
    public int position { get; private set; }

    public Menu(Pair<Callback,String>[] _options, String _text)
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
        if (position >= options.Length)
            position = 0;
    }
    public void decrementPosition()
    {
        position--;
        if (position <= 0)
            position = options.Length - 1;
    }
}
