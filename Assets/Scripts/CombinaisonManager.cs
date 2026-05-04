using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CombinaisonManager : MonoBehaviour
{
    public static CombinaisonManager Instance;
    public TMP_Text DebugText;

    [Header("Buttons")]
    public string[] ButtonsClickedName;
    public Material RedMaterial, WhiteMaterial, BlackMaterial, GreenMaterial;

    private List<GameObject> ButtonsGO = new List<GameObject>();
    private int _indexButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        DebugText.gameObject.SetActive(false);
    }

    public void HandleButton(InteractiveObject obj)
    {
        if (obj.objectName == ButtonsClickedName[_indexButton])
        {
            _indexButton++;
            ButtonsGO.Add(obj.gameObject);
            //obj.gameObject.GetComponentInChildren<MeshRenderer>().material = BlackMaterial;

            if (_indexButton >= ButtonsClickedName.Length)
            {
                foreach (GameObject go in ButtonsGO)
                {
                    go.GetComponentInChildren<MeshRenderer>().material = GreenMaterial;
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
}