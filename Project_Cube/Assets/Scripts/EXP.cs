using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static ShopManager;

public class EXP : MonoSingleton<EXP>, ISubject
{
    List<IObserver> observers = new List<IObserver>();
    public List<int> _needEXP = new List<int>();

    public int _level = 1;

    int _expAmount = 0;
    public class EXPData {
        internal int level;
        internal int needEXPToLevelUp;

        public void Read(XmlNode node)
        {
            level = int.Parse(node.Attributes["level"].Value);
            needEXPToLevelUp = int.Parse(node.Attributes["needEXP"].Value);
        }
    }
    protected override void OnCreate()
    {
        _needEXP = new List<int>();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("Assets/XML/LevelEXPInfor.xml");

        XmlNodeList nodes = xmlDoc.SelectNodes("LevelData/Level");

        for (int i = 0; i < nodes.Count; i++)
        {
            EXPData expData = new EXPData();
            expData.Read(nodes[i]);

            _needEXP.Add(expData.needEXPToLevelUp);
        }
    }

    public void AddExp(int amount) {
        _expAmount += amount;

        if (_needEXP[_level] <= _expAmount) {
            _level++;
        }

        Notify();
    }

    public int GetLevel() {
        return _level;
    }
    public int GetNowExpAmount() {
        return _expAmount - _needEXP[_level - 1];
    }

    public int GetNeedExpAmount() {
        return (_needEXP[_level] - _needEXP[_level - 1]);
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.UpdateData();
        }
    }
}
