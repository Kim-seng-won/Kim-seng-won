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
        talkData.Add(1000, new string[] { "헉!", "살려줘!", "내 친구가 납치되었어..","이 동굴에는 이상한 괴물이 득실득실해 그래서 내가 직접 구하러 갈 수 없었어.. ㅠ.ㅠ", "어디로 대려갔는지는 나도 모르지..",
            "그치만.. 넌 강해보이니까!", "괴물을 어떻게 처리하고 친구를 구해줘!" });
        nameData.Add(1000,  "김나약함");

        talkData.Add(1001, new string[] { "역시!","구하러 와줬구나!!","너가 올지 어떻게 알았냐구?","사실 여기 갇힌지 1024시간이나 지났거든..","그래서 누군가 곧 오겠지 하고있었어!", "누가 날 가둔거냐구?", 
        "사실 나도 잘 몰라. 아마 지성있는 보스 몬스터 에셋을 구하면 이름이 정해지지 않을까?", "그럼 이만~"});
        nameData.Add(1001, "김무지함");

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
