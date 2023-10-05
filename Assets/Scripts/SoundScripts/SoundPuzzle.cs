using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPuzzle : MonoBehaviour
{
    // Idea for a sound puzzle where the players would need to activate 4 levers in order. Each lever will make a sound and the players will have to match the sounds to a melody.

    [SerializeField] private Default2dSoundScript sound1;
    [SerializeField] private Default2dSoundScript sound2;
    [SerializeField] private Default2dSoundScript sound3;
    [SerializeField] private Default2dSoundScript sound4;

    [SerializeField] private Default2dSoundScript hintSound;

    [SerializeField] private Default2dSoundScript puzzleCompletionSound;

    public bool sound1Activated = false;
    public bool sound2Activated = false;
    public bool sound3Activated = false;
    public bool sound4Activated = false;

    public bool sound1Active = false;
    public bool sound2Active = false;
    public bool sound3Active = false;
    public bool sound4Active = false;


    private void SetSoundActive(int soundNum)
    {
        if(soundNum == 1 || sound1Active)
        {
            if(!sound1Active)
            {
                sound1Active=true;
                return;
            }
            
            if(soundNum == 2 || sound2Active)
            {
                if(!sound2Active)
                {
                    sound2Active=true;
                    return;
                }
                if(soundNum == 3 || sound3Active)
                {
                    if(!sound3Active)
                {
                    sound3Active=true;
                    return;
                }
                    if(soundNum == 4)
                    {
                        sound4Active=true;
                        PuzzleComplete();
                    }
                    else
                    {
                        sound1Active=false;
                        sound2Active=false;
                        sound3Active=false;
                    }
                    
                }
                else
                {
                    sound1Active=false;
                    sound2Active=false;
                    sound3Active=false;
                }
            }
            else
            {
                sound1Active=false;
                sound2Active=false;
                sound3Active=false;
            }
        }
        else
        {
            sound1Active=false;
            sound2Active=false;
            sound3Active=false;
        }
    }

     private void PuzzleComplete()
     {
        puzzleCompletionSound.PlaySound();
     }



    private void Update()
    {
        if(sound1Activated)
        {
            sound1.PlaySound();
            SetSoundActive(1);
            sound1Activated = false;
        }
        else if(sound2Activated)
        {
            sound2.PlaySound();
            SetSoundActive(2);
            sound2Activated = false;
        }
        else if(sound3Activated)
        {
            sound3.PlaySound();
            SetSoundActive(3);
            sound3Activated = false;
        }
        else if(sound4Activated)
        {
            sound4.PlaySound();
            SetSoundActive(4);
            sound4Activated = false;
        }
        
    }
}
