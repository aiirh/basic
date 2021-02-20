using System;
using System.Collections.Generic;

namespace Aiirh.Basic.Entities.Time
{
    public enum TimeZones
    {
        DatelineStandardTime = 000, // (GMT-12:00) International Date Line West
        SamoaStandardTime = 001, // (GMT-11:00) Midway Island, Samoa
        HawaiianStandardTime = 002, // (GMT-10:00) Hawaii
        AlaskanStandardTime = 003, // (GMT-09:00) Alaska
        PacificStandardTime = 004, // (GMT-08:00) Pacific Time (US and Canada); Tijuana
        MountainStandardTime = 010, // (GMT-07:00) Mountain Time (US and Canada)
        MexicoStandardTime2 = 013, // (GMT-07:00) Chihuahua, La Paz, Mazatlan
        USMountainStandardTime = 015, // (GMT-07:00) Arizona
        CentralStandardTime = 020, // (GMT-06:00) Central Time (US and Canada)
        CanadaCentralStandardTime = 025, // (GMT-06:00) Saskatchewan
        MexicoStandardTime = 030, // (GMT-06:00) Guadalajara, Mexico City, Monterrey
        CentralAmericaStandardTime = 033, // (GMT-06:00) Central America
        EasternStandardTime = 035, // (GMT-05:00) Eastern Time (US and Canada)
        USEasternStandardTime = 040, // (GMT-05:00) Indiana (East)
        SAPacificStandardTime = 045, // (GMT-05:00) Bogota, Lima, Quito
        AtlanticStandardTime = 050, // (GMT-04:00) Atlantic Time (Canada)
        SAWesternStandardTime = 055, // (GMT-04:00) Caracas, La Paz
        PacificSAStandardTime = 056, // (GMT-04:00) Santiago
        NewfoundlandandLabradorStandardTime = 060, // (GMT-03:30) Newfoundland and Labrador
        ESouthAmericaStandardTime = 065, // (GMT-03:00) Brasilia
        SAEasternStandardTime = 070, // (GMT-03:00) Buenos Aires, Georgetown
        GreenlandStandardTime = 073, // (GMT-03:00) Greenland
        MidAtlanticStandardTime = 075, // (GMT-02:00) Mid-Atlantic
        AzoresStandardTime = 080, // (GMT-01:00) Azores
        CapeVerdeStandardTime = 083, // (GMT-01:00) Cape Verde Islands
        GMTStandardTime = 085, // (GMT) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London
        GreenwichStandardTime = 090, // (GMT) Casablanca, Monrovia
        CentralEuropeStandardTime = 095, // (GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague
        CentralEuropeanStandardTime = 100, // (GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb
        RomanceStandardTime = 105, // (GMT+01:00) Brussels, Copenhagen, Madrid, Paris
        WEuropeStandardTime = 110, // (GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna
        WCentralAfricaStandardTime = 113, // (GMT+01:00) West Central Africa
        EEuropeStandardTime = 115, // (GMT+02:00) Bucharest
        EgyptStandardTime = 120, // (GMT+02:00) Cairo
        FLEStandardTime = 125, // (GMT+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius
        GTBStandardTime = 130, // (GMT+02:00) Athens, Istanbul, Minsk
        IsraelStandardTime = 135, // (GMT+02:00) Jerusalem
        SouthAfricaStandardTime = 140, // (GMT+02:00) Harare, Pretoria
        RussianStandardTime = 145, // (GMT+03:00) Moscow, St. Petersburg, Volgograd
        ArabStandardTime = 150, // (GMT+03:00) Kuwait, Riyadh
        EAfricaStandardTime = 155, // (GMT+03:00) Nairobi
        ArabicStandardTime = 158, // (GMT+03:00) Baghdad
        IranStandardTime = 160, // (GMT+03:30) Tehran
        ArabianStandardTime = 165, // (GMT+04:00) Abu Dhabi, Muscat
        CaucasusStandardTime = 170, // (GMT+04:00) Baku, Tbilisi, Yerevan
        TransitionalIslamicStateofAfghanistanStandardTime = 175, // (GMT+04:30) Kabul
        EkaterinburgStandardTime = 180, // (GMT+05:00) Ekaterinburg
        WestAsiaStandardTime = 185, // (GMT+05:00) Islamabad, Karachi, Tashkent
        IndiaStandardTime = 190, // (GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi
        NepalStandardTime = 193, // (GMT+05:45) Kathmandu
        CentralAsiaStandardTime = 195, // (GMT+06:00) Astana, Dhaka
        SriLankaStandardTime = 200, // (GMT+06:00) Sri Jayawardenepura
        NCentralAsiaStandardTime = 201, // (GMT+06:00) Almaty, Novosibirsk
        MyanmarStandardTime = 203, // (GMT+06:30) Yangon (Rangoon)
        SEAsiaStandardTime = 205, // (GMT+07:00) Bangkok, Hanoi, Jakarta
        NorthAsiaStandardTime = 207, // (GMT+07:00) Krasnoyarsk
        ChinaStandardTime = 210, // (GMT+08:00) Beijing, Chongqing, Hong Kong SAR, Urumqi
        SingaporeStandardTime = 215, // (GMT+08:00) Kuala Lumpur, Singapore
        TaipeiStandardTime = 220, // (GMT+08:00) Taipei
        WAustraliaStandardTime = 225, // (GMT+08:00) Perth
        NorthAsiaEastStandardTime = 227, // (GMT+08:00) Irkutsk, Ulaan Bataar
        KoreaStandardTime = 230, // (GMT+09:00) Seoul
        TokyoStandardTime = 235, // (GMT+09:00) Osaka, Sapporo, Tokyo
        YakutskStandardTime = 240, // (GMT+09:00) Yakutsk
        AUSCentralStandardTime = 245, // (GMT+09:30) Darwin
        CenAustraliaStandardTime = 250, // (GMT+09:30) Adelaide
        AUSEasternStandardTime = 255, // (GMT+10:00) Canberra, Melbourne, Sydney
        EAustraliaStandardTime = 260, // (GMT+10:00) Brisbane
        TasmaniaStandardTime = 265, // (GMT+10:00) Hobart
        VladivostokStandardTime = 270, // (GMT+10:00) Vladivostok
        WestPacificStandardTime = 275, // (GMT+10:00) Guam, Port Moresby
        CentralPacificStandardTime = 280, // (GMT+11:00) Magadan, Solomon Islands, New Caledonia
        FijiIslandsStandardTime = 285, // (GMT+12:00) Fiji Islands, Kamchatka, Marshall Islands
        NewZealandStandardTime = 290, // (GMT+12:00) Auckland, Wellington
        TongaStandardTime = 300 // (GMT+13:00) Nuku'alofa
    }

    public static partial class SystemClock
    {
        private static Dictionary<TimeZones, string> _timeZoneMapping = new Dictionary<TimeZones, string> {
            { TimeZones.DatelineStandardTime, "Dateline Standard Time" },
            { TimeZones.SamoaStandardTime, "Samoa Standard Time" },
            { TimeZones.HawaiianStandardTime, "Hawaiian Standard Time" },
            { TimeZones.AlaskanStandardTime, "Alaskan Standard Time" },
            { TimeZones.PacificStandardTime, "Pacific Standard Time" },
            { TimeZones.MountainStandardTime, "Mountain Standard Time" },
            { TimeZones.MexicoStandardTime2, "Mexico Standard Time 2" },
            { TimeZones.USMountainStandardTime, "U.S. Mountain Standard Time" },
            { TimeZones.CentralStandardTime, "Central Standard Time" },
            { TimeZones.CanadaCentralStandardTime, "Canada Central Standard Time" },
            { TimeZones.MexicoStandardTime, "Mexico Standard Time" },
            { TimeZones.CentralAmericaStandardTime, "Central America Standard Time" },
            { TimeZones.EasternStandardTime, "Eastern Standard Time" },
            { TimeZones.USEasternStandardTime, "U.S. Eastern Standard Time" },
            { TimeZones.SAPacificStandardTime, "S.A. Pacific Standard Time" },
            { TimeZones.AtlanticStandardTime, "Atlantic Standard Time" },
            { TimeZones.SAWesternStandardTime, "S.A. Western Standard Time" },
            { TimeZones.PacificSAStandardTime, "Pacific S.A. Standard Time" },
            { TimeZones.NewfoundlandandLabradorStandardTime, "Newfoundland and Labrador Standard Time" },
            { TimeZones.ESouthAmericaStandardTime, "E. South America Standard Time" },
            { TimeZones.SAEasternStandardTime, "S.A. Eastern Standard Time" },
            { TimeZones.GreenlandStandardTime, "Greenland Standard Time" },
            { TimeZones.MidAtlanticStandardTime, "Mid-Atlantic Standard Time" },
            { TimeZones.AzoresStandardTime, "Azores Standard Time" },
            { TimeZones.CapeVerdeStandardTime, "Cape Verde Standard Time" },
            { TimeZones.GMTStandardTime, "GMT Standard Time" },
            { TimeZones.GreenwichStandardTime, "Greenwich Standard Time" },
            { TimeZones.CentralEuropeStandardTime, "Central Europe Standard Time" },
            { TimeZones.CentralEuropeanStandardTime, "Central European Standard Time" },
            { TimeZones.RomanceStandardTime, "Romance Standard Time" },
            { TimeZones.WEuropeStandardTime, "W. Europe Standard Time" },
            { TimeZones.WCentralAfricaStandardTime, "W. Central Africa Standard Time" },
            { TimeZones.EEuropeStandardTime, "E. Europe Standard Time" },
            { TimeZones.EgyptStandardTime, "Egypt Standard Time" },
            { TimeZones.FLEStandardTime, "FLE Standard Time" },
            { TimeZones.GTBStandardTime, "GTB Standard Time" },
            { TimeZones.IsraelStandardTime, "Israel Standard Time" },
            { TimeZones.SouthAfricaStandardTime, "South Africa Standard Time" },
            { TimeZones.RussianStandardTime, "Russian Standard Time" },
            { TimeZones.ArabStandardTime, "Arab Standard Time" },
            { TimeZones.EAfricaStandardTime, "E. Africa Standard Time" },
            { TimeZones.ArabicStandardTime, "Arabic Standard Time" },
            { TimeZones.IranStandardTime, "Iran Standard Time" },
            { TimeZones.ArabianStandardTime, "Arabian Standard Time" },
            { TimeZones.CaucasusStandardTime, "Caucasus Standard Time" },
            { TimeZones.TransitionalIslamicStateofAfghanistanStandardTime, "Transitional Islamic State of Afghanistan Standard Time" },
            { TimeZones.EkaterinburgStandardTime, "Ekaterinburg Standard Time" },
            { TimeZones.WestAsiaStandardTime, "West Asia Standard Time" },
            { TimeZones.IndiaStandardTime, "India Standard Time" },
            { TimeZones.NepalStandardTime, "Nepal Standard Time" },
            { TimeZones.CentralAsiaStandardTime, "Central Asia Standard Time" },
            { TimeZones.SriLankaStandardTime, "Sri Lanka Standard Time" },
            { TimeZones.NCentralAsiaStandardTime, "N. Central Asia Standard Time" },
            { TimeZones.MyanmarStandardTime, "Myanmar Standard Time" },
            { TimeZones.SEAsiaStandardTime, "S.E. Asia Standard Time" },
            { TimeZones.NorthAsiaStandardTime, "North Asia Standard Time" },
            { TimeZones.ChinaStandardTime, "China Standard Time" },
            { TimeZones.SingaporeStandardTime, "Singapore Standard Time" },
            { TimeZones.TaipeiStandardTime, "Taipei Standard Time" },
            { TimeZones.WAustraliaStandardTime, "W. Australia Standard Time" },
            { TimeZones.NorthAsiaEastStandardTime, "North Asia East Standard Time" },
            { TimeZones.KoreaStandardTime, "Korea Standard Time" },
            { TimeZones.TokyoStandardTime, "Tokyo Standard Time" },
            { TimeZones.YakutskStandardTime, "Yakutsk Standard Time" },
            { TimeZones.AUSCentralStandardTime, "A.U.S. Central Standard Time" },
            { TimeZones.CenAustraliaStandardTime, "Cen. Australia Standard Time" },
            { TimeZones.AUSEasternStandardTime, "A.U.S. Eastern Standard Time" },
            { TimeZones.EAustraliaStandardTime, "E. Australia Standard Time" },
            { TimeZones.TasmaniaStandardTime, "Tasmania Standard Time" },
            { TimeZones.VladivostokStandardTime, "Vladivostok Standard Time" },
            { TimeZones.WestPacificStandardTime, "West Pacific Standard Time" },
            { TimeZones.CentralPacificStandardTime, "Central Pacific Standard Time" },
            { TimeZones.FijiIslandsStandardTime, "Fiji Islands Standard Time" },
            { TimeZones.NewZealandStandardTime, "New Zealand Standard Time" },
            { TimeZones.TongaStandardTime, "Tonga Standard Time" }
        };

        private static Dictionary<TimeZones, TimeSpan> _timeZoneMappingTimeSpans = new Dictionary<TimeZones, TimeSpan> {
            { TimeZones.DatelineStandardTime, new TimeSpan(-12, 0, 0) },
            { TimeZones.SamoaStandardTime, new TimeSpan(-11, 0, 0) },
            { TimeZones.HawaiianStandardTime, new TimeSpan(-10, 0, 0) },
            { TimeZones.AlaskanStandardTime, new TimeSpan(-9, 0, 0) },
            { TimeZones.PacificStandardTime, new TimeSpan(-8, 0, 0) },
            { TimeZones.MountainStandardTime, new TimeSpan(-7, 0, 0) },
            { TimeZones.MexicoStandardTime2, new TimeSpan(-7, 0, 0) },
            { TimeZones.USMountainStandardTime, new TimeSpan(-7, 0, 0) },
            { TimeZones.CentralStandardTime, new TimeSpan(-6, 0, 0) },
            { TimeZones.CanadaCentralStandardTime, new TimeSpan(-6, 0, 0) },
            { TimeZones.MexicoStandardTime, new TimeSpan(-6, 0, 0) },
            { TimeZones.CentralAmericaStandardTime, new TimeSpan(-6, 0, 0) },
            { TimeZones.EasternStandardTime, new TimeSpan(-5, 0, 0) },
            { TimeZones.USEasternStandardTime, new TimeSpan(-5, 0, 0) },
            { TimeZones.SAPacificStandardTime, new TimeSpan(-5, 0, 0) },
            { TimeZones.AtlanticStandardTime, new TimeSpan(-4, 0, 0) },
            { TimeZones.SAWesternStandardTime, new TimeSpan(-4, 0, 0) },
            { TimeZones.PacificSAStandardTime, new TimeSpan(-4, 0, 0) },
            { TimeZones.NewfoundlandandLabradorStandardTime, new TimeSpan(0, 0, 0) },
            { TimeZones.ESouthAmericaStandardTime, new TimeSpan(-3, 0, 0) },
            { TimeZones.SAEasternStandardTime, new TimeSpan(-3, 0, 0) },
            { TimeZones.GreenlandStandardTime, new TimeSpan(-3, 0, 0) },
            { TimeZones.MidAtlanticStandardTime, new TimeSpan(-2, 0, 0) },
            { TimeZones.AzoresStandardTime, new TimeSpan(-1, 0, 0) },
            { TimeZones.CapeVerdeStandardTime, new TimeSpan(-1, 0, 0) },
            { TimeZones.GMTStandardTime, new TimeSpan(0, 0, 0) },
            { TimeZones.GreenwichStandardTime, new TimeSpan(0, 0, 0) },
            { TimeZones.CentralEuropeStandardTime, new TimeSpan(1, 0, 0) },
            { TimeZones.CentralEuropeanStandardTime, new TimeSpan(1, 0, 0) },
            { TimeZones.RomanceStandardTime, new TimeSpan(1, 0, 0) },
            { TimeZones.WEuropeStandardTime, new TimeSpan(1, 0, 0) },
            { TimeZones.WCentralAfricaStandardTime, new TimeSpan(1, 0, 0) },
            { TimeZones.EEuropeStandardTime, new TimeSpan(2, 0, 0) },
            { TimeZones.EgyptStandardTime, new TimeSpan(2, 0, 0) },
            { TimeZones.FLEStandardTime, new TimeSpan(2, 0, 0) },
            { TimeZones.GTBStandardTime, new TimeSpan(2, 0, 0) },
            { TimeZones.IsraelStandardTime, new TimeSpan(2, 0, 0) },
            { TimeZones.SouthAfricaStandardTime, new TimeSpan(2, 0, 0) },
            { TimeZones.RussianStandardTime, new TimeSpan(3, 0, 0) },
            { TimeZones.ArabStandardTime, new TimeSpan(3, 0, 0) },
            { TimeZones.EAfricaStandardTime, new TimeSpan(3, 0, 0) },
            { TimeZones.ArabicStandardTime, new TimeSpan(3, 0, 0) },
            { TimeZones.IranStandardTime, new TimeSpan(3, 30, 0) },
            { TimeZones.ArabianStandardTime, new TimeSpan(4, 0, 0) },
            { TimeZones.CaucasusStandardTime, new TimeSpan(4, 0, 0) },
            { TimeZones.TransitionalIslamicStateofAfghanistanStandardTime, new TimeSpan(4, 30, 0) },
            { TimeZones.EkaterinburgStandardTime, new TimeSpan(5, 0, 0) },
            { TimeZones.WestAsiaStandardTime, new TimeSpan(5, 0, 0) },
            { TimeZones.IndiaStandardTime, new TimeSpan(5, 30, 0) },
            { TimeZones.NepalStandardTime, new TimeSpan(5, 45, 0) },
            { TimeZones.CentralAsiaStandardTime, new TimeSpan(6, 0, 0) },
            { TimeZones.SriLankaStandardTime, new TimeSpan(6, 0, 0) },
            { TimeZones.NCentralAsiaStandardTime, new TimeSpan(6, 0, 0) },
            { TimeZones.MyanmarStandardTime, new TimeSpan(6, 30, 0) },
            { TimeZones.SEAsiaStandardTime, new TimeSpan(7, 0, 0) },
            { TimeZones.NorthAsiaStandardTime, new TimeSpan(7, 0, 0) },
            { TimeZones.ChinaStandardTime, new TimeSpan(8, 0, 0) },
            { TimeZones.SingaporeStandardTime, new TimeSpan(8, 0, 0) },
            { TimeZones.TaipeiStandardTime, new TimeSpan(8, 0, 0) },
            { TimeZones.WAustraliaStandardTime, new TimeSpan(8, 0, 0) },
            { TimeZones.NorthAsiaEastStandardTime, new TimeSpan(8, 0, 0) },
            { TimeZones.KoreaStandardTime, new TimeSpan(9, 0, 0) },
            { TimeZones.TokyoStandardTime, new TimeSpan(9, 0, 0) },
            { TimeZones.YakutskStandardTime, new TimeSpan(9, 0, 0) },
            { TimeZones.AUSCentralStandardTime, new TimeSpan(9, 30, 0) },
            { TimeZones.CenAustraliaStandardTime, new TimeSpan(9, 30, 0) },
            { TimeZones.AUSEasternStandardTime, new TimeSpan(10, 0, 0) },
            { TimeZones.EAustraliaStandardTime, new TimeSpan(10, 0, 0) },
            { TimeZones.TasmaniaStandardTime, new TimeSpan(10, 0, 0) },
            { TimeZones.VladivostokStandardTime, new TimeSpan(10, 0, 0) },
            { TimeZones.WestPacificStandardTime, new TimeSpan(10, 0, 0) },
            { TimeZones.CentralPacificStandardTime, new TimeSpan(11, 0, 0) },
            { TimeZones.FijiIslandsStandardTime, new TimeSpan(12, 0, 0) },
            { TimeZones.NewZealandStandardTime, new TimeSpan(12, 0, 0) },
            { TimeZones.TongaStandardTime, new TimeSpan(13, 0, 0) }
        };
    }
}
