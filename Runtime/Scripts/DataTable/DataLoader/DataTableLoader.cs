using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TheMist.DataTable
{
    public static class DataTableLoader
    {
        const string DATA_ASSET_KEY = "DataTables";

        public static Dictionary<string, byte[]> allDataTableBytes;

        public static async UniTask LoadDataAssets()
        {
            allDataTableBytes = new Dictionary<string, byte[]>();
            //从Addressable中读取对应所有配置表的包，存入dic

            var handle = Addressables.LoadAssetsAsync<TextAsset>(DATA_ASSET_KEY, (datas) =>
            {
                Debug.Log("Data assets load CallBack");
            });
            await handle;
            if (handle.IsDone)
            {
                Debug.Log("AllOverLoad");
                for (int i = 0; i < handle.Result.Count; i++)
                {
                    allDataTableBytes.Add(handle.Result[i].name, handle.Result[i].bytes);
                }
            }
        }

    }
}
