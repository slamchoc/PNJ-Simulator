using UnityEngine;
using System;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

    private Callback[] options;
    private int position=0;

    public Menu(Delegate[] _options)
    {
        options = (Callback[])_options;
    }

	public void call()
    {
        options[position]();
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
