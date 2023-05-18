using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderBoard : MonoBehaviour
{
    public List<ScoreRecord> scoreRecords;
    private List<int> scoreList;

    public Button adButton;
    private void OnEnable()
    {
        Debug.Log("enable");
        scoreList = GameManager.instance.GetScoreListData();

        adButton.onClick.AddListener(AdsManager.instance.ShowRewardAd);
    }
    private void Start()
    {
        Debug.Log("start");
        SetLeaderBoardData();
    }
    private void SetLeaderBoardData()
    {
        for (int i = 0; i < scoreRecords.Count; i++)
        {
            if (i < scoreList.Count)
            {
                scoreRecords[i].SetScoreText(scoreList[i]);
                scoreRecords[i].gameObject.SetActive(true);
            } else
            {
                scoreRecords[i].gameObject.SetActive(false);
            }
        }
    }
}
