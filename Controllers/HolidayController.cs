using Gadgets.Enum;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gadgets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        #region 是否休息
        //[HttpGet]
        /// <summary>
        /// 大小周-指定日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("info/single/{date}")]
        public JsonResult Single(DateTime? date)
        {
            HolidayDate holiday = new HolidayDate();
            holiday = GetHolidayInfo(date, true);
            return new JsonResult(holiday);
        }

        /// <summary>
        /// 大小周-当日
        /// </summary>
        /// <returns></returns>
        [HttpGet("info/single")]
        public JsonResult SingleToday()
        {
            HolidayDate holiday = new HolidayDate();
            holiday = GetHolidayInfo(DateTime.Now, true);
            return new JsonResult(holiday);
        }

        /// <summary>
        /// 大小周-当日-节后单休
        /// </summary>
        /// <returns></returns>
        [HttpGet("info/singlee")]
        public JsonResult SingleeToday()
        {
            HolidayDate holiday = new HolidayDate();
            holiday = GetHolidayInfo(DateTime.Now, true, true);
            return new JsonResult(holiday);
        }

        /// <summary>
        /// 大小周-次日
        /// </summary>
        /// <returns></returns>
        [HttpGet("info/single/next")]
        public JsonResult SingleNext()
        {
            HolidayDate holiday = new HolidayDate();
            holiday = GetHolidayInfo(DateTime.Now.AddDays(1), true);
            return new JsonResult(holiday);
        }

        /// <summary>
        /// 大小周-次日-节后单休
        /// </summary>
        /// <returns></returns>
        [HttpGet("info/singlee/next")]
        public JsonResult SingleeNext()
        {
            HolidayDate holiday = new HolidayDate();
            holiday = GetHolidayInfo(DateTime.Now.AddDays(1), true, true);
            return new JsonResult(holiday);
        }

        /// <summary>
        /// 双休-指定日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("info/{date}")]
        public JsonResult HolidayInfo(DateTime? date)
        {
            HolidayDate holiday = new HolidayDate();
            holiday = GetHolidayInfo(date);
            return new JsonResult(holiday);
        }

        /// <summary>
        /// 双休-当日
        /// </summary>
        /// <returns></returns>
        [HttpGet("info")]
        public JsonResult HolidayInfoToday()
        {
            HolidayDate holiday = new HolidayDate();
            holiday = GetHolidayInfo(DateTime.Now);
            return new JsonResult(holiday);
        }

        /// <summary>
        /// 双休-次日
        /// </summary>
        /// <returns></returns>
        [HttpGet("info/next")]
        public JsonResult HolidayInfoNext()
        {
            HolidayDate holiday = new HolidayDate();
            holiday = GetHolidayInfo(DateTime.Now.AddDays(1));
            return new JsonResult(holiday);
        }

        /// <summary>
        /// 计算是否休息
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="isSingle">是否大小周</param>
        /// <param name="holiSing">是否节后单休</param>
        /// <returns></returns>
        public HolidayDate GetHolidayInfo(DateTime? date, bool isSingle = false, bool holiSing = false)
        {
            string jsonfile = "Scripts/holiday.json";//JSON文件路径
            StreamReader streamReader = new StreamReader(jsonfile, Encoding.Default); //读取文件
            string json = streamReader.ReadToEnd();//读取文件内容
            List<HolidayList> list = JsonConvert.DeserializeObject<List<HolidayList>>(json.ToString());//反序列化json,获取节假日List

            if (date == null) date = DateTime.Now;//日期为空则取当日
            DateTime day = date.Value;//可空类型转换DateTime
            var detail = list.Where(t => t.Year == day.ToString("yyyy")).FirstOrDefault().HolidayDetails;//获取指定年份的节假日列表

            HolidayDate holiday = new HolidayDate();
            holiday.Date = day.ToString("yyyy-MM-dd");
            var allholi = detail.Where(t => t.Date == day.ToString("MMdd")).FirstOrDefault(); //获取指定日期的节假日信息

            HolidayType h_type = new HolidayType();
            h_type.Week = (int)day.DayOfWeek == 0 ? 7 : (int)day.DayOfWeek;
            h_type.Name = ((WeekCNEnum)h_type.Week).ToString();
            h_type.Type = h_type.Week > 5 ? HolidayTypeEnum.周末 : HolidayTypeEnum.工作日;

            //单数单休，双数双休
            if (h_type.Week == 6 && (day.Day % 2 != 0) && isSingle)
            {
                h_type.Type = HolidayTypeEnum.工作日;
                h_type.Name += "单休";
            }

            //节后单休
            if (h_type.Week == 6 && isSingle && holiSing)
            {
                var newDay = day;
                for (int i = 0; i < 7; i++)
                {
                    newDay = newDay.AddDays(-1);
                    var history = detail.Where(t => t.Date == newDay.ToString("MMdd")).FirstOrDefault();
                    if (history != null)
                    {
                        h_type.Type = HolidayTypeEnum.工作日;
                        h_type.Name += "节后单休";
                        break;
                    }
                }
            }

            if (allholi != null)
            {
                HolidayInfo h_info = new HolidayInfo();
                h_info.Holiday = allholi.Type == HolidayTypeEnum.节日 ? true : false;
                h_info.Name = allholi.Name;
                h_info.Target = allholi.Target;
                h_info.Wage = allholi.Wage;
                holiday.Holiday = h_info;

                h_type.Name = h_info.Name;
                h_type.Type = h_info.Holiday ? HolidayTypeEnum.节日 : HolidayTypeEnum.调休;
            }
            holiday.Type = h_type;
            return holiday;
        }
        #endregion

    }
}
