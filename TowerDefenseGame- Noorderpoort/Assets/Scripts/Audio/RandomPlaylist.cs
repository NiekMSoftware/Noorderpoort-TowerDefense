using UnityEngine;

public class RandomPlaylist : MonoBehaviour {
    [Header("All music")]
    [SerializeField] AudioClip[] clips;

    [Space]
    [SerializeField] AudioSource source;

    //So the music doesnt play the same one twice in a row
    private AudioClip prevClip;
    
    
    void Start() {
        NewClip();
    }

    void Update() {

        //If done playing music, play another
        if (!source.isPlaying) {
            this.NewClip();
        }
    }

    /// <summary>
    /// Plays a new music track
    /// </summary>
    void NewClip() {
        bool hasClip = false;
        int clipNum = 1;

        //Deadly if there is only 1 music track
        while (!hasClip)
        {
            clipNum = Random.Range(0, this.clips.Length);

            //Stops the loop when it gets a new track
            if (clips[clipNum] != prevClip)
            {
                hasClip = true;
            }
        }
        source.PlayOneShot(clips[clipNum]);

        prevClip = clips[clipNum];

        print("You are now listening to: " + clips[clipNum]);
    }
}
