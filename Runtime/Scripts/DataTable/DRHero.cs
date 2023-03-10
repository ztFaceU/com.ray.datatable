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
    /// 故事英雄信息
    /// </summary>
    public class DRHero : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取传奇ID。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取传奇人物名称。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取人物简介。
        /// </summary>
        public string Introduction
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取职业名称。
        /// </summary>
        public string Job
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取职业大图标。
        /// </summary>
        public string JobTypeIcon
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取结算界面职业大图标。
        /// </summary>
        public string JobTypeResultIcon
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取职业小图标。
        /// </summary>
        public string JobIcon
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取杀人任务名称。
        /// </summary>
        public string KillMissionName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取模型预设。
        /// </summary>
        public string ModelPrefab
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取头像。
        /// </summary>
        public string AvatarUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取立绘。
        /// </summary>
        public string PortraitUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取立绘（带背景）。
        /// </summary>
        public string PortraitWithBgUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取速度。
        /// </summary>
        public int Speed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取体质。
        /// </summary>
        public int Physique
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取意志。
        /// </summary>
        public int Volition
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取体格。
        /// </summary>
        public int BodyBuild
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取力量。
        /// </summary>
        public int Power
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取异变知识。
        /// </summary>
        public int MutationKnowledge
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取基础生命值上限。
        /// </summary>
        public int MaxHp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取基础SAN上限。
        /// </summary>
        public int MaxSan
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取基础攻击力。
        /// </summary>
        public int Attack
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取基础饱腹度上限。
        /// </summary>
        public int MaxFullStomach
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取主要技能图标（红）。
        /// </summary>
        public string MainSkillIconRed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取主要技能图标（灰）。
        /// </summary>
        public string MainSkillIconGray
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否为主动技能。
        /// </summary>
        public int IsActiveSkill
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取主要技能名称。
        /// </summary>
        public string MainSkillName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取主要技能描述。
        /// </summary>
        public string MainSkillDesc
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取次要技能图标（红）。
        /// </summary>
        public string SecondarySkillIconRed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取次要技能图标（灰）。
        /// </summary>
        public string SecondarySkillIconGray
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取次要技能名称。
        /// </summary>
        public string SecondarySkillName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取次要技能描述。
        /// </summary>
        public string SecondarySkillDesc
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取初始物品。
        /// </summary>
        public string StartItems
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取角色语句。
        /// </summary>
        public string Comment
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取角色背景。
        /// </summary>
        public string Background
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
            Introduction = columnStrings[index++];
            Job = columnStrings[index++];
            JobTypeIcon = columnStrings[index++];
            JobTypeResultIcon = columnStrings[index++];
            JobIcon = columnStrings[index++];
            KillMissionName = columnStrings[index++];
            ModelPrefab = columnStrings[index++];
            AvatarUrl = columnStrings[index++];
            PortraitUrl = columnStrings[index++];
            PortraitWithBgUrl = columnStrings[index++];
            Speed = int.Parse(columnStrings[index++]);
            Physique = int.Parse(columnStrings[index++]);
            Volition = int.Parse(columnStrings[index++]);
            BodyBuild = int.Parse(columnStrings[index++]);
            Power = int.Parse(columnStrings[index++]);
            MutationKnowledge = int.Parse(columnStrings[index++]);
            MaxHp = int.Parse(columnStrings[index++]);
            MaxSan = int.Parse(columnStrings[index++]);
            Attack = int.Parse(columnStrings[index++]);
            MaxFullStomach = int.Parse(columnStrings[index++]);
            MainSkillIconRed = columnStrings[index++];
            MainSkillIconGray = columnStrings[index++];
            IsActiveSkill = int.Parse(columnStrings[index++]);
            MainSkillName = columnStrings[index++];
            MainSkillDesc = columnStrings[index++];
            SecondarySkillIconRed = columnStrings[index++];
            SecondarySkillIconGray = columnStrings[index++];
            SecondarySkillName = columnStrings[index++];
            SecondarySkillDesc = columnStrings[index++];
            StartItems = columnStrings[index++];
            Comment = columnStrings[index++];
            Background = columnStrings[index++];

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
                    Introduction = binaryReader.ReadString();
                    Job = binaryReader.ReadString();
                    JobTypeIcon = binaryReader.ReadString();
                    JobTypeResultIcon = binaryReader.ReadString();
                    JobIcon = binaryReader.ReadString();
                    KillMissionName = binaryReader.ReadString();
                    ModelPrefab = binaryReader.ReadString();
                    AvatarUrl = binaryReader.ReadString();
                    PortraitUrl = binaryReader.ReadString();
                    PortraitWithBgUrl = binaryReader.ReadString();
                    Speed = binaryReader.Read7BitEncodedInt32();
                    Physique = binaryReader.Read7BitEncodedInt32();
                    Volition = binaryReader.Read7BitEncodedInt32();
                    BodyBuild = binaryReader.Read7BitEncodedInt32();
                    Power = binaryReader.Read7BitEncodedInt32();
                    MutationKnowledge = binaryReader.Read7BitEncodedInt32();
                    MaxHp = binaryReader.Read7BitEncodedInt32();
                    MaxSan = binaryReader.Read7BitEncodedInt32();
                    Attack = binaryReader.Read7BitEncodedInt32();
                    MaxFullStomach = binaryReader.Read7BitEncodedInt32();
                    MainSkillIconRed = binaryReader.ReadString();
                    MainSkillIconGray = binaryReader.ReadString();
                    IsActiveSkill = binaryReader.Read7BitEncodedInt32();
                    MainSkillName = binaryReader.ReadString();
                    MainSkillDesc = binaryReader.ReadString();
                    SecondarySkillIconRed = binaryReader.ReadString();
                    SecondarySkillIconGray = binaryReader.ReadString();
                    SecondarySkillName = binaryReader.ReadString();
                    SecondarySkillDesc = binaryReader.ReadString();
                    StartItems = binaryReader.ReadString();
                    Comment = binaryReader.ReadString();
                    Background = binaryReader.ReadString();
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
