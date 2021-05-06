using Gadgets.Enum;
using System;
using System.Collections.Generic;

namespace Gadgets
{
    /// <summary>
    /// 输入数据
    /// </summary>
    public class HolidayList
    {
        public string Year { get; set; }
        public List<HolidayDetails> HolidayDetails { get; set; }
    }
    public class HolidayDetails
    {
        public string Date { get; set; }
        public HolidayTypeEnum Type { get; set; }
        public string Target { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 工资倍数
        /// </summary>
        public int Wage { get; set; }
    }
    /// <summary>
    /// 输出数据
    /// </summary>
    public class HolidayDate
    {
        /// <summary>
        /// 0:正常; 1:出错
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string Date { get; set; }
        public HolidayType Type { get; set; }
        /// <summary>
        /// 节假日信息
        /// </summary>
        public HolidayInfo Holiday { get; set; }
    }
    public class HolidayType
    {
        /// <summary>
        /// 周几
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 节假日类型，分别表示 工作日、周末、节日、调休
        /// </summary>
        public HolidayTypeEnum Type { get; set; }
        /// <summary>
        /// 一周中的第几天。值为 1 - 7，分别表示 周一 至 周日
        /// </summary>
        public int Week { get; set; }
    }
    public class HolidayInfo
    {
        /// <summary>
        /// 是否节假日/休息
        /// </summary>
        public bool Holiday { get; set; }
        /// <summary>
        /// 节假日名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 调休目标
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 工资倍数
        /// </summary>
        public int Wage { get; set; }
    }
}
