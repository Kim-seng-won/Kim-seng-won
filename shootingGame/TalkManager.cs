using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, string> nameData;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        nameData = new Dictionary<int, string>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "��!", "�����!", "�� ģ���� ��ġ�Ǿ���..","�� �������� �̻��� ������ ��ǵ���� �׷��� ���� ���� ���Ϸ� �� �� ������.. ��.��", "���� ����������� ���� ����..",
            "��ġ��.. �� ���غ��̴ϱ�!", "������ ��� ó���ϰ� ģ���� ������!" });
        nameData.Add(1000,  "�質����");

        talkData.Add(1001, new string[] { "����!","���Ϸ� ���ᱸ��!!","�ʰ� ���� ��� �˾ҳı�?","��� ���� ������ 1024�ð��̳� �����ŵ�..","�׷��� ������ �� ������ �ϰ��־���!", "���� �� ���аųı�?", 
        "��� ���� �� ����. �Ƹ� �����ִ� ���� ���� ������ ���ϸ� �̸��� �������� ������?", "�׷� �̸�~"});
        nameData.Add(1001, "�蹫����");

    }

    public string getTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
        {
            return talkData[id][talkIndex];
        }
    }
    public string getName(int id)
    {
        return nameData[id];
    }
}
