﻿using System.Collections.Generic;

namespace Aiirh.DateAndTime
{
    internal static class TimeZoneMapping
    {
        internal static Dictionary<IanaTimeZone, string> TimeZoneToIanaName => new Dictionary<IanaTimeZone, string>
        {
            { IanaTimeZone.AustraliaDarwin, "Australia/Darwin" },
            { IanaTimeZone.AustraliaSydney, "Australia/Sydney" },
            { IanaTimeZone.AustraliaMelbourne, "Australia/Melbourne" },
            { IanaTimeZone.AsiaKabul, "Asia/Kabul" },
            { IanaTimeZone.AmericaAnchorage, "America/Anchorage" },
            { IanaTimeZone.AmericaJuneau, "America/Juneau" },
            { IanaTimeZone.AmericaMetlakatla, "America/Metlakatla" },
            { IanaTimeZone.AmericaNome, "America/Nome" },
            { IanaTimeZone.AmericaSitka, "America/Sitka" },
            { IanaTimeZone.AmericaYakutat, "America/Yakutat" },
            { IanaTimeZone.AmericaAdak, "America/Adak" },
            { IanaTimeZone.AsiaBarnaul, "Asia/Barnaul" },
            { IanaTimeZone.AsiaRiyadh, "Asia/Riyadh" },
            { IanaTimeZone.AsiaBahrain, "Asia/Bahrain" },
            { IanaTimeZone.AsiaKuwait, "Asia/Kuwait" },
            { IanaTimeZone.AsiaQatar, "Asia/Qatar" },
            { IanaTimeZone.AsiaAden, "Asia/Aden" },
            { IanaTimeZone.AsiaDubai, "Asia/Dubai" },
            { IanaTimeZone.AsiaMuscat, "Asia/Muscat" },
            { IanaTimeZone.EtcGMTminus4, "Etc/GMT-4" },
            { IanaTimeZone.AsiaBaghdad, "Asia/Baghdad" },
            { IanaTimeZone.AmericaBuenosAires, "America/Buenos_Aires" },
            { IanaTimeZone.AmericaArgentinaLaRioja, "America/Argentina/La_Rioja" },
            { IanaTimeZone.AmericaArgentinaRioGallegos, "America/Argentina/Rio_Gallegos" },
            { IanaTimeZone.AmericaArgentinaSalta, "America/Argentina/Salta" },
            { IanaTimeZone.AmericaArgentinaSanJuan, "America/Argentina/San_Juan" },
            { IanaTimeZone.AmericaArgentinaSanLuis, "America/Argentina/San_Luis" },
            { IanaTimeZone.AmericaArgentinaTucuman, "America/Argentina/Tucuman" },
            { IanaTimeZone.AmericaArgentinaUshuaia, "America/Argentina/Ushuaia" },
            { IanaTimeZone.AmericaCatamarca, "America/Catamarca" },
            { IanaTimeZone.AmericaCordoba, "America/Cordoba" },
            { IanaTimeZone.AmericaJujuy, "America/Jujuy" },
            { IanaTimeZone.AmericaMendoza, "America/Mendoza" },
            { IanaTimeZone.EuropeAstrakhan, "Europe/Astrakhan" },
            { IanaTimeZone.EuropeUlyanovsk, "Europe/Ulyanovsk" },
            { IanaTimeZone.AmericaHalifax, "America/Halifax" },
            { IanaTimeZone.AtlanticBermuda, "Atlantic/Bermuda" },
            { IanaTimeZone.AmericaGlaceBay, "America/Glace_Bay" },
            { IanaTimeZone.AmericaGooseBay, "America/Goose_Bay" },
            { IanaTimeZone.AmericaMoncton, "America/Moncton" },
            { IanaTimeZone.AmericaThule, "America/Thule" },
            { IanaTimeZone.AustraliaEucla, "Australia/Eucla" },
            { IanaTimeZone.AsiaBaku, "Asia/Baku" },
            { IanaTimeZone.AtlanticAzores, "Atlantic/Azores" },
            { IanaTimeZone.AmericaScoresbysund, "America/Scoresbysund" },
            { IanaTimeZone.AmericaBahia, "America/Bahia" },
            { IanaTimeZone.AsiaDhaka, "Asia/Dhaka" },
            { IanaTimeZone.AsiaThimphu, "Asia/Thimphu" },
            { IanaTimeZone.EuropeMinsk, "Europe/Minsk" },
            { IanaTimeZone.PacificBougainville, "Pacific/Bougainville" },
            { IanaTimeZone.AmericaRegina, "America/Regina" },
            { IanaTimeZone.AmericaSwiftCurrent, "America/Swift_Current" },
            { IanaTimeZone.AtlanticCapeVerde, "Atlantic/Cape_Verde" },
            { IanaTimeZone.EtcGMTplus1, "Etc/GMT+1" },
            { IanaTimeZone.AsiaYerevan, "Asia/Yerevan" },
            { IanaTimeZone.AustraliaAdelaide, "Australia/Adelaide" },
            { IanaTimeZone.AustraliaBrokenHill, "Australia/Broken_Hill" },
            { IanaTimeZone.AmericaGuatemala, "America/Guatemala" },
            { IanaTimeZone.AmericaBelize, "America/Belize" },
            { IanaTimeZone.AmericaCostaRica, "America/Costa_Rica" },
            { IanaTimeZone.PacificGalapagos, "Pacific/Galapagos" },
            { IanaTimeZone.AmericaTegucigalpa, "America/Tegucigalpa" },
            { IanaTimeZone.AmericaManagua, "America/Managua" },
            { IanaTimeZone.AmericaElSalvador, "America/El_Salvador" },
            { IanaTimeZone.EtcGMTplus6, "Etc/GMT+6" },
            { IanaTimeZone.AsiaAlmaty, "Asia/Almaty" },
            { IanaTimeZone.AntarcticaVostok, "Antarctica/Vostok" },
            { IanaTimeZone.AsiaUrumqi, "Asia/Urumqi" },
            { IanaTimeZone.IndianChagos, "Indian/Chagos" },
            { IanaTimeZone.AsiaBishkek, "Asia/Bishkek" },
            { IanaTimeZone.AsiaQostanay, "Asia/Qostanay" },
            { IanaTimeZone.EtcGMTminus6, "Etc/GMT-6" },
            { IanaTimeZone.AmericaCuiaba, "America/Cuiaba" },
            { IanaTimeZone.AmericaCampoGrande, "America/Campo_Grande" },
            { IanaTimeZone.EuropeBudapest, "Europe/Budapest" },
            { IanaTimeZone.EuropeTirane, "Europe/Tirane" },
            { IanaTimeZone.EuropePrague, "Europe/Prague" },
            { IanaTimeZone.EuropePodgorica, "Europe/Podgorica" },
            { IanaTimeZone.EuropeBelgrade, "Europe/Belgrade" },
            { IanaTimeZone.EuropeLjubljana, "Europe/Ljubljana" },
            { IanaTimeZone.EuropeBratislava, "Europe/Bratislava" },
            { IanaTimeZone.EuropeWarsaw, "Europe/Warsaw" },
            { IanaTimeZone.EuropeSarajevo, "Europe/Sarajevo" },
            { IanaTimeZone.EuropeZagreb, "Europe/Zagreb" },
            { IanaTimeZone.EuropeSkopje, "Europe/Skopje" },
            { IanaTimeZone.PacificGuadalcanal, "Pacific/Guadalcanal" },
            { IanaTimeZone.AntarcticaCasey, "Antarctica/Casey" },
            { IanaTimeZone.PacificPonape, "Pacific/Ponape" },
            { IanaTimeZone.PacificKosrae, "Pacific/Kosrae" },
            { IanaTimeZone.PacificNoumea, "Pacific/Noumea" },
            { IanaTimeZone.PacificEfate, "Pacific/Efate" },
            { IanaTimeZone.EtcGMTminus11, "Etc/GMT-11" },
            { IanaTimeZone.AmericaMexicoCity, "America/Mexico_City" },
            { IanaTimeZone.AmericaBahiaBanderas, "America/Bahia_Banderas" },
            { IanaTimeZone.AmericaMerida, "America/Merida" },
            { IanaTimeZone.AmericaMonterrey, "America/Monterrey" },
            { IanaTimeZone.AmericaChicago, "America/Chicago" },
            { IanaTimeZone.AmericaWinnipeg, "America/Winnipeg" },
            { IanaTimeZone.AmericaRainyRiver, "America/Rainy_River" },
            { IanaTimeZone.AmericaRankinInlet, "America/Rankin_Inlet" },
            { IanaTimeZone.AmericaResolute, "America/Resolute" },
            { IanaTimeZone.AmericaMatamoros, "America/Matamoros" },
            { IanaTimeZone.AmericaIndianaKnox, "America/Indiana/Knox" },
            { IanaTimeZone.AmericaIndianaTellCity, "America/Indiana/Tell_City" },
            { IanaTimeZone.AmericaMenominee, "America/Menominee" },
            { IanaTimeZone.AmericaNorthDakotaBeulah, "America/North_Dakota/Beulah" },
            { IanaTimeZone.AmericaNorthDakotaCenter, "America/North_Dakota/Center" },
            { IanaTimeZone.AmericaNorthDakotaNewSalem, "America/North_Dakota/New_Salem" },
            { IanaTimeZone.CST6CDT, "CST6CDT" },
            { IanaTimeZone.PacificChatham, "Pacific/Chatham" },
            { IanaTimeZone.AsiaShanghai, "Asia/Shanghai" },
            { IanaTimeZone.AsiaHongKong, "Asia/Hong_Kong" },
            { IanaTimeZone.AsiaMacau, "Asia/Macau" },
            { IanaTimeZone.AmericaHavana, "America/Havana" },
            { IanaTimeZone.EtcGMTplus12, "Etc/GMT+12" },
            { IanaTimeZone.AfricaNairobi, "Africa/Nairobi" },
            { IanaTimeZone.AntarcticaSyowa, "Antarctica/Syowa" },
            { IanaTimeZone.AfricaDjibouti, "Africa/Djibouti" },
            { IanaTimeZone.AfricaAsmera, "Africa/Asmera" },
            { IanaTimeZone.AfricaAddisAbaba, "Africa/Addis_Ababa" },
            { IanaTimeZone.IndianComoro, "Indian/Comoro" },
            { IanaTimeZone.IndianAntananarivo, "Indian/Antananarivo" },
            { IanaTimeZone.AfricaMogadishu, "Africa/Mogadishu" },
            { IanaTimeZone.AfricaDaresSalaam, "Africa/Dar_es_Salaam" },
            { IanaTimeZone.AfricaKampala, "Africa/Kampala" },
            { IanaTimeZone.IndianMayotte, "Indian/Mayotte" },
            { IanaTimeZone.EtcGMTminus3, "Etc/GMT-3" },
            { IanaTimeZone.AustraliaBrisbane, "Australia/Brisbane" },
            { IanaTimeZone.AustraliaLindeman, "Australia/Lindeman" },
            { IanaTimeZone.EuropeChisinau, "Europe/Chisinau" },
            { IanaTimeZone.AmericaSaoPaulo, "America/Sao_Paulo" },
            { IanaTimeZone.PacificEaster, "Pacific/Easter" },
            { IanaTimeZone.AmericaCancun, "America/Cancun" },
            { IanaTimeZone.AmericaNewYork, "America/New_York" },
            { IanaTimeZone.AmericaNassau, "America/Nassau" },
            { IanaTimeZone.AmericaToronto, "America/Toronto" },
            { IanaTimeZone.AmericaIqaluit, "America/Iqaluit" },
            { IanaTimeZone.AmericaMontreal, "America/Montreal" },
            { IanaTimeZone.AmericaNipigon, "America/Nipigon" },
            { IanaTimeZone.AmericaPangnirtung, "America/Pangnirtung" },
            { IanaTimeZone.AmericaThunderBay, "America/Thunder_Bay" },
            { IanaTimeZone.AmericaDetroit, "America/Detroit" },
            { IanaTimeZone.AmericaIndianaPetersburg, "America/Indiana/Petersburg" },
            { IanaTimeZone.AmericaIndianaVincennes, "America/Indiana/Vincennes" },
            { IanaTimeZone.AmericaIndianaWinamac, "America/Indiana/Winamac" },
            { IanaTimeZone.AmericaKentuckyMonticello, "America/Kentucky/Monticello" },
            { IanaTimeZone.AmericaLouisville, "America/Louisville" },
            { IanaTimeZone.EST5EDT, "EST5EDT" },
            { IanaTimeZone.AfricaCairo, "Africa/Cairo" },
            { IanaTimeZone.AsiaYekaterinburg, "Asia/Yekaterinburg" },
            { IanaTimeZone.EuropeKiev, "Europe/Kiev" },
            { IanaTimeZone.EuropeMariehamn, "Europe/Mariehamn" },
            { IanaTimeZone.EuropeSofia, "Europe/Sofia" },
            { IanaTimeZone.EuropeTallinn, "Europe/Tallinn" },
            { IanaTimeZone.EuropeHelsinki, "Europe/Helsinki" },
            { IanaTimeZone.EuropeVilnius, "Europe/Vilnius" },
            { IanaTimeZone.EuropeRiga, "Europe/Riga" },
            { IanaTimeZone.EuropeUzhgorod, "Europe/Uzhgorod" },
            { IanaTimeZone.EuropeZaporozhye, "Europe/Zaporozhye" },
            { IanaTimeZone.PacificFiji, "Pacific/Fiji" },
            { IanaTimeZone.EuropeLondon, "Europe/London" },
            { IanaTimeZone.AtlanticCanary, "Atlantic/Canary" },
            { IanaTimeZone.AtlanticFaeroe, "Atlantic/Faeroe" },
            { IanaTimeZone.EuropeGuernsey, "Europe/Guernsey" },
            { IanaTimeZone.EuropeDublin, "Europe/Dublin" },
            { IanaTimeZone.EuropeIsleofMan, "Europe/Isle_of_Man" },
            { IanaTimeZone.EuropeJersey, "Europe/Jersey" },
            { IanaTimeZone.EuropeLisbon, "Europe/Lisbon" },
            { IanaTimeZone.AtlanticMadeira, "Atlantic/Madeira" },
            { IanaTimeZone.EuropeBucharest, "Europe/Bucharest" },
            { IanaTimeZone.AsiaNicosia, "Asia/Nicosia" },
            { IanaTimeZone.AsiaFamagusta, "Asia/Famagusta" },
            { IanaTimeZone.EuropeAthens, "Europe/Athens" },
            { IanaTimeZone.AsiaTbilisi, "Asia/Tbilisi" },
            { IanaTimeZone.AmericaGodthab, "America/Godthab" },
            { IanaTimeZone.AtlanticReykjavik, "Atlantic/Reykjavik" },
            { IanaTimeZone.AtlanticStHelena, "Atlantic/St_Helena" },
            { IanaTimeZone.AfricaOuagadougou, "Africa/Ouagadougou" },
            { IanaTimeZone.AfricaAbidjan, "Africa/Abidjan" },
            { IanaTimeZone.AfricaAccra, "Africa/Accra" },
            { IanaTimeZone.AfricaBanjul, "Africa/Banjul" },
            { IanaTimeZone.AfricaConakry, "Africa/Conakry" },
            { IanaTimeZone.AfricaBissau, "Africa/Bissau" },
            { IanaTimeZone.AfricaMonrovia, "Africa/Monrovia" },
            { IanaTimeZone.AfricaBamako, "Africa/Bamako" },
            { IanaTimeZone.AfricaNouakchott, "Africa/Nouakchott" },
            { IanaTimeZone.AfricaFreetown, "Africa/Freetown" },
            { IanaTimeZone.AfricaDakar, "Africa/Dakar" },
            { IanaTimeZone.AfricaLome, "Africa/Lome" },
            { IanaTimeZone.AmericaPortAuPrince, "America/Port-au-Prince" },
            { IanaTimeZone.PacificHonolulu, "Pacific/Honolulu" },
            { IanaTimeZone.PacificRarotonga, "Pacific/Rarotonga" },
            { IanaTimeZone.PacificTahiti, "Pacific/Tahiti" },
            { IanaTimeZone.PacificJohnston, "Pacific/Johnston" },
            { IanaTimeZone.EtcGMTplus10, "Etc/GMT+10" },
            { IanaTimeZone.AsiaCalcutta, "Asia/Calcutta" },
            { IanaTimeZone.AsiaTehran, "Asia/Tehran" },
            { IanaTimeZone.AsiaJerusalem, "Asia/Jerusalem" },
            { IanaTimeZone.AsiaAmman, "Asia/Amman" },
            { IanaTimeZone.EuropeKaliningrad, "Europe/Kaliningrad" },
            { IanaTimeZone.AsiaSeoul, "Asia/Seoul" },
            { IanaTimeZone.AfricaTripoli, "Africa/Tripoli" },
            { IanaTimeZone.PacificKiritimati, "Pacific/Kiritimati" },
            { IanaTimeZone.EtcGMTminus14, "Etc/GMT-14" },
            { IanaTimeZone.AustraliaLordHowe, "Australia/Lord_Howe" },
            { IanaTimeZone.AsiaMagadan, "Asia/Magadan" },
            { IanaTimeZone.AmericaPuntaArenas, "America/Punta_Arenas" },
            { IanaTimeZone.PacificMarquesas, "Pacific/Marquesas" },
            { IanaTimeZone.IndianMauritius, "Indian/Mauritius" },
            { IanaTimeZone.IndianReunion, "Indian/Reunion" },
            { IanaTimeZone.IndianMahe, "Indian/Mahe" },
            { IanaTimeZone.AsiaBeirut, "Asia/Beirut" },
            { IanaTimeZone.AmericaMontevideo, "America/Montevideo" },
            { IanaTimeZone.AfricaCasablanca, "Africa/Casablanca" },
            { IanaTimeZone.AfricaElAaiun, "Africa/El_Aaiun" },
            { IanaTimeZone.AmericaChihuahua, "America/Chihuahua" },
            { IanaTimeZone.AmericaMazatlan, "America/Mazatlan" },
            { IanaTimeZone.AmericaDenver, "America/Denver" },
            { IanaTimeZone.AmericaEdmonton, "America/Edmonton" },
            { IanaTimeZone.AmericaCambridgeBay, "America/Cambridge_Bay" },
            { IanaTimeZone.AmericaInuvik, "America/Inuvik" },
            { IanaTimeZone.AmericaYellowknife, "America/Yellowknife" },
            { IanaTimeZone.AmericaOjinaga, "America/Ojinaga" },
            { IanaTimeZone.AmericaBoise, "America/Boise" },
            { IanaTimeZone.MST7MDT, "MST7MDT" },
            { IanaTimeZone.AsiaRangoon, "Asia/Rangoon" },
            { IanaTimeZone.IndianCocos, "Indian/Cocos" },
            { IanaTimeZone.AsiaNovosibirsk, "Asia/Novosibirsk" },
            { IanaTimeZone.AfricaWindhoek, "Africa/Windhoek" },
            { IanaTimeZone.AsiaKatmandu, "Asia/Katmandu" },
            { IanaTimeZone.PacificAuckland, "Pacific/Auckland" },
            { IanaTimeZone.AntarcticaMcMurdo, "Antarctica/McMurdo" },
            { IanaTimeZone.AmericaStJohns, "America/St_Johns" },
            { IanaTimeZone.PacificNorfolk, "Pacific/Norfolk" },
            { IanaTimeZone.AsiaIrkutsk, "Asia/Irkutsk" },
            { IanaTimeZone.AsiaKrasnoyarsk, "Asia/Krasnoyarsk" },
            { IanaTimeZone.AsiaNovokuznetsk, "Asia/Novokuznetsk" },
            { IanaTimeZone.AsiaPyongyang, "Asia/Pyongyang" },
            { IanaTimeZone.AsiaOmsk, "Asia/Omsk" },
            { IanaTimeZone.AmericaSantiago, "America/Santiago" },
            { IanaTimeZone.AmericaTijuana, "America/Tijuana" },
            { IanaTimeZone.AmericaSantaIsabel, "America/Santa_Isabel" },
            { IanaTimeZone.AmericaLosAngeles, "America/Los_Angeles" },
            { IanaTimeZone.AmericaVancouver, "America/Vancouver" },
            { IanaTimeZone.PST8PDT, "PST8PDT" },
            { IanaTimeZone.AsiaKarachi, "Asia/Karachi" },
            { IanaTimeZone.AmericaAsuncion, "America/Asuncion" },
            { IanaTimeZone.AsiaQyzylorda, "Asia/Qyzylorda" },
            { IanaTimeZone.EuropeParis, "Europe/Paris" },
            { IanaTimeZone.EuropeBrussels, "Europe/Brussels" },
            { IanaTimeZone.EuropeCopenhagen, "Europe/Copenhagen" },
            { IanaTimeZone.AfricaCeuta, "Africa/Ceuta" },
            { IanaTimeZone.EuropeMadrid, "Europe/Madrid" },
            { IanaTimeZone.AsiaSrednekolymsk, "Asia/Srednekolymsk" },
            { IanaTimeZone.AsiaKamchatka, "Asia/Kamchatka" },
            { IanaTimeZone.AsiaAnadyr, "Asia/Anadyr" },
            { IanaTimeZone.EuropeSamara, "Europe/Samara" },
            { IanaTimeZone.EuropeMoscow, "Europe/Moscow" },
            { IanaTimeZone.EuropeKirov, "Europe/Kirov" },
            { IanaTimeZone.EuropeSimferopol, "Europe/Simferopol" },
            { IanaTimeZone.AmericaCayenne, "America/Cayenne" },
            { IanaTimeZone.AntarcticaRothera, "Antarctica/Rothera" },
            { IanaTimeZone.AntarcticaPalmer, "Antarctica/Palmer" },
            { IanaTimeZone.AmericaFortaleza, "America/Fortaleza" },
            { IanaTimeZone.AmericaBelem, "America/Belem" },
            { IanaTimeZone.AmericaMaceio, "America/Maceio" },
            { IanaTimeZone.AmericaRecife, "America/Recife" },
            { IanaTimeZone.AmericaSantarem, "America/Santarem" },
            { IanaTimeZone.AtlanticStanley, "Atlantic/Stanley" },
            { IanaTimeZone.AmericaParamaribo, "America/Paramaribo" },
            { IanaTimeZone.EtcGMTplus3, "Etc/GMT+3" },
            { IanaTimeZone.AmericaBogota, "America/Bogota" },
            { IanaTimeZone.AmericaRioBranco, "America/Rio_Branco" },
            { IanaTimeZone.AmericaEirunepe, "America/Eirunepe" },
            { IanaTimeZone.AmericaCoralHarbour, "America/Coral_Harbour" },
            { IanaTimeZone.AmericaGuayaquil, "America/Guayaquil" },
            { IanaTimeZone.AmericaJamaica, "America/Jamaica" },
            { IanaTimeZone.AmericaCayman, "America/Cayman" },
            { IanaTimeZone.AmericaPanama, "America/Panama" },
            { IanaTimeZone.AmericaLima, "America/Lima" },
            { IanaTimeZone.EtcGMTplus5, "Etc/GMT+5" },
            { IanaTimeZone.AmericaLaPaz, "America/La_Paz" },
            { IanaTimeZone.AmericaAntigua, "America/Antigua" },
            { IanaTimeZone.AmericaAnguilla, "America/Anguilla" },
            { IanaTimeZone.AmericaAruba, "America/Aruba" },
            { IanaTimeZone.AmericaBarbados, "America/Barbados" },
            { IanaTimeZone.AmericaStBarthelemy, "America/St_Barthelemy" },
            { IanaTimeZone.AmericaKralendijk, "America/Kralendijk" },
            { IanaTimeZone.AmericaManaus, "America/Manaus" },
            { IanaTimeZone.AmericaBoaVista, "America/Boa_Vista" },
            { IanaTimeZone.AmericaPortoVelho, "America/Porto_Velho" },
            { IanaTimeZone.AmericaBlancSablon, "America/Blanc-Sablon" },
            { IanaTimeZone.AmericaCuracao, "America/Curacao" },
            { IanaTimeZone.AmericaDominica, "America/Dominica" },
            { IanaTimeZone.AmericaSantoDomingo, "America/Santo_Domingo" },
            { IanaTimeZone.AmericaGrenada, "America/Grenada" },
            { IanaTimeZone.AmericaGuadeloupe, "America/Guadeloupe" },
            { IanaTimeZone.AmericaGuyana, "America/Guyana" },
            { IanaTimeZone.AmericaStKitts, "America/St_Kitts" },
            { IanaTimeZone.AmericaStLucia, "America/St_Lucia" },
            { IanaTimeZone.AmericaMarigot, "America/Marigot" },
            { IanaTimeZone.AmericaMartinique, "America/Martinique" },
            { IanaTimeZone.AmericaMontserrat, "America/Montserrat" },
            { IanaTimeZone.AmericaPuertoRico, "America/Puerto_Rico" },
            { IanaTimeZone.AmericaLowerPrinces, "America/Lower_Princes" },
            { IanaTimeZone.AmericaPortofSpain, "America/Port_of_Spain" },
            { IanaTimeZone.AmericaStVincent, "America/St_Vincent" },
            { IanaTimeZone.AmericaTortola, "America/Tortola" },
            { IanaTimeZone.AmericaStThomas, "America/St_Thomas" },
            { IanaTimeZone.EtcGMTplus4, "Etc/GMT+4" },
            { IanaTimeZone.AsiaBangkok, "Asia/Bangkok" },
            { IanaTimeZone.AntarcticaDavis, "Antarctica/Davis" },
            { IanaTimeZone.IndianChristmas, "Indian/Christmas" },
            { IanaTimeZone.AsiaJakarta, "Asia/Jakarta" },
            { IanaTimeZone.AsiaPontianak, "Asia/Pontianak" },
            { IanaTimeZone.AsiaPhnomPenh, "Asia/Phnom_Penh" },
            { IanaTimeZone.AsiaVientiane, "Asia/Vientiane" },
            { IanaTimeZone.AsiaSaigon, "Asia/Saigon" },
            { IanaTimeZone.EtcGMTminus7, "Etc/GMT-7" },
            { IanaTimeZone.AmericaMiquelon, "America/Miquelon" },
            { IanaTimeZone.AsiaSakhalin, "Asia/Sakhalin" },
            { IanaTimeZone.PacificApia, "Pacific/Apia" },
            { IanaTimeZone.AfricaSaoTome, "Africa/Sao_Tome" },
            { IanaTimeZone.EuropeSaratov, "Europe/Saratov" },
            { IanaTimeZone.AsiaSingapore, "Asia/Singapore" },
            { IanaTimeZone.AsiaBrunei, "Asia/Brunei" },
            { IanaTimeZone.AsiaMakassar, "Asia/Makassar" },
            { IanaTimeZone.AsiaKualaLumpur, "Asia/Kuala_Lumpur" },
            { IanaTimeZone.AsiaKuching, "Asia/Kuching" },
            { IanaTimeZone.AsiaManila, "Asia/Manila" },
            { IanaTimeZone.EtcGMTminus8, "Etc/GMT-8" },
            { IanaTimeZone.AfricaJohannesburg, "Africa/Johannesburg" },
            { IanaTimeZone.AfricaBujumbura, "Africa/Bujumbura" },
            { IanaTimeZone.AfricaGaborone, "Africa/Gaborone" },
            { IanaTimeZone.AfricaLubumbashi, "Africa/Lubumbashi" },
            { IanaTimeZone.AfricaMaseru, "Africa/Maseru" },
            { IanaTimeZone.AfricaBlantyre, "Africa/Blantyre" },
            { IanaTimeZone.AfricaMaputo, "Africa/Maputo" },
            { IanaTimeZone.AfricaKigali, "Africa/Kigali" },
            { IanaTimeZone.AfricaMbabane, "Africa/Mbabane" },
            { IanaTimeZone.AfricaLusaka, "Africa/Lusaka" },
            { IanaTimeZone.AfricaHarare, "Africa/Harare" },
            { IanaTimeZone.EtcGMTminus2, "Etc/GMT-2" },
            { IanaTimeZone.AfricaJuba, "Africa/Juba" },
            { IanaTimeZone.AsiaColombo, "Asia/Colombo" },
            { IanaTimeZone.AfricaKhartoum, "Africa/Khartoum" },
            { IanaTimeZone.AsiaDamascus, "Asia/Damascus" },
            { IanaTimeZone.AsiaTaipei, "Asia/Taipei" },
            { IanaTimeZone.AustraliaHobart, "Australia/Hobart" },
            { IanaTimeZone.AustraliaCurrie, "Australia/Currie" },
            { IanaTimeZone.AntarcticaMacquarie, "Antarctica/Macquarie" },
            { IanaTimeZone.AmericaAraguaina, "America/Araguaina" },
            { IanaTimeZone.AsiaTokyo, "Asia/Tokyo" },
            { IanaTimeZone.AsiaJayapura, "Asia/Jayapura" },
            { IanaTimeZone.PacificPalau, "Pacific/Palau" },
            { IanaTimeZone.AsiaDili, "Asia/Dili" },
            { IanaTimeZone.EtcGMTminus9, "Etc/GMT-9" },
            { IanaTimeZone.AsiaTomsk, "Asia/Tomsk" },
            { IanaTimeZone.PacificTongatapu, "Pacific/Tongatapu" },
            { IanaTimeZone.AsiaChita, "Asia/Chita" },
            { IanaTimeZone.EuropeIstanbul, "Europe/Istanbul" },
            { IanaTimeZone.AmericaGrandTurk, "America/Grand_Turk" },
            { IanaTimeZone.AmericaIndianapolis, "America/Indianapolis" },
            { IanaTimeZone.AmericaIndianaMarengo, "America/Indiana/Marengo" },
            { IanaTimeZone.AmericaIndianaVevay, "America/Indiana/Vevay" },
            { IanaTimeZone.AmericaPhoenix, "America/Phoenix" },
            { IanaTimeZone.AmericaCreston, "America/Creston" },
            { IanaTimeZone.AmericaDawsonCreek, "America/Dawson_Creek" },
            { IanaTimeZone.AmericaFortNelson, "America/Fort_Nelson" },
            { IanaTimeZone.AmericaHermosillo, "America/Hermosillo" },
            { IanaTimeZone.EtcGMTplus7, "Etc/GMT+7" },
            { IanaTimeZone.EtcGMTminus12, "Etc/GMT-12" },
            { IanaTimeZone.PacificTarawa, "Pacific/Tarawa" },
            { IanaTimeZone.PacificMajuro, "Pacific/Majuro" },
            { IanaTimeZone.PacificKwajalein, "Pacific/Kwajalein" },
            { IanaTimeZone.PacificNauru, "Pacific/Nauru" },
            { IanaTimeZone.PacificFunafuti, "Pacific/Funafuti" },
            { IanaTimeZone.PacificWake, "Pacific/Wake" },
            { IanaTimeZone.PacificWallis, "Pacific/Wallis" },
            { IanaTimeZone.EtcGMTminus13, "Etc/GMT-13" },
            { IanaTimeZone.PacificEnderbury, "Pacific/Enderbury" },
            { IanaTimeZone.PacificFakaofo, "Pacific/Fakaofo" },
            { IanaTimeZone.EtcGMT, "Etc/GMT" },
            { IanaTimeZone.AmericaDanmarkshavn, "America/Danmarkshavn" },
            { IanaTimeZone.EtcUTC, "Etc/UTC" },
            { IanaTimeZone.EtcGMTplus2, "Etc/GMT+2" },
            { IanaTimeZone.AmericaNoronha, "America/Noronha" },
            { IanaTimeZone.AtlanticSouthGeorgia, "Atlantic/South_Georgia" },
            { IanaTimeZone.EtcGMTplus8, "Etc/GMT+8" },
            { IanaTimeZone.PacificPitcairn, "Pacific/Pitcairn" },
            { IanaTimeZone.EtcGMTplus9, "Etc/GMT+9" },
            { IanaTimeZone.PacificGambier, "Pacific/Gambier" },
            { IanaTimeZone.EtcGMTplus11, "Etc/GMT+11" },
            { IanaTimeZone.PacificPagoPago, "Pacific/Pago_Pago" },
            { IanaTimeZone.PacificNiue, "Pacific/Niue" },
            { IanaTimeZone.PacificMidway, "Pacific/Midway" },
            { IanaTimeZone.AsiaUlaanbaatar, "Asia/Ulaanbaatar" },
            { IanaTimeZone.AsiaChoibalsan, "Asia/Choibalsan" },
            { IanaTimeZone.AmericaCaracas, "America/Caracas" },
            { IanaTimeZone.AsiaVladivostok, "Asia/Vladivostok" },
            { IanaTimeZone.AsiaUstNera, "Asia/Ust-Nera" },
            { IanaTimeZone.EuropeVolgograd, "Europe/Volgograd" },
            { IanaTimeZone.AustraliaPerth, "Australia/Perth" },
            { IanaTimeZone.AfricaLagos, "Africa/Lagos" },
            { IanaTimeZone.AfricaLuanda, "Africa/Luanda" },
            { IanaTimeZone.AfricaPortoNovo, "Africa/Porto-Novo" },
            { IanaTimeZone.AfricaKinshasa, "Africa/Kinshasa" },
            { IanaTimeZone.AfricaBangui, "Africa/Bangui" },
            { IanaTimeZone.AfricaBrazzaville, "Africa/Brazzaville" },
            { IanaTimeZone.AfricaDouala, "Africa/Douala" },
            { IanaTimeZone.AfricaAlgiers, "Africa/Algiers" },
            { IanaTimeZone.AfricaLibreville, "Africa/Libreville" },
            { IanaTimeZone.AfricaMalabo, "Africa/Malabo" },
            { IanaTimeZone.AfricaNiamey, "Africa/Niamey" },
            { IanaTimeZone.AfricaNdjamena, "Africa/Ndjamena" },
            { IanaTimeZone.AfricaTunis, "Africa/Tunis" },
            { IanaTimeZone.EtcGMTminus1, "Etc/GMT-1" },
            { IanaTimeZone.EuropeBerlin, "Europe/Berlin" },
            { IanaTimeZone.EuropeAndorra, "Europe/Andorra" },
            { IanaTimeZone.EuropeVienna, "Europe/Vienna" },
            { IanaTimeZone.EuropeZurich, "Europe/Zurich" },
            { IanaTimeZone.EuropeBusingen, "Europe/Busingen" },
            { IanaTimeZone.EuropeGibraltar, "Europe/Gibraltar" },
            { IanaTimeZone.EuropeRome, "Europe/Rome" },
            { IanaTimeZone.EuropeVaduz, "Europe/Vaduz" },
            { IanaTimeZone.EuropeLuxembourg, "Europe/Luxembourg" },
            { IanaTimeZone.EuropeMonaco, "Europe/Monaco" },
            { IanaTimeZone.EuropeMalta, "Europe/Malta" },
            { IanaTimeZone.EuropeAmsterdam, "Europe/Amsterdam" },
            { IanaTimeZone.EuropeOslo, "Europe/Oslo" },
            { IanaTimeZone.EuropeStockholm, "Europe/Stockholm" },
            { IanaTimeZone.ArcticLongyearbyen, "Arctic/Longyearbyen" },
            { IanaTimeZone.EuropeSanMarino, "Europe/San_Marino" },
            { IanaTimeZone.EuropeVatican, "Europe/Vatican" },
            { IanaTimeZone.AsiaHovd, "Asia/Hovd" },
            { IanaTimeZone.AsiaTashkent, "Asia/Tashkent" },
            { IanaTimeZone.AntarcticaMawson, "Antarctica/Mawson" },
            { IanaTimeZone.AsiaOral, "Asia/Oral" },
            { IanaTimeZone.AsiaAqtau, "Asia/Aqtau" },
            { IanaTimeZone.AsiaAqtobe, "Asia/Aqtobe" },
            { IanaTimeZone.AsiaAtyrau, "Asia/Atyrau" },
            { IanaTimeZone.IndianMaldives, "Indian/Maldives" },
            { IanaTimeZone.IndianKerguelen, "Indian/Kerguelen" },
            { IanaTimeZone.AsiaDushanbe, "Asia/Dushanbe" },
            { IanaTimeZone.AsiaAshgabat, "Asia/Ashgabat" },
            { IanaTimeZone.AsiaSamarkand, "Asia/Samarkand" },
            { IanaTimeZone.EtcGMTminus5, "Etc/GMT-5" },
            { IanaTimeZone.AsiaHebron, "Asia/Hebron" },
            { IanaTimeZone.AsiaGaza, "Asia/Gaza" },
            { IanaTimeZone.PacificPortMoresby, "Pacific/Port_Moresby" },
            { IanaTimeZone.AntarcticaDumontDUrville, "Antarctica/DumontDUrville" },
            { IanaTimeZone.PacificTruk, "Pacific/Truk" },
            { IanaTimeZone.PacificGuam, "Pacific/Guam" },
            { IanaTimeZone.PacificSaipan, "Pacific/Saipan" },
            { IanaTimeZone.EtcGMTminus10, "Etc/GMT-10" },
            { IanaTimeZone.AsiaYakutsk, "Asia/Yakutsk" },
            { IanaTimeZone.AsiaKhandyga, "Asia/Khandyga" },
            { IanaTimeZone.AmericaWhitehorse, "America/Whitehorse" },
            { IanaTimeZone.AmericaDawson, "America/Dawson" },
            { IanaTimeZone.AfricaTimbuktu, "Africa/Timbuktu" },
            { IanaTimeZone.Egypt, "Egypt" },
            { IanaTimeZone.AfricaAsmara, "Africa/Asmara" },
            { IanaTimeZone.Libya, "Libya" },
            { IanaTimeZone.AmericaAtka, "America/Atka" },
            { IanaTimeZone.USAleutian, "US/Aleutian" },
            { IanaTimeZone.USAlaska, "US/Alaska" },
            { IanaTimeZone.AmericaArgentinaBuenosAires, "America/Argentina/Buenos_Aires" },
            { IanaTimeZone.AmericaArgentinaCatamarca, "America/Argentina/Catamarca" },
            { IanaTimeZone.AmericaArgentinaComodRivadavia, "America/Argentina/ComodRivadavia" },
            { IanaTimeZone.AmericaArgentinaCordoba, "America/Argentina/Cordoba" },
            { IanaTimeZone.AmericaRosario, "America/Rosario" },
            { IanaTimeZone.AmericaArgentinaJujuy, "America/Argentina/Jujuy" },
            { IanaTimeZone.AmericaArgentinaMendoza, "America/Argentina/Mendoza" },
            { IanaTimeZone.AmericaAtikokan, "America/Atikokan" },
            { IanaTimeZone.USCentral, "US/Central" },
            { IanaTimeZone.AmericaShiprock, "America/Shiprock" },
            { IanaTimeZone.Navajo, "Navajo" },
            { IanaTimeZone.USMountain, "US/Mountain" },
            { IanaTimeZone.USMichigan, "US/Michigan" },
            { IanaTimeZone.CanadaMountain, "Canada/Mountain" },
            { IanaTimeZone.CanadaAtlantic, "Canada/Atlantic" },
            { IanaTimeZone.Cuba, "Cuba" },
            { IanaTimeZone.AmericaIndianaIndianapolis, "America/Indiana/Indianapolis" },
            { IanaTimeZone.USEastIndiana, "US/East-Indiana" },
            { IanaTimeZone.AmericaKnoxIN, "America/Knox_IN" },
            { IanaTimeZone.USIndianaStarke, "US/Indiana-Starke" },
            { IanaTimeZone.Jamaica, "Jamaica" },
            { IanaTimeZone.AmericaKentuckyLouisville, "America/Kentucky/Louisville" },
            { IanaTimeZone.USPacific, "US/Pacific" },
            { IanaTimeZone.BrazilWest, "Brazil/West" },
            { IanaTimeZone.MexicoBajaSur, "Mexico/BajaSur" },
            { IanaTimeZone.MexicoGeneral, "Mexico/General" },
            { IanaTimeZone.USEastern, "US/Eastern" },
            { IanaTimeZone.BrazilDeNoronha, "Brazil/DeNoronha" },
            { IanaTimeZone.AmericaNuuk, "America/Nuuk" },
            { IanaTimeZone.USArizona, "US/Arizona" },
            { IanaTimeZone.AmericaVirgin, "America/Virgin" },
            { IanaTimeZone.CanadaSaskatchewan, "Canada/Saskatchewan" },
            { IanaTimeZone.AmericaPortoAcre, "America/Porto_Acre" },
            { IanaTimeZone.BrazilAcre, "Brazil/Acre" },
            { IanaTimeZone.ChileContinental, "Chile/Continental" },
            { IanaTimeZone.BrazilEast, "Brazil/East" },
            { IanaTimeZone.CanadaNewfoundland, "Canada/Newfoundland" },
            { IanaTimeZone.AmericaEnsenada, "America/Ensenada" },
            { IanaTimeZone.MexicoBajaNorte, "Mexico/BajaNorte" },
            { IanaTimeZone.CanadaEastern, "Canada/Eastern" },
            { IanaTimeZone.CanadaPacific, "Canada/Pacific" },
            { IanaTimeZone.CanadaYukon, "Canada/Yukon" },
            { IanaTimeZone.CanadaCentral, "Canada/Central" },
            { IanaTimeZone.AsiaAshkhabad, "Asia/Ashkhabad" },
            { IanaTimeZone.AsiaDacca, "Asia/Dacca" },
            { IanaTimeZone.AsiaHoChiMinh, "Asia/Ho_Chi_Minh" },
            { IanaTimeZone.Hongkong, "Hongkong" },
            { IanaTimeZone.AsiaTelAviv, "Asia/Tel_Aviv" },
            { IanaTimeZone.Israel, "Israel" },
            { IanaTimeZone.AsiaKathmandu, "Asia/Kathmandu" },
            { IanaTimeZone.AsiaKolkata, "Asia/Kolkata" },
            { IanaTimeZone.AsiaMacao, "Asia/Macao" },
            { IanaTimeZone.AsiaUjungPandang, "Asia/Ujung_Pandang" },
            { IanaTimeZone.EuropeNicosia, "Europe/Nicosia" },
            { IanaTimeZone.ROK, "ROK" },
            { IanaTimeZone.AsiaChongqing, "Asia/Chongqing" },
            { IanaTimeZone.AsiaChungking, "Asia/Chungking" },
            { IanaTimeZone.AsiaHarbin, "Asia/Harbin" },
            { IanaTimeZone.PRC, "PRC" },
            { IanaTimeZone.Singapore, "Singapore" },
            { IanaTimeZone.ROC, "ROC" },
            { IanaTimeZone.Iran, "Iran" },
            { IanaTimeZone.AsiaThimbu, "Asia/Thimbu" },
            { IanaTimeZone.Japan, "Japan" },
            { IanaTimeZone.AsiaUlanBator, "Asia/Ulan_Bator" },
            { IanaTimeZone.AsiaKashgar, "Asia/Kashgar" },
            { IanaTimeZone.AsiaYangon, "Asia/Yangon" },
            { IanaTimeZone.WET, "WET" },
            { IanaTimeZone.AtlanticFaroe, "Atlantic/Faroe" },
            { IanaTimeZone.Iceland, "Iceland" },
            { IanaTimeZone.AustraliaSouth, "Australia/South" },
            { IanaTimeZone.AustraliaQueensland, "Australia/Queensland" },
            { IanaTimeZone.AustraliaYancowinna, "Australia/Yancowinna" },
            { IanaTimeZone.AustraliaNorth, "Australia/North" },
            { IanaTimeZone.AustraliaTasmania, "Australia/Tasmania" },
            { IanaTimeZone.AustraliaLHI, "Australia/LHI" },
            { IanaTimeZone.AustraliaVictoria, "Australia/Victoria" },
            { IanaTimeZone.AustraliaWest, "Australia/West" },
            { IanaTimeZone.AustraliaACT, "Australia/ACT" },
            { IanaTimeZone.AustraliaCanberra, "Australia/Canberra" },
            { IanaTimeZone.AustraliaNSW, "Australia/NSW" },
            { IanaTimeZone.HST, "HST" },
            { IanaTimeZone.EST, "EST" },
            { IanaTimeZone.MST, "MST" },
            { IanaTimeZone.EtcGMTplus0, "Etc/GMT+0" },
            { IanaTimeZone.EtcGMTminus0, "Etc/GMT-0" },
            { IanaTimeZone.EtcGMT0, "Etc/GMT0" },
            { IanaTimeZone.EtcGreenwich, "Etc/Greenwich" },
            { IanaTimeZone.GMT, "GMT" },
            { IanaTimeZone.GMTplus0, "GMT+0" },
            { IanaTimeZone.GMTminus0, "GMT-0" },
            { IanaTimeZone.GMT0, "GMT0" },
            { IanaTimeZone.Greenwich, "Greenwich" },
            { IanaTimeZone.EtcUCT, "Etc/UCT" },
            { IanaTimeZone.EtcUniversal, "Etc/Universal" },
            { IanaTimeZone.EtcZulu, "Etc/Zulu" },
            { IanaTimeZone.UCT, "UCT" },
            { IanaTimeZone.UTC, "UTC" },
            { IanaTimeZone.Universal, "Universal" },
            { IanaTimeZone.Zulu, "Zulu" },
            { IanaTimeZone.MET, "MET" },
            { IanaTimeZone.EET, "EET" },
            { IanaTimeZone.EuropeTiraspol, "Europe/Tiraspol" },
            { IanaTimeZone.Eire, "Eire" },
            { IanaTimeZone.AsiaIstanbul, "Asia/Istanbul" },
            { IanaTimeZone.Turkey, "Turkey" },
            { IanaTimeZone.Portugal, "Portugal" },
            { IanaTimeZone.EuropeBelfast, "Europe/Belfast" },
            { IanaTimeZone.GB, "GB" },
            { IanaTimeZone.GBEire, "GB-Eire" },
            { IanaTimeZone.WSU, "W-SU" },
            { IanaTimeZone.AtlanticJanMayen, "Atlantic/Jan_Mayen" },
            { IanaTimeZone.CET, "CET" },
            { IanaTimeZone.Poland, "Poland" },
            { IanaTimeZone.AntarcticaSouthPole, "Antarctica/South_Pole" },
            { IanaTimeZone.NZ, "NZ" },
            { IanaTimeZone.NZCHAT, "NZ-CHAT" },
            { IanaTimeZone.PacificChuuk, "Pacific/Chuuk" },
            { IanaTimeZone.PacificYap, "Pacific/Yap" },
            { IanaTimeZone.ChileEasterIsland, "Chile/EasterIsland" },
            { IanaTimeZone.USHawaii, "US/Hawaii" },
            { IanaTimeZone.Kwajalein, "Kwajalein" },
            { IanaTimeZone.PacificSamoa, "Pacific/Samoa" },
            { IanaTimeZone.USSamoa, "US/Samoa" },
            { IanaTimeZone.PacificPohnpei, "Pacific/Pohnpei" },
            { IanaTimeZone.AntarcticaTroll, "Antarctica/Troll" }
        };
    }
}