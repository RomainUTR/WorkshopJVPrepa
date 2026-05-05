using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractiveCombinationHandler : InteractiveObject
{
    [Header("Buttons")]
    public string[] ButtonsClickedName;
    List<string> ClickedNames;
    public Material RedMaterial, WhiteMaterial, BlackMaterial, GreenMaterial;

    //public SpriteRenderer[] TutorialRenderers; 
    //public Sprite[] TutorialSprites;

    private List<GameObject> ButtonsGO = new List<GameObject>();
    private int _indexButton;
    private bool _canClick = true;

    void Start()
    {
        //for (int i = 0; i < TutorialRenderers.Length; i++)
        //{
        //    if (i < TutorialSprites.Length) 
        //    {
        //        TutorialRenderers[i].sprite = TutorialSprites[i];
        //        TutorialRenderers[i].color = Color.white; 
        //    }
        //}

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
            go.GetComponentInChildren<MeshRenderer>().material = RedMaterial;
            //PlayerInteractor.Fin
        }

        yield return new WaitForSeconds(1.5f);
        ResetTutorial();
    }

    public void SucceedCombination()
    {
        print("cool");
        foreach (GameObject go in ButtonsGO)
        {
            //go.GetComponentInChildren<MeshRenderer>().material = GreenMaterial;
            //go.GetComponent<InteractiveZone>().enabled = false;
            _canClick = false;
            ResetTutorial();
        }

        if(interactions.Length > 0)
        PlayerInteractor.Instance.Interact(this);
    }
    public void ResetMaterial()
    {
        foreach(GameObject go in ButtonsGO)
        {
            go.GetComponentInChildren<MeshRenderer>().material = WhiteMaterial;
        }
    }
    private void ResetTutorial()
    {
        //foreach (SpriteRenderer renderer in TutorialRenderers)
        //{
        //    renderer.color = Color.white; 
        //}
    }
}