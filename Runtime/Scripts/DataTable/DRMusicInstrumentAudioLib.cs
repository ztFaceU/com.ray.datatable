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
    /// 乐器曲库
    /// </summary>
    public class DRMusicInstrumentAudioLib : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取唯一Id。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取关卡ID。
        /// </summary>
        public int LevelId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取曲子ID。
        /// </summary>
        public int MusicId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取音轨序号Id。
        /// </summary>
        public int AudioTrackId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取下一首曲子Id。
        /// </summary>
        public int NextMusicId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取音乐资源名。
        /// </summary>
        public string AudioName
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
            LevelId = int.Parse(columnStrings[index++]);
            MusicId = int.Parse(columnStrings[index++]);
            AudioTrackId = int.Parse(columnStrings[index++]);
            NextMusicId = int.Parse(columnStrings[index++]);
            AudioName = columnStrings[index++];

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
                    LevelId = binaryReader.Read7BitEncodedInt32();
                    MusicId = binaryReader.Read7BitEncodedInt32();
                    AudioTrackId = binaryReader.Read7BitEncodedInt32();
                    NextMusicId = binaryReader.Read7BitEncodedInt32();
                    AudioName = binaryReader.ReadString();
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
