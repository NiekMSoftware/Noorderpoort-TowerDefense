using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaylist : MonoBehaviour {
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource source;
    [SerializeField] float newClip;
    [SerializeField] float timer;

    private AudioClip prevClip;
    
    
    // Start is called before the first frame update
    void Start() {
        NewClip();
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if (timer >= this.newClip + 2) {
            this.NewClip();
            timer = 0;
        }
    }

    void NewClip() {
        bool hasClip = false;
        int clipNum = 1;
        while (!hasClip)
        {
            clipNum = Random.Range(0, this.clips.Length);
            if (clips[clipNum] != prevClip)
            {
                hasClip = true;
            }
        }

        while (source.isPlaying)
        {

        }
        source.PlayOneShot(clips[clipNum]);

        prevClip = clips[clipNum];
        this.newClip = this.clips[clipNum].length;

        print("You are now listening to: " + clips[clipNum]);
    }
}
