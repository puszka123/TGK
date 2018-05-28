using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissionManager : MonoBehaviour
{

    bool _hasStone = false;
    bool _hasKey = false;
    public GameObject Stone;
    public GameObject Rim;
    public AudioClip[] horrorMusic;
    bool playMusic = false;
    public GameObject AudioObject;
    AudioSource audioSource;
    int audioIndex = 0;

    public bool HasKey { get { return _hasKey; } }

    // Use this for initialization
    void Start()
    {
        audioSource = AudioObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //rim find gold - check if the player can throw a stone
        if (_hasStone)
        {
            if (Input.GetKey(KeyCode.G))
            {
                ThrowStone();
            }
        }
        if (playMusic)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = horrorMusic[audioIndex++];
                audioSource.Play();
                if (audioIndex >= horrorMusic.Length)
                {
                    audioIndex = 0;
                }
            }
        }
        else if (audioSource.isPlaying) audioSource.Stop();
    }

    public void TakeStone()
    {
        Debug.Log("You've taken a stone");
        _hasStone = true;
    }

    public void TakeKey()
    {
        Debug.Log("You've taken a key");
        _hasKey = true;
    }

    //rim find gold mission:
    void ThrowStone()
    {
        _hasStone = false;
        Rigidbody clone = Instantiate(Stone.GetComponent<Rigidbody>(), transform.position, transform.rotation) as Rigidbody;
        Rim.SendMessage("LookAtStone", clone);
        clone.AddForce(transform.TransformDirection(Vector3.forward * 1000));
    }

    public void PlayHorrorMusic(bool music)
    {
        playMusic = music;
    }
}
