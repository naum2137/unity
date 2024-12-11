using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource; // �r�d�o d�wi�ku
    public AudioClip[] musicTracks; // Tablica utwor�w muzycznych

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
            Debug.LogError("Brak utwor�w w tablicy MusicTracks!");
            return;
        }

        PlayNextTrack(); // Rozpocznij odtwarzanie pierwszego utworu
    }

    private void Update()
    {
        // Je�li muzyka si� sko�czy�a, odtw�rz nast�pny utw�r
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        if (musicTracks.Length == 0)
            return;

        // Odtw�rz aktualny utw�r
        audioSource.clip = musicTracks[currentTrackIndex];
        audioSource.Play();

        // Przejd� do nast�pnego utworu
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length; // Zap�tlenie listy
    }
}
