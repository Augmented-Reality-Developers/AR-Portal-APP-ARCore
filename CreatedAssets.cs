using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;

public class CreatedAssets : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private string _assetName;

    private List<GameObject> Assets { get; } = new List<GameObject>();

    private void Start()
    {
        CreateAndWaitUntilCompleted();
    }

    private async Task CreateAndWaitUntilCompleted()
    {
        await CreateAddressablesLoader.InitAssets(_label, Assets);
        await CreateAddressablesLoader.InitAssets(_assetName, Assets);

        foreach (var asset in Assets)
        {
            //OBJS IS NOW LOADED
            Debug.Log(asset.name);
        }

        //To destroy the loaded Addressable Asset
        //await Task.Delay(2000);
        //ClearAsset(Assets[0]);
    }

    private void ClearAsset(GameObject go)
    {
        Addressables.Release(go);
    }
}
