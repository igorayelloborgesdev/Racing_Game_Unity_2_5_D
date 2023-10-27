using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

public class LanguageOrganizer
{
#if (UNITY_EDITOR)
    [MenuItem("Language/Organizer")]
    private static void Organizer()
    {
        List<List<string>> language = new List<List<string>>();
        language.Add(new List<string>());
        language.Add(new List<string>());
        TextAsset textAsset = (TextAsset)Resources.Load("languageReorg");
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
        XmlDocument doc = new XmlDocument();
        doc.LoadXml("<languages></languages>");
        for (int i = 0; i < language[0].Count; i++)
        {
            XmlElement newElem = doc.CreateElement("language");
            newElem.SetAttribute("id", i.ToString());
            XmlElement newElem1 = doc.CreateElement("english");
            XmlElement newElem2 = doc.CreateElement("portuguese");
            doc.DocumentElement.AppendChild(newElem).AppendChild(newElem1);
            doc.DocumentElement.AppendChild(newElem).AppendChild(newElem2);
            newElem1.InnerText = language[0][i];
            newElem2.InnerText = language[1][i];
        }        
        doc.PreserveWhitespace = true;
        doc.Save("data.xml");        
    }

#endif
}
