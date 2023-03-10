//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：
//------------------------------------------------------------

using System.IO;
using System.Text;
using TheMist.DataTable;
using System.Collections.Generic;

namespace TheMist
{
    /// <summary>
    /// 道具表
    /// </summary>
    public class DRProps : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取ID。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取名称。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取英文名。
        /// </summary>
        public string NameEN
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取分类描述。
        /// </summary>
        public string TypeDesc
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取使用类型。
        /// </summary>
        public int UseType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取道具等级。
        /// </summary>
        public int Level
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取叠堆数量。
        /// </summary>
        public int Overlay
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取背包最大数量。
        /// </summary>
        public int MaxCount
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取能否使用 0：否 1：能。
        /// </summary>
        public int CanUse
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取逻辑脚本ID。
        /// </summary>
        public string ScriptId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取道具逻辑。
        /// </summary>
        public string Logic
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取道具使用过程需要持续。
        /// </summary>
        public int IsUsePersist
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取道具使用过程持续ID。
        /// </summary>
        public int UsePersistID
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取图标。
        /// </summary>
        public string Icon
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取音效。
        /// </summary>
        public string Audio
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取道具来源。
        /// </summary>
        public string Source
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取道具描述。
        /// </summary>
        public string Desc
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            m_Id = int.Parse(columnStrings[index++]);
            Name = columnStrings[index++];
            NameEN = columnStrings[index++];
            TypeDesc = columnStrings[index++];
            UseType = int.Parse(columnStrings[index++]);
            Level = int.Parse(columnStrings[index++]);
            Overlay = int.Parse(columnStrings[index++]);
            MaxCount = int.Parse(columnStrings[index++]);
            CanUse = int.Parse(columnStrings[index++]);
            ScriptId = columnStrings[index++];
            Logic = columnStrings[index++];
            IsUsePersist = int.Parse(columnStrings[index++]);
            UsePersistID = int.Parse(columnStrings[index++]);
            Icon = columnStrings[index++];
            Audio = columnStrings[index++];
            Source = columnStrings[index++];
            Desc = columnStrings[index++];

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Name = binaryReader.ReadString();
                    NameEN = binaryReader.ReadString();
                    TypeDesc = binaryReader.ReadString();
                    UseType = binaryReader.Read7BitEncodedInt32();
                    Level = binaryReader.Read7BitEncodedInt32();
                    Overlay = binaryReader.Read7BitEncodedInt32();
                    MaxCount = binaryReader.Read7BitEncodedInt32();
                    CanUse = binaryReader.Read7BitEncodedInt32();
                    ScriptId = binaryReader.ReadString();
                    Logic = binaryReader.ReadString();
                    IsUsePersist = binaryReader.Read7BitEncodedInt32();
                    UsePersistID = binaryReader.Read7BitEncodedInt32();
                    Icon = binaryReader.ReadString();
                    Audio = binaryReader.ReadString();
                    Source = binaryReader.ReadString();
                    Desc = binaryReader.ReadString();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
