using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource; // èrÛd≥o düwiÍku
    public AudioClip[] musicTracks; // Tablica utworÛw muzycznych

    private int currentTrackIndex = 0; // Indeks aktualnego utworu

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource nie jest przypisany do MusicManager!");
            return;
        }

        if (musicTracks.Length == 0)
        {
            Debug.LogError("Brak utworÛw w tablicy MusicTracks!");
            return;
        }

        PlayNextTrack(); // Rozpocznij odtwarzanie pierwszego utworu
    }

    private void Update()
    {
        // Jeúli muzyka siÍ skoÒczy≥a, odtwÛrz nastÍpny utwÛr
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        if (musicTracks.Length == 0)
            return;

        // OdtwÛrz aktualny utwÛr
        audioSource.clip = musicTracks[currentTrackIndex];
        audioSource.Play();

        // Przejdü do nastÍpnego utworu
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length; // ZapÍtlenie listy
    }
}
