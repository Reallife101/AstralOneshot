using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownAudio : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference threeSound;
    [SerializeField] FMODUnity.EventReference twoSound;
    [SerializeField] FMODUnity.EventReference oneSound;
    [SerializeField] FMODUnity.EventReference slaySound;

    // Use if all else fails
    [SerializeField] FMODUnity.EventReference CountSound;

    public void CallThreeSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(threeSound);
    }
    public void CallTwoSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(twoSound);
    }
    public void CallOneSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(oneSound);
    }
    public void CallSlaySound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(slaySound);
    }

    // Use if all else fails
    public void CallCountSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(CountSound);
    }
    
}
