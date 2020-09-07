using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;

public static class CreateAddressablesLoader
{
    public static async Task InitAssets<T>(string assetNameOrLabel, List<T> createdAssets)
      where T : Object
    {
        var locations = await Addressables.LoadResourceLocationsAsync(assetNameOrLabel).Task;
        foreach (var location in locations)
            createdAssets.Add(await Addressables.InstantiateAsync(location).Task as T);
    }

}
