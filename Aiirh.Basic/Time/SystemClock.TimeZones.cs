using System;
using System.Collections.Generic;

namespace Aiirh.Basic.Time
{
    [Obsolete("Use Aiirh.DateAndTime package")]
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
        UTC = 084,
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

    [Obsolete("Use Aiirh.DateAndTime package")]
    internal static class TimeZoneMapping
    {
        [Obsolete("Use Aiirh.DateAndTime package")]
        internal static Dictionary<TimeZones, string> TimeZoneToName => new Dictionary<TimeZones, string> {
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
            { TimeZones.UTC, "UTC" },
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
    }
}
