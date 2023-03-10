﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheMist.DataTable
{
    public abstract class DataRow
    {
        public abstract int Id
        {
            get;
        }

        /// <summary>
        /// 解析数据表行。
        /// </summary>
        /// <param name="dataRowString">要解析的数据表行字符串。</param>
        /// <returns>是否解析数据表行成功。</returns>
        public virtual bool ParseDataRow(string dataRowString)
        {
            Debug.LogWarning("Not implemented ParseDataRow(string dataRowString, object userData).");
            return false;
        }

        /// <summary>
        /// 解析数据表行。
        /// </summary>
        /// <param name="dataRowBytes">要解析的数据表行二进制流。</param>
        /// <param name="startIndex">数据表行二进制流的起始位置。</param>
        /// <param name="length">数据表行二进制流的长度。</param>
        /// <returns>是否解析数据表行成功。</returns>
        public virtual bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length)
        {
            Debug.LogWarning("Not implemented ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData).");
            return false;
        }
    }
}
