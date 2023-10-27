using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;
using System;

public class Language {
	#region Variables	
	private static List<List<string>> language;	
	public static List<List<string>> GetLanguage{
		get{ 
			return language;
		}
	}
    private static string[] textLanguageNames = new string[] {
        "language_Arab",
        "language_Bulgarian",
        "language_Chinese_S",
        "language_Chinese_T",
        "language_Croatian",
        "language_Czech",
        "language_Danish",
        "language_Dutch",
        "language_Finnish",
        "language_French",
        "language_German",
        "language_Greek",
        "language_Hebrew",
        "language_Hindi",
        "language_Hungarian",
        "language_Indonesian",
        "language_Italian",
        "language_Japan",
        "language_Korean",
        "language_Lithuanian",
        "language_Malai",
        "language_Norwegian",
        "language_Persian",
        "language_Polish",
        "language_Romanian",
        "language_Russian",
        "language_Serbian",
        "language_Spanish",
        "language_Swedish",
        "language_Thai",
        "language_Turkish",
        "language_Ukrainian",
        "language_Vietnamese"
    };

	#endregion
	#region Methods	
	public static void LoadLanguage()
	{
		language = new List<List<string>> ();
        for(int i = 0; i < 35; i++)
        {
            language.Add(new List<string>());
        }
				
		try
		{
			TextAsset textAsset = (TextAsset) Resources.Load("language");  
			XmlDocument xmldoc = new XmlDocument ();
			xmldoc.LoadXml ( textAsset.text );
			XmlNodeList nodeList = xmldoc.GetElementsByTagName ("language");
			foreach(XmlNode node in nodeList)
			{
				XmlNodeList content = node.ChildNodes;
				foreach(XmlNode node1 in content)
				{
					if (node1.Name == "english")
						language [0].Add (node1.InnerText);
					else
						language [1].Add (node1.InnerText);
				}
			}
            //for(int i = 0; i < textLanguageNames.Length; i++)//----- Use this to enable all languages
            //{
            //    TextAsset textAsset1 = (TextAsset)Resources.Load(textLanguageNames[i]);
            //    XmlDocument xmldoc1 = new XmlDocument();
            //    xmldoc1.LoadXml(textAsset1.text);
            //    XmlNodeList nodeList1 = xmldoc1.GetElementsByTagName("language");
            //    foreach (XmlNode node1 in nodeList1)
            //    {
            //        XmlNodeList content1 = node1.ChildNodes;
            //        foreach (XmlNode node2 in content1)
            //        {
            //            language[2 + i].Add(node2.InnerText);                        
            //        }
            //    }
            //}

		}catch{
		}
	}	
	public static void ChangeLanguage(int id, LanguageText[] languageText)
	{
		try
		{
			General.GetSetConfig.languageID = id;
			for(int i = 0; i < languageText.Length; i++){
				languageText [i].ChangeText ();
			}	
		}catch {
		}
	}
	#endregion
}