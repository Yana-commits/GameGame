using Game.Data;
using Services.Firebase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardController : MonoBehaviour
{
    [SerializeField]
    private LeaderBoardSmall prefab;
    [SerializeField]
    private Transform parent;
    [Range(1, 20)]
    [SerializeField]
    private int leadersAmount = 5;
    [SerializeField]
    private Button backButton;

    private List<LeaderBoardSmall> leaderViews = new List<LeaderBoardSmall>();

    private void Awake()
    {
        backButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        for (int i = 0; i < leaderViews.Count; i++)
        {
            Destroy(leaderViews[i].gameObject);
        }
        leaderViews.Clear();
    }

    private async void Initialize()
    {
        List<UserData> leaders = await FirebaseManager.databaseManager.GetLeaderBoard(leadersAmount);
        leadersAmount = Mathf.Min(leadersAmount, leaders.Count);
        for (int i = 0; i < leadersAmount; i++)
        {
            var leader = leaders[i];
            var leaderView = Instantiate(prefab, parent, false);

            leaderView.transform.localScale = Vector3.one;
            leaderView.gameObject.SetActive(true);
            var position = leaderView.transform.position;
            position.z = 0;
            leaderView.transform.localPosition = position;

            leaderView.Initialize(leader.Name, leader.score,leader.increaseNomber);
            leaderViews.Add(leaderView);
        }
    }
}
