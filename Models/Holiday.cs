using Gadgets.Enum;
using System;
using System.Collections.Generic;

namespace Gadgets
{
    /// <summary>
    /// ��������
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
        /// ���ʱ���
        /// </summary>
        public int Wage { get; set; }
    }
    /// <summary>
    /// �������
    /// </summary>
    public class HolidayDate
    {
        /// <summary>
        /// 0:����; 1:����
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// ʱ��
        /// </summary>
        public string Date { get; set; }
        public HolidayType Type { get; set; }
        /// <summary>
        /// �ڼ�����Ϣ
        /// </summary>
        public HolidayInfo Holiday { get; set; }
    }
    public class HolidayType
    {
        /// <summary>
        /// �ܼ�
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// �ڼ������ͣ��ֱ��ʾ �����ա���ĩ�����ա�����
        /// </summary>
        public HolidayTypeEnum Type { get; set; }
        /// <summary>
        /// һ���еĵڼ��졣ֵΪ 1 - 7���ֱ��ʾ ��һ �� ����
        /// </summary>
        public int Week { get; set; }
    }
    public class HolidayInfo
    {
        /// <summary>
        /// �Ƿ�ڼ���/��Ϣ
        /// </summary>
        public bool Holiday { get; set; }
        /// <summary>
        /// �ڼ�������
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ����Ŀ��
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// ���ʱ���
        /// </summary>
        public int Wage { get; set; }
    }
}
