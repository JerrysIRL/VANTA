using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorPuzzle
{
    private bool door1Open;
    private bool door2Open;
    private bool done;

    public bool OpenDoor(int door)
    {
        if (done) { return false; }

        if (door == 0) 
        { 
            door1Open = true;
        }
        else 
        {
            door2Open = true;
        }

        if (door1Open && door2Open) 
        {
            done= true;
            return true;
        }
        else 
        { 
            return false; 
        }
    }
}
