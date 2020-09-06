using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement;
using UnityEngine.AddressableAssets;


public static class AddressableLocationLoader
{
    public static async Task GetAll(string label, IList<UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation> loadedLocations)
    {
        var unloadedLocations = await Addressables.LoadResourceLocationsAsync(label).Task;

        foreach (var location in unloadedLocations)
            loadedLocations.Add(location);
    }
}
