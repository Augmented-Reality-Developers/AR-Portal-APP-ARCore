using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement;

public class LoadedAddressableLocations : MonoBehaviour
{
    [SerializeField] private string _label;

    public IList<UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation> AssetLocations { get; } = new List<UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation>();

    private void Start()
    {
        InitAndWaitUntilLoaded(_label);
    }

    public async Task InitAndWaitUntilLoaded(string label)
    {
        await AddressableLocationLoader.GetAll(label, AssetLocations);

        foreach (var location in AssetLocations)
        {
            //ASSETS ARE FULLY LOADED
            //PERFORM ADDITIONAL OPERATIONS HERE
            Debug.Log(location.PrimaryKey);
        }
    }

}
