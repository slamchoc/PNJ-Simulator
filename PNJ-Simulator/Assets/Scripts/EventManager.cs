﻿using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All the types of event that can be raised
/// </summary>
public enum EventType
{
    CHANGE_SCENE,
    WIN,
    EVENTBEFOREWIN,
    TITRE_APPARAIT,
    CINEMATIC_ENDED,
    CINEMATIC_BEGIN,
    THINK_ABOUT_BROOM,
    PLAYER_DEAD,
    SELECTION_CHANGED,
    SELECTION_VALIDATED,
    END_SCENE,
    ATTACK_ENNEMY,
    ATTACK_PLAYER,
    LOOSE_LIFE_ENNEMY,
    LOOSE_LIFE_PLAYER,
    NEW_SCENE,
    KILL_MONSTER,
    DAMAGE_ENNEMY,
    DAMAGE_PLAYER,
    SELL,
    GOLD_CHANGE,
    REPUTATION_CHANGE,
    GIVE_QUEST,
    END_DAY,
    SLAM_DOOR,
    MENU_ENTERED,
    MENU_EXIT,
    QUIT_GAME,
    PLAY_SOUND_ONCE,
    PLAY_SOUND_LOOP,
    STOP_SOUND
}

public delegate void Callback();

public delegate void Callback<T>(T arg);



public static class EventManager
{

    /// <summary>
    /// Dictionnary that translate the events to callbacks, methods to call 
    /// when the event is raised
    /// </summary> 
    private static Dictionary<EventType, List<Delegate>> dicoEventAction = new Dictionary<EventType, List<Delegate>>();

    /// <summary>
    /// Subscribe a method to an event
    /// </summary>
    /// <param name="even">The event</param>
    /// <param name="method">The method</param>
    public static void addActionToEvent(EventType even, Callback method)
    {
        if (!dicoEventAction.ContainsKey(even))
        {
            dicoEventAction.Add(even, new List<Delegate>());
        }
        dicoEventAction[even].Add(method);
    }

    /// <summary>
    /// Subscribe a method to an event
    /// </summary>
    /// <param name="even">The event</param>
    /// <param name="method">The method</param>
    public static void addActionToEvent<T>(EventType even, Callback<T> method)
    {
        if (!dicoEventAction.ContainsKey(even))
        {
            dicoEventAction.Add(even, new List<Delegate>());
        }
        dicoEventAction[even].Add(method);
    }

    /// <summary>c
    /// Unsubscribe a method to an event
    /// </summary>
    /// <param name="even">The event to unsubscribe</param>
    /// <param name="method">The method to unsubscribe</param>
    public static void removeActionFromEvent(EventType even, Callback method)
    {
        if (dicoEventAction.ContainsKey(even))
        {
            dicoEventAction[even].Remove(method);
        }
    }

    /// <summary>
    /// Unsubscribe a method to an event
    /// </summary>
    /// <param name="even">The event to unsubscribe</param>
    /// <param name="method">The method to unsubscribe</param>
    public static void removeActionFromEvent<T>(EventType even, Callback<T> method)
    {
        if (dicoEventAction.ContainsKey(even))
        {
            dicoEventAction[even].Remove(method);
        }
    }

    /// <summary>
    /// Raise an event, so it will trigger all the subscribed methods
    /// </summary>
    /// <param name="eventToCall">The event to raise</param>
    public static void raise(EventType eventToCall)
    {
        foreach (Delegate d in dicoEventAction[eventToCall].ToArray())
        {
            Callback c = (Callback)d;
            if (c != null)
                c();
        }
    }

    /// <summary>
    /// Raise an event, so it will trigger all the subscribed methods
    /// </summary>
    /// <param name="eventToCall">The event to raise</param>
    /// <param name="arg">The argument that we pass with the event</param>

    public static void raise<T>(EventType eventToCall, T arg)
    {
        foreach (Delegate d in dicoEventAction[eventToCall].ToArray())
        {
            Callback<T> c = (Callback<T>)d;
            if (c != null)
                c(arg);
        }
    }
}
