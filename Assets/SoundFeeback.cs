using UnityEngine;

public enum SoundType
{
    Place,
    InvalidPlacement,
    Rotate,
    Click,
    Remove,
    WrongPlacement
}

public class SoundFeedback : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip placeClip;
    [SerializeField] private AudioClip invalidClip;
    [SerializeField] private AudioClip rotateClip;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip removeClip;
    [SerializeField] private AudioClip wrongClip;

    public void PlaySound(SoundType type)
    {
        switch (type)
        {
            case SoundType.Place:
                audioSource.PlayOneShot(placeClip);
                break;
            case SoundType.InvalidPlacement:
                audioSource.PlayOneShot(invalidClip);
                break;
            case SoundType.Rotate:
                audioSource.PlayOneShot(rotateClip);
                break;
            case SoundType.Click:
                audioSource.PlayOneShot(clickClip);
                break;
            case SoundType.Remove:
                audioSource.PlayOneShot(removeClip);
                break;
            case SoundType.WrongPlacement:
                audioSource.PlayOneShot(wrongClip);
                break;
        }
    }
}
