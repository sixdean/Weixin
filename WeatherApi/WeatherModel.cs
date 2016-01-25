using System.Collections.Generic;

namespace WeatherApi
{
    public class WeatherModel
    {
        public class HeWeatherResult
        {
            public List<HeWeather> HeWeatherList { get; set; }
        }
        public class HeWeather
        {
            /// <summary>
            /// 接口状态
            /// </summary>
            public string status { get; set; }

            public aqi aqi { get; set; }
            public basic basic { get; set; }
            public List<daily_forecast> daily_forecast { get; set; }
            public List<hourly_forecast> hourly_forecast { get; set; }
            public now now { get; set; }
            public suggestion suggestion { get; set; }
        }
        public class daily_forecast
        {
            public string date { get; set; }
            public astro astro { get; set; }
            public conds cond { get; set; }
            public string hum { get; set; }
            public string pcpn { get; set; }
            public string pop { get; set; }
            public string pres { get; set; }
            public string vis { get; set; }
            public tmp tmp { get; set; }
            public wind wind { get; set; }

        }
        public class astro
        {
            public string sr { get; set; }
            public string ss { get; set; }
        }


        public class tmp
        {
            public string max { get; set; }
            public string min { get; set; }

        }
        public class hourly_forecast
        {
            public string date { get; set; }
            public string hum { get; set; }
            public string pop { get; set; }
            public string pres { get; set; }
            public string tmp { get; set; }
            public wind wind { get; set; }

        }
        public class conds
        {
            public string code_d { get; set; }
            public string code_n { get; set; }
            public string txt_d { get; set; }
            public string txt_n { get; set; }
        }

        /// <summary>
        ///  空气质量指数
        /// </summary>
        public class aqi
        {
            public city city { get; set; }
        }

        /// <summary>
        /// 城市数据
        /// </summary>
        public class city
        {
            /// <summary>
            /// 空气质量指数
            /// </summary>
            public string aqi { get; set; }
            /// <summary>
            /// 一氧化碳1小时平均值(ug/m³)
            /// </summary>
            public string co { get; set; }
            /// <summary>
            /// 二氧化氮1小时平均值(ug/m³)
            /// </summary>
            public string no2 { get; set; }
            /// <summary>
            /// 臭氧1小时平均值(ug/m³)
            /// </summary>
            public string o3 { get; set; }
            /// <summary>
            /// PM10 1小时平均值(ug/m³)
            /// </summary>
            public string pm10 { get; set; }
            /// <summary>
            /// PM2.5 1小时平均值(ug/m³)
            /// </summary>
            public string pm25 { get; set; }
            /// <summary>
            /// 空气质量类别
            /// </summary>
            public string qlty { get; set; }
            /// <summary>
            /// 二氧化硫1小时平均值(ug/m³)
            /// </summary>
            public string so2 { get; set; }

        }

        public class basic
        {
            /// <summary>
            /// 城市名称
            /// </summary>
            public string city { get; set; }
            /// <summary>
            /// 城市ID
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 国家名称
            /// </summary>
            public string cnty { get; set; }
            /// <summary>
            /// 纬度
            /// </summary>
            public string lat { get; set; }
            /// <summary>
            /// 经度
            /// </summary>
            public string lon { get; set; }
            /// <summary>
            /// 数据更新时间,24小时制
            /// </summary>
            public update update { get; set; }
        }
        public class update
        {
            /// <summary>
            /// 数据更新的当地时间
            /// </summary>
            public string loc { get; set; }
            /// <summary>
            /// 数据更新的UTC时间
            /// </summary>
            public string utc { get; set; }
        }

        /// <summary>
        /// 生活指数
        /// </summary>
        public class suggestion
        {
            /// <summary>
            /// 舒适度指数
            /// </summary>
            public content comf { get; set; }
            /// <summary>
            /// 洗车指数
            /// </summary>
            public content cw { get; set; }
            /// <summary>
            /// 穿衣指数
            /// </summary>
            public content drsg { get; set; }
            /// <summary>
            /// 感冒指数
            /// </summary>
            public content flu { get; set; }
            /// <summary>
            /// 运动指数
            /// </summary>
            public content sport { get; set; }
            /// <summary>
            /// 旅游指数
            /// </summary>
            public content trav { get; set; }
            /// <summary>
            /// 紫外线指数
            /// </summary>
            public content uv { get; set; }
        }
        public class content
        {
            /// <summary>
            /// 简介
            /// </summary>
            public string brf { get; set; }
            /// <summary>
            /// 详情
            /// </summary>
            public string txt { get; set; }

        }

        /// <summary>
        /// 实况天气
        /// </summary>
        public class now
        {
            /// <summary>
            /// 天气状况
            /// </summary>
            public cond cond { get; set; }

            /// <summary>
            /// 体感温度
            /// </summary>
            public string fl { get; set; }
            /// <summary>
            /// 相对湿度（%）
            /// </summary>
            public string hum { get; set; }
            /// <summary>
            /// 降水量（mm）
            /// </summary>
            public string pcpn { get; set; }
            /// <summary>
            /// 气压
            /// </summary>
            public string pres { get; set; }
            /// <summary>
            /// 温度
            /// </summary>
            public string tmp { get; set; }
            /// <summary>
            /// 能见度（km）
            /// </summary>
            /// </summary>
            public string vis { get; set; }
            /// <summary>
            /// 风力风向
            /// </summary>
            public wind wind { get; set; }
        }
        /// <summary>
        /// 天气状况
        /// </summary>
        public class cond
        {
            /// <summary>
            /// 天气状况代码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 天气状况描述
            /// </summary>
            public string txt { get; set; }

        }

        public class wind
        {
            /// <summary>
            /// 风向（360度）
            /// </summary>
            public string deg { get; set; }
            /// <summary>
            /// 风向
            /// </summary>
            public string dir { get; set; }
            /// <summary>
            /// 风力
            /// </summary>
            public string sc { get; set; }
            /// <summary>
            /// 风速（kmph）
            /// </summary>
            public string spd { get; set; }
        }
    }
}





