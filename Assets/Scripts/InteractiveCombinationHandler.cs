using RomainUTR.SLToolbox.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveCombinationHandler : InteractiveObject
{
    [Header("Buttons")]
    public string[] ButtonsClickedName;
    List<string> ClickedNames;
    public Material RedMaterial, WhiteMaterial, BlackMaterial, GreenMaterial;
    [Range(0f, 5f)] public float DurationOfErrorColor = 1.0f;

    public bool IsTutorial = false;
    [SLShowIf("IsTutorial")] public SpriteRenderer[] TutorialRenderers;
    [SLShowIf("IsTutorial")] public Sprite[] TutorialSprites;

    private List<GameObject> ButtonsGO = new List<GameObject>();
    private int _indexButton;
    private bool _canClick = true;

    void Start()
    {
        if (IsTutorial)
        {
            for (int i = 0; i < TutorialRenderers.Length; i++)
            {
                if (i < TutorialSprites.Length)
                {
                    TutorialRenderers[i].sprite = TutorialSprites[i];
                    TutorialRenderers[i].color = Color.white;
                }
            }
        } else
        {
            foreach (SpriteRenderer sr in TutorialRenderers)
            {
                sr.enabled = false;
            }
        }

        ClickedNames = new List<string>();
    }

    public void HandleButton(InteractiveObject obj)
    {
        if (_canClick == false) return;

        ClickedNames.Add(obj.objectName);
        ButtonsGO.Add(obj.gameObject);

        if(ClickedNames.Count >= ButtonsClickedName.Length)
        {

            bool correct = true;

            for (int i = 0; i < ButtonsClickedName.Length; i++)
            {
                if (ButtonsClickedName[i] != ClickedNames[i])
                {
                    correct = false;
                    break;
                }
            }

            if (correct) SucceedCombination(); else StartCoroutine(FailCombinaison());

            _indexButton = 0;
            ClickedNames.Clear();
        }
        else
        {
            _indexButton++;
        }
    }

    private IEnumerator FailCombinaison()
    {
        print("pas cool");
        
        foreach (GameObject go in ButtonsGO)
        {
            ChangeButtonMaterial(this, RedMaterial);
            //PlayerInteractor.Fin
        }

        yield return new WaitForSeconds(DurationOfErrorColor);
        ResetMaterial();
    }

    public void SucceedCombination()
    {
        print("cool");
        foreach (GameObject go in ButtonsGO)
        {
            ChangeButtonMaterial(this, GreenMaterial);
            _canClick = false;
        }

        if(interactions.Length > 0)
        PlayerInteractor.Instance.Interact(this);
    }

    public void ResetMaterial()
    {
        foreach(GameObject go in ButtonsGO)
        {
            ChangeButtonMaterial(this, WhiteMaterial);
        }
    }

    void ChangeButtonMaterial(InteractiveObject obj, Material targetMaterial)
    {
        foreach (GameObject go in ButtonsGO)
        {
            go.GetComponentInChildren<MeshRenderer>().material = targetMaterial;
        }
    }
}