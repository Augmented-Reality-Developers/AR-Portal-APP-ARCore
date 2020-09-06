using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetRefData : MonoBehaviour
{
    [SerializeField] private AssetReference gameobject_mine;
    [SerializeField] private List<AssetReference> references = new List<AssetReference>();

    [SerializeField] private List<GameObject> completedObj = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        references.Add(gameobject_mine);

        StartCoroutine(LoadAndWaitUntilComplete());
    }

    private IEnumerator LoadAndWaitUntilComplete()
    {
        yield return AssetRefeLoad.CreateAssetsAddToList(references, completedObj);
    }
}
