using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoSingleton<UnitManager>
{

    public class UnitData {
        public string name;
        public string stringKey;
        public string img;
        public string prefabPath;

        public void Read(XmlNode node)
        {
            name = node.Attributes["name"].Value;
            stringKey = node.Attributes["stringKey"].Value;
            img = node.Attributes["img"].Value;
            prefabPath = "Assets/Prefabs/Objects/" + node.Attributes["prefab"].Value;
        }
    }

    public Dictionary<string, UnitData> _dic;

    protected override void OnCreate()
    {
        _dic = new Dictionary<string, UnitData>();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("Assets/XML/UnitInfor.xml");

        XmlNodeList nodes = xmlDoc.SelectNodes("UnitData/Unit");

        for (int i = 0; i < nodes.Count; i++)
        {
            UnitData unitData = new UnitData();
            unitData.Read(nodes[i]);

            _dic.Add(unitData.name, unitData);
        }
    }

    public Unit CreateUnit(string key)
    {
        string path = _dic[key].prefabPath;
        Unit result = AssetOpenManager.Import<Unit>(path);

        return result;
    }
    public string GetSpriteKey(string key) {
        return _dic[key].img;
    }
}
