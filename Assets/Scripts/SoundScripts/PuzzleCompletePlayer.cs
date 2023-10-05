using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleCompletePlayer : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPuzzleComplete;

    private DoubleDoorPuzzle _doorPuzzle1;
    private DoubleDoorPuzzle _doorPuzzle2;

    private void Awake()
    {
        _doorPuzzle1 = new DoubleDoorPuzzle();
        _doorPuzzle2= new DoubleDoorPuzzle();
    }

    public void OpenDoor(int puzzle, int door)
    {
        if (puzzle == 0) 
        {
            _doorPuzzle1.OpenDoor(door);
        }
        else
        {
            _doorPuzzle2.OpenDoor(door);
        }
    }

    public void OpenDoor1()
    {
        if (_doorPuzzle1.OpenDoor(0))
        {
            _onPuzzleComplete.Invoke();
        }
    }
    public void OpenDoor2()
    {
        if (_doorPuzzle1.OpenDoor(1))
        {
            _onPuzzleComplete.Invoke();
        }
    }
    public void OpenDoor3()
    {
        if (_doorPuzzle2.OpenDoor(0))
        {
            _onPuzzleComplete.Invoke();
        }
    }
    public void OpenDoor4()
    {
        if (_doorPuzzle2.OpenDoor(1))
        {
            _onPuzzleComplete.Invoke();
        }
    }
}
