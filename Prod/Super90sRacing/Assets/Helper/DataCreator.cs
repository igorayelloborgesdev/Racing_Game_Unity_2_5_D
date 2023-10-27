using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class DataCreator
{
#if (UNITY_EDITOR)
    [MenuItem("Data/Country language")]
    private static void CountryLanguage()
    {
        List<List<string>> language = new List<List<string>>();
        language.Add(new List<string>());
        language.Add(new List<string>());
        TextAsset textAsset = (TextAsset)Resources.Load("language");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
        XmlNodeList nodeList = xmldoc.GetElementsByTagName("language");
        foreach (XmlNode node in nodeList)
        {
            XmlNodeList content = node.ChildNodes;
            foreach (XmlNode node1 in content)
            {
                if (node1.Name == "english")
                    language[0].Add(node1.InnerText);
                else
                    language[1].Add(node1.InnerText);
            }
        }

        List<List<string>> languageCountry = new List<List<string>>();
        languageCountry.Add(new List<string>());
        languageCountry.Add(new List<string>());
        TextAsset textAssetCountry = (TextAsset)Resources.Load("languageCountry");
        XmlDocument xmldocCountry = new XmlDocument();
        xmldocCountry.LoadXml(textAssetCountry.text);
        XmlNodeList nodeListCountry = xmldocCountry.GetElementsByTagName("language");
        foreach (XmlNode nodeCountry in nodeListCountry)
        {
            XmlNodeList contentCountry = nodeCountry.ChildNodes;
            foreach (XmlNode node1Country in contentCountry)
            {
                if (node1Country.Name == "english")
                    languageCountry[0].Add(node1Country.InnerText);
                else
                    languageCountry[1].Add(node1Country.InnerText);
            }
        }

        int inc = 0;
        XmlDocument doc = new XmlDocument();
        doc.LoadXml("<languages></languages>");
        for (int i = 0; i < language[0].Count; i++)
        {
            XmlElement newElem = doc.CreateElement("language");
            newElem.SetAttribute("id", inc.ToString());
            XmlElement newElem1 = doc.CreateElement("english");
            XmlElement newElem2 = doc.CreateElement("portuguese");
            doc.DocumentElement.AppendChild(newElem).AppendChild(newElem1);
            doc.DocumentElement.AppendChild(newElem).AppendChild(newElem2);
            newElem1.InnerText = language[0][i];
            newElem2.InnerText = language[1][i];
            inc++;
        }

        for (int i = 0; i < languageCountry[0].Count; i++)
        {
            XmlElement newElem = doc.CreateElement("language");
            newElem.SetAttribute("id", inc.ToString());
            XmlElement newElem1 = doc.CreateElement("english");
            XmlElement newElem2 = doc.CreateElement("portuguese");
            doc.DocumentElement.AppendChild(newElem).AppendChild(newElem1);
            doc.DocumentElement.AppendChild(newElem).AppendChild(newElem2);
            newElem1.InnerText = languageCountry[0][i];
            newElem2.InnerText = languageCountry[1][i];
            inc++;
        }

        doc.PreserveWhitespace = true;
        doc.Save("LANGUAGE_NEW.xml");

    }

    [MenuItem("Data/Country")]
    public static void Country()
    {
        string[] countriesCode = new string[]
        {
            "ARG",
            "AUS",
            "AUT",
            "BEL",
            "BRA",
            "BUL",
            "CAN",
            "CHI",
            "CHN",
            "COL",
            "CZE",
            "DEN",
            "ESP",
            "FIN",
            "FRA",
            "GBR",
            "GER",
            "GRE",
            "HUN",
            "IND",
            "IRL",
            "ITA",
            "JPN",
            "KOR",
            "MAR",
            "MEX",
            "MON",
            "NED",
            "NOR",
            "NZL",
            "POL",
            "POR",
            "ROM",
            "RSA",
            "SMR",
            "SUI",
            "SWE",
            "URU",
            "USA",
            "VEN"
        };

        List<CountryDTO> countryList = new List<CountryDTO>();

        for (int i = 0; i < countriesCode.Length; i++)
        {
            CountryDTO country = new CountryDTO()
            {
                id = i,
                idLanguage = 47 + i,
                countryCode = countriesCode[i],
                countryFlag = countriesCode[i] + "_flag"
            };
            countryList.Add(country);
        }

        CountryObjectDTO countryObjectDTO = new CountryObjectDTO();
        countryObjectDTO.countryDTO = countryList.ToArray();

        string dataAsJson = JsonConvert.SerializeObject(countryObjectDTO);
        string filePath = Application.persistentDataPath;

        var destinationDirectory = new DirectoryInfo(filePath);
        if (!destinationDirectory.Exists)
            destinationDirectory.Create();
        filePath = filePath + "/countries.json";
        File.WriteAllText(filePath, dataAsJson);

    }

    [MenuItem("Data/Tracks")]
    public static void Tracks()
    {
        int[] countriesID = new int[]
        {
            38,//USA
            4,//BRA
            34,//SMR
            26,//MON
            6,//CAN
            25,//MEX
            14,//FRA
            15,//GBR
            16,//GER
            18,//HUN
            3,//BEL
            21,//ITA
            31,//POR            
            12,//ESP
            22,//JPN
            1//AUS
        };

        int[] laps = new int[]
        {
            8,//USA
            7,//BRA
            6,//SMR
            8,//MON
            7,//CAN
            7,//MEX
            7,//FRA
            6,//GBR
            5,//GER
            8,//HUN
            4,//BEL
            5,//ITA
            7,//POR            
            7,//ESP
            5,//JPN
            8//AUS
        };

        int[] softTyreWear = new int[]
        {
            6,//USA
            5,//BRA
            4,//SMR
            6,//MON
            5,//CAN
            5,//MEX
            5,//FRA
            4,//GBR
            4,//GER
            6,//HUN
            3,//BEL
            4,//ITA
            5,//POR            
            5,//ESP
            4,//JPN
            6//AUS
        };

        int[] pitWindow = new int[]
        {
            3,//USA
            3,//BRA
            3,//SMR
            3,//MON
            3,//CAN
            3,//MEX
            3,//FRA
            3,//GBR
            3,//GER
            3,//HUN
            2,//BEL
            2,//ITA
            3,//POR            
            3,//ESP
            3,//JPN
            3//AUS
        };

        float[] pitStopPertinence = new float[]
        {
            3.0f,//USA
	        4.0f,//BRA
	        5.0f,//SMR
	        1.0f,//MON
	        5.5f,//CAN
	        6.5f,//MEX
	        7.5f,//FRA
	        4.5f,//GBR
	        7.75f,//GER
	        2.0f,//HUN
	        8.15f,//BEL
	        8.65f,//ITA
	        5.3f,//POR            
	        6.2f,//ESP
	        3.75f,//JPN
	        5.7f//AUS
        };

        float[] poleTime = new float[]
        {
            43.23635f,//USA
            40.37888f,//BRA
            39.51764f,//SMR
            55.99909f,//MON
            53.15543f,//CAN
            59.11231f,//MEX
            59.96529f,//FRA
            52.15488f,//GBR
            44.65842f,//GER
            60.15284f,//HUN
            50.30307f,//BEL
            42.28171f,//ITA
            58.22236f,//POR
            55.70346f,//ESP
            35.43778f,//JPN
            46.95984f//AUS
        };

        List<TrackDTO> trackList = new List<TrackDTO>();
        for (int i = 0; i < countriesID.Length; i++)
        {
            TrackDTO track = new TrackDTO()
            {
                id = i,
                trackFileName = "track_" + i.ToString(),
                countryId = countriesID[i],
                laps = laps[i],
                softTyreWear = softTyreWear[i],
                pitStopPertinence = pitStopPertinence[i],
                pitWindow = pitWindow[i],
                poleTime = poleTime[i]
            };
            trackList.Add(track);
        }

        TrackObjectDTO trackObjectDTO = new TrackObjectDTO();
        trackObjectDTO.trackDTO = trackList.ToArray();

        string dataAsJson = JsonConvert.SerializeObject(trackObjectDTO);
        string filePath = Application.persistentDataPath;

        var destinationDirectory = new DirectoryInfo(filePath);
        if (!destinationDirectory.Exists)
            destinationDirectory.Create();
        filePath = filePath + "/tracks.json";

        File.WriteAllText(filePath, dataAsJson);

    }

    [MenuItem("Data/Teams")]
    public static void Teams()
    {
        string[] teamNames = new string[]
        {
            "MacNally",//MacNally
            "Will",//Will
            "Forte",//Forte
            "Bennett",//Bennett
            "Focus",//Focus
            "Taylor",//Taylor
            "Lejeune",//Lejeune
            "Mineo",//Mineo
            "Jennings",//Jennings
            "House"//House
        };

        int[] tier = new int[]
        {
            0,//MacNally
            0,//Will
            1,//Forte
            1,//Bennett
            2,//Focus
            2,//Taylor
            3,//Lejeune
            3,//Mineo
            4,//Jennings
            4//House
        };

        int[] accel = new int[]
        {
            7,//MacNally
            9,//Will
            7,//Forte
            10,//Bennett
            6,//Focus
            7,//Taylor
            6,//Lejeune
            6,//Mineo
            3,//Jennings
            2//House
        };

        int[] speed = new int[]
        {
            10,//MacNally
            9,//Will
            10,//Forte
            7,//Bennett
            9,//Focus
            4,//Taylor
            6,//Lejeune
            3,//Mineo
            4,//Jennings
            5//House
        };

        int[] corner = new int[]
        {
            10,//MacNally
            9,//Will
            8,//Forte
            6,//Bennett
            5,//Focus
            6,//Taylor
            3,//Lejeune
            6,//Mineo
            2,//Jennings
            2//House
        };

        List<TeamDTO> teamList = new List<TeamDTO>();
        for (int i = 0; i < teamNames.Length; i++)
        {
            TeamDTO team = new TeamDTO()
            {
                id = i,
                name = teamNames[i],
                tier = tier[i],
                accel = accel[i],
                speed = speed[i],
                corner = corner[i],

                carImage = "carImage_" + i.ToString(),
                carFrontImage = "carFrontImage_" + i.ToString(),
                carBackImage = "carBackImage_" + i.ToString(),
                carImageIconStanding = "carImageIconStanding_" + i.ToString(),
                carImageIconGrid = "carImageIconGrid_" + i.ToString(),

                teamLogoFontColor = new ColorDTO() { r = 0.5f, g = 0.5f, b = 0.5f },

                teamLogoColorList = new List<ColorDTO>()
                {
                    new ColorDTO() { r = 0.1f, g = 0.1f, b = 0.1f },
                    new ColorDTO() { r = 0.5f, g = 0.5f, b = 0.5f },
                    new ColorDTO() { r = 0.9f, g = 0.9f, b = 0.9f }
                }.ToArray(),

                cockpitColorList = new List<ColorDTO>()
                {
                    new ColorDTO() { r = 0.1f, g = 0.1f, b = 0.1f },
                    new ColorDTO() { r = 0.5f, g = 0.5f, b = 0.5f }
                }.ToArray(),

                cockpitClothColorList = new List<ColorDTO>()
                {
                    new ColorDTO() { r = 0.25f, g = 0.25f, b = 0.25f },
                    new ColorDTO() { r = 0.75f, g = 0.75f, b = 0.75f }
                }.ToArray(),

                clothColorList = new List<ColorDTO>()
                {
                    new ColorDTO() { r = 0.25f, g = 0.25f, b = 0.25f },
                    new ColorDTO() { r = 0.5f, g = 0.5f, b = 0.5f },
                    new ColorDTO() { r = 0.75f, g = 0.75f, b = 0.75f },
                    new ColorDTO() { r = 1.0f, g = 1.0f, b = 1.0f }
                }.ToArray(),

            };
            teamList.Add(team);
        }

        TeamObjectDTO teamObjectDTO = new TeamObjectDTO();
        teamObjectDTO.teamDTO = teamList.ToArray();

        string dataAsJson = JsonConvert.SerializeObject(teamObjectDTO);
        string filePath = Application.persistentDataPath;

        var destinationDirectory = new DirectoryInfo(filePath);
        if (!destinationDirectory.Exists)
            destinationDirectory.Create();
        filePath = filePath + "/teams.json";
        File.WriteAllText(filePath, dataAsJson);

    }

    [MenuItem("Data/Drivers")]
    public static void Drivers()
    {
        string[] driverNames = new string[]
        {
            "Ailton Sendas",//BRA
            "Gerald Berg",//AUT
            "Noah Mansfiled",//GBR
            "Roberto Patmeri",//ITA
            "Alban Prorcher",//FRA
            "Jules Aledie",//FRA
            "Nilson Piqxel",//BRA
            "Manheim Schwein",//GER
            "Mikke Hakapalainen",//FIN
            "John Hebenderson",//GBR
            "Angelo De Colombo",//ITA
            "Shin Nakayama",//JPN
            "Élisée Combes",//FRA
            "Troilus Botgaert",//BEL
            "Paolo Marcone",//ITA
            "Gian Mordini",//ITA
            "Sergio Modesero",//ITA
            "Bertl Gaceminne",//BEL
            "Ivo Capello",//ITA
            "Marcio Gugel"//BRA
        };

        string[] code = new string[]
        {
            "SEN",//"Ailton Sendas",//BRA
            "BER",//"Gerald Berg",//AUT
            "MAN",//"Noah Mansfiled",//GBR
            "PAT",//"Roberto Patmeri",//ITA
            "PRO",//"Alban Prorcher",//FRA
            "ALE",//"Jules Aledie",//FRA
            "PIQ",//"Nilson Piqxel",//BRA
            "SCH",//"Manheim Schwein",//GER
            "HAK",//"Mikke Hakapalainen",//FIN
            "HEB",//"John Hebenderson",//GBR
            "DEC",//"Angelo De Colombo",//ITA
            "NAK",//"Shin Nakayama",//JPN
            "COM",//"Élisée Combes",//FRA
            "BOT",//"Troilus Botgaert",//BEL
            "MAR",//"Paolo Marcone",//ITA
            "MOR",//"Gian Mordini",//ITA
            "MOD",//"Sergio Modesero",//ITA
            "GAC",//"Bertl Gaceminne",//BEL
            "CAP",//"Ivo Capello",//ITA
            "GUG"//"Marcio Gugel"//BRA
        };

        int[] countryId = new int[]
        {
            4,//"SEN",//"Ailton Sendas",//BRA
            2,//"BER",//"Gerald Berg",//AUT
            15,//"MAN",//"Noah Mansfiled",//GBR
            21,//"PAT",//"Roberto Patmeri",//ITA
            14,//"PRO",//"Alban Prorcher",//FRA
            14,//"ALE",//"Jules Aledie",//FRA
            4,//"PIQ",//"Nilson Piqxel",//BRA
            16,//"SCH",//"Manheim Schwein",//GER
            13,//"HAK",//"Mikke Hakapalainen",//FIN
            15,//"HEB",//"John Hebenderson",//GBR
            21,//"DEC",//"Angelo De Colombo",//ITA
            22,//"NAK",//"Shin Nakayama",//JPN
            14,//"COM",//"Élisée Combes",//FRA
            3,//"BOT",//"Troilus Botgaert",//BEL
            21,//"MAR",//"Paolo Marcone",//ITA
            21,//"MOR",//"Gian Mordini",//ITA
            21,//"MOD",//"Sergio Modesero",//ITA
            3,//"GAC",//"Bertl Gaceminne",//BEL
            21,//"CAP",//"Ivo Capello",//ITA
            4//"GUG",//"Marcio Gugel"//BRA
        };

        int[] skill = new int[]
        {
            10,//"SEN",//"Ailton Sendas",//BRA
            7,//"BER",//"Gerald Berg",//AUT
            9,//"MAN",//"Noah Mansfiled",//GBR
            7,//"PAT",//"Roberto Patmeri",//ITA
            10,//"PRO",//"Alban Prorcher",//FRA
            7,//"ALE",//"Jules Aledie",//FRA
            9,//"PIQ",//"Nilson Piqxel",//BRA
            9,//"SCH",//"Manheim Schwein",//GER
            8,//"HAK",//"Mikke Hakapalainen",//FIN
            7,//"HEB",//"John Hebenderson",//GBR
            6,//"DEC",//"Angelo De Colombo",//ITA
            4,//"NAK",//"Shin Nakayama",//JPN
            4,//"COM",//"Élisée Combes",//FRA
            7,//"BOT",//"Troilus Botgaert",//BEL
            4,//"MAR",//"Paolo Marcone",//ITA
            5,//"MOR",//"Gian Mordini",//ITA
            5,//"MOD",//"Sergio Modesero",//ITA
            3,//"GAC",//"Bertl Gaceminne",//BEL
            5,//"CAP",//"Ivo Capello",//ITA
            4//"GUG",//"Marcio Gugel"//BRA
        };

        int[] teamId = new int[]
        {
            0,//"SEN",//"Ailton Sendas",//BRA
            0,//"BER",//"Gerald Berg",//AUT
            1,//"MAN",//"Noah Mansfiled",//GBR
            1,//"PAT",//"Roberto Patmeri",//ITA
            2,//"PRO",//"Alban Prorcher",//FRA
            2,//"ALE",//"Jules Aledie",//FRA
            3,//"PIQ",//"Nilson Piqxel",//BRA
            3,//"SCH",//"Manheim Schwein",//GER
            4,//"HAK",//"Mikke Hakapalainen",//FIN
            4,//"HEB",//"John Hebenderson",//GBR
            5,//"DEC",//"Angelo De Colombo",//ITA
            5,//"NAK",//"Shin Nakayama",//JPN
            6,//"COM",//"Élisée Combes",//FRA
            6,//"BOT",//"Troilus Botgaert",//BEL
            7,//"MAR",//"Paolo Marcone",//ITA
            7,//"MOR",//"Gian Mordini",//ITA
            8,//"MOD",//"Sergio Modesero",//ITA
            8,//"GAC",//"Bertl Gaceminne",//BEL
            9,//"CAP",//"Ivo Capello",//ITA
            9//"GUG",//"Marcio Gugel"//BRA
        };

        List<DriverDTO> driverList = new List<DriverDTO>();

        for (int i = 0; i < driverNames.Length; i++)
        {
            DriverDTO driver = new DriverDTO()
            {
                id = i,
                name = driverNames[i],
                code = code[i],
                countryId = countryId[i],
                skill = skill[i],
                avatarId = "avatar_" + i.ToString(),
                teamId = teamId[i],
                helmetColor = new ColorDTO() { r = 0.25f, g = 0.25f, b = 0.25f }
            };
            driverList.Add(driver);
        }

        DriverObjectDTO driverObjectDTO = new DriverObjectDTO();
        driverObjectDTO.driverDTO = driverList.ToArray();

        string dataAsJson = JsonConvert.SerializeObject(driverObjectDTO);
        string filePath = Application.persistentDataPath;

        var destinationDirectory = new DirectoryInfo(filePath);
        if (!destinationDirectory.Exists)
            destinationDirectory.Create();
        filePath = filePath + "/drivers.json";
        File.WriteAllText(filePath, dataAsJson);

    }
#endif
}
