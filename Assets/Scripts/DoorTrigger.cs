using DG.Tweening;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private Vector3 openOffset = new Vector3(3f, 0f, 0f);
    [SerializeField] private float duration = 0.6f;
    [SerializeField] private Ease easeType = Ease.InOutQuad;

    [Header("Sons (optionnel)")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;


    [Header("Var :")]
    public GameObject Door1;
    public GameObject Door2;


    public Vector3 closedPosition1;
    public Vector3 openPosition1;

    public Vector3 closedPosition2;
    public Vector3 openPosition2;

    private bool isOpen1;
    private bool isOpen2;
    private bool isAnimating = false;

    private void Start()
    {
        isOpen1 = false;
        isOpen2 = false;
        closedPosition1 = Door1.transform.position;
        closedPosition2 = Door2.transform.position;
        openPosition1 = Door1.transform.position + openOffset;
        openPosition2 = Door2.transform.position + openOffset;

    }
    private void OnTriggerEnter(Collider other)
    {
        //if (isAnimating) return;

        if (isOpen1 && isOpen2)
        {
            CloseDoor1(); 
            CloseDoor2();
            print("close");
        }
            
        else
        {
            OpenDoor1();
            OpenDoor2();
            print("open");
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        //if (isAnimating) return;

        if (isOpen1 && isOpen2)
        {
            CloseDoor1();
            CloseDoor2();
            print("close");
        }

        else
        {
            OpenDoor1();
            OpenDoor2();
            print("open");
        }
    }





    public void OpenDoor1()
    {
        if (isOpen1 || isAnimating) return;

        isAnimating = true;
        PlaySound(openSound);

        transform.DOLocalMove(openPosition1, duration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                isOpen1 = true;
                isAnimating = false;
            });
    }

    public void CloseDoor1()
    {
        if (!isOpen1 || isAnimating) return;

        isAnimating = true;
        PlaySound(closeSound);

        transform.DOLocalMove(closedPosition1, duration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                isOpen1 = false;
                isAnimating = false;
            });
    }

    public void OpenDoor2()
    {
        if (isOpen2 || isAnimating) return;

        isAnimating = true;
        PlaySound(openSound);

        transform.DOLocalMove(openPosition2, duration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                isOpen2 = true;
                isAnimating = false;
            });
    }

    public void CloseDoor2()
    {
        if (!isOpen2 || isAnimating) return;

        isAnimating = true;
        PlaySound(closeSound);

        transform.DOLocalMove(closedPosition2, duration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                isOpen2 = false;
                isAnimating = false;
            });
    }






    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }

    private void OnDestroy()
    {
        // Annuler le tween si l'objet est détruit pendant l'animation
        transform.DOKill();
    }
}
