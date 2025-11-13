using UnityEngine;
using System.Collections;

public class P1ManageCharacter : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Trigger Name / Trigger End Name / AudioClip / DelayTime / Trigger Name2 / Trigger2 End Name")]
    public P1Struct[] _p1Variable;


    [SerializeField]
    [Tooltip("Animator Controller on the Character Object")]
    private Animator AC_Character;
    //[SerializeField] private string EndTrigger = "end";
    private int Counter = -1;
    [SerializeField]
    [Tooltip("Audio Source in the LipSync Game Object")]
    private AudioSource _audioSource;
    private bool AudioRunning = false;



    // Update is called once per frame
    void Update()
    {
        if (AudioRunning)
        {
            if (_audioSource.isPlaying)
            {
                return;
            }
            else
            {
                AudioRunning = false;
                StartCoroutine(EndTrigger());
            }
        }
    }

    public void NextStep(int step)
    {
        Counter = step;
        if (Counter == _p1Variable.Length)
        {
            return;
        }

        //set the audio sources audio calip to the referenced clip
        if (_p1Variable[Counter]._audioClip != null)
        {
            _audioSource.clip = _p1Variable[Counter]._audioClip;
            _audioSource.Play();
            AudioRunning = true;
        }
        if (_p1Variable[Counter].TriggerName != "")
        {
            AC_Character.SetTrigger(_p1Variable[Counter].TriggerName);
        }

        if (_p1Variable[Counter].DelayTime > 0)
            {
                StartCoroutine(SecondaryTrigger(_p1Variable[Counter].DelayTime, _p1Variable[Counter].TriggerName2));
            }
        
    }


    IEnumerator EndTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        //send end trinner
        if (_p1Variable[Counter].TriggerEnd != "")
        {
            Debug.Log(_p1Variable[Counter].TriggerEnd);
            AC_Character.SetTrigger(_p1Variable[Counter].TriggerEnd);
        }
        if (_p1Variable[Counter].DelayTime > 0)
        {
            AC_Character.SetTrigger(_p1Variable[Counter].TriggerEnd2);
        }
        NextStep(Counter + 1);
    }

    IEnumerator SecondaryTrigger(float delay, string trigger2)
    {
        yield return new WaitForSeconds(delay);
        AC_Character.SetTrigger(trigger2);
    }
}
