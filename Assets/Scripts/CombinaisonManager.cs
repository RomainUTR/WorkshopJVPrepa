using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CombinaisonManager : MonoBehaviour
{
    public static CombinaisonManager Instance;

    [Header("Buttons")]
    public string[] ButtonsClickedName;
    public Material RedMaterial, WhiteMaterial, BlackMaterial, GreenMaterial;

    public SpriteRenderer[] TutorialRenderers; 
    public Sprite[] TutorialSprites;

    private List<GameObject> ButtonsGO = new List<GameObject>();
    private int _indexButton;
    private bool _canClick = true;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < TutorialRenderers.Length; i++)
        {
            if (i < TutorialSprites.Length) 
            {
                TutorialRenderers[i].sprite = TutorialSprites[i];
                TutorialRenderers[i].color = Color.white; 
            }
        }
    }

    public void HandleButton(InteractiveObject obj)
    {
        if (_canClick == false) return; 

        if (obj.objectName == ButtonsClickedName[_indexButton])
        {
            TutorialRenderers[_indexButton].color = new Color(0.3f, 0.3f, 0.3f, 1f);
            
            _indexButton++;
            ButtonsGO.Add(obj.gameObject);

            if (_indexButton >= ButtonsClickedName.Length)
            {
                foreach (GameObject go in ButtonsGO)
                {
                    go.GetComponentInChildren<MeshRenderer>().material = GreenMaterial;
                    go.GetComponent<InteractiveZone>().enabled = false;
                    _canClick = false;
                    ResetTutorial();
                }
                _indexButton = 0;
            }
        }
        else
        {
            foreach (GameObject go in ButtonsGO)
            {
                go.GetComponentInChildren<MeshRenderer>().material = RedMaterial;
            }

            StartCoroutine(FailCombinaison());

            ResetTutorial();

            _indexButton = 0;
        }
    }

    private IEnumerator FailCombinaison()
    {
        yield return new WaitForSeconds(2);

        foreach (GameObject go in ButtonsGO)
        {
            go.GetComponentInChildren<MeshRenderer>().material = WhiteMaterial;
        }
    }

    private void ResetTutorial()
    {
        foreach (SpriteRenderer renderer in TutorialRenderers)
        {
            renderer.color = Color.white; 
        }
    }
}