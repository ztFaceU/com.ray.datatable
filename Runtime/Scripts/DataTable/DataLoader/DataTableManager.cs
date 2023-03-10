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
    public static class DataTableManager
    {
        public const string DATA_TYPE_NAMESPACE = "TheMist";
        public const string DATA_TYPE_PREFIX = "DR";
        /*  key == fileName == TypeName =- DR  */
        public static Dictionary<string, IDataTable> allDataTables;

        public static async UniTask LoadDataAssets()
        {
            await DataTableLoader.LoadDataAssets();
            Debug.Log("ManagerLoad");
            allDataTables = new Dictionary<string, IDataTable>();
            foreach (var dataBytes in DataTableLoader.allDataTableBytes)
            {
                string typeStr = string.Format("{0}.DR{1}", DATA_TYPE_NAMESPACE, dataBytes.Key);
                Type dataRowType = Type.GetType(typeStr);
                CreateDataTable(dataRowType, dataBytes.Key, dataBytes.Value);
            }

        }

        private static void CreateDataTable(Type dataRowType ,string name,byte[] bytes)
        {
            var dataTable =(IDataTable) Activator.CreateInstance(typeof(DataTable<>).MakeGenericType(dataRowType));
            dataTable.LoadBytes(bytes);
            allDataTables.Add(name, dataTable);
        }

        public static DataTable<T> GetDataTable<T>(string name) where T:DataRow
        {
            if(allDataTables.TryGetValue(name, out var table))
            {
                return table as DataTable<T>;
            }
            return null;
        }
    }
}
