using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaylist : MonoBehaviour {
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource source;
    [SerializeField] float newClip;
    [SerializeField] float timer;
    
    
    // Start is called before the first frame update
    void Start() {
        this.source.gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if (timer >= this.newClip + 1) {
            this.NewClip();
            timer = 0;
        }
    }

    void NewClip() {
        int clipNum = Random.Range(0, this.clips.Length);

        if (!this.source.isPlaying) {
            this.source.loop = true;
            this.source.PlayOneShot(clips[clipNum]);
        }

        this.newClip = this.clips[clipNum].length;
    }
}
