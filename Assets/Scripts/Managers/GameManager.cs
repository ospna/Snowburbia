using Assets.Scripts.Entities;
using Assets.Scripts.Utilities;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.Managers
{
    // Manages the entire game
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        HalfTimePanel _halfTimePanel;

        [SerializeField]
        StartPanel _startPanel;

        [SerializeField]
        MatchInfoPanel _matchInfoPanel;

        [SerializeField]
        MatchOverPanel _matchOverPanel;

        [SerializeField]
        MatchOnPanel _matchOnPanel;

        // Event raised when continuing to second half
        public Action OnContinueToSecondHalf;

        // Event raised when switching to match on
        public Action OnMessageSwitchToMatchOn;

        private void Awake()
        {
            // register the game manager to some events
            Ball.Instance.OnBallLaunched += SoundManager.Instance.PlayBallKickedSound;
            MatchManager.Instance.OnGoalScored += SoundManager.Instance.PlayGoalScoredSound;

            //register managers to listen to me
            OnContinueToSecondHalf += MatchManager.Instance.Instance_OnContinueToSecondHalf;
            OnMessageSwitchToMatchOn += MatchManager.Instance.Instance_OnMessagedSwitchToMatchOn;

            //listen to match manager events
            MatchManager.Instance.OnBroadcastHalfStart += Instance_OnBroadcastHalfStart;
            MatchManager.Instance.OnBroadcastMatchStart += Instance_OnBroadcastMatchStart;
            MatchManager.Instance.OnEnterHalfTime += Instance_OnEnterHalfTime;
            MatchManager.Instance.OnEnterWaitForMatchOnInstruction += Instance_OnEnterWaitForMatchOnInstruction;
            MatchManager.Instance.OnExitHalfTime += Instance_OnExitHalfTime;
            MatchManager.Instance.OnExitMatchOver += Instance_OnExitMatchOver;
            MatchManager.Instance.OnExitWaitForMatchOnInstruction += Instance_OnExitWaitForMatchOnInstruction;
            MatchManager.Instance.OnFinishBroadcastHalfStart += _Instance_OnFinishBroadcastHalfStart;
            MatchManager.Instance.OnFinishBroadcastMatchStart += Instance_OnFinishBroadcastMatchStart;
            MatchManager.Instance.OnGoalScored += Instance_OnGoalScored;
            MatchManager.Instance.OnMatchOver += Instance_OnMatchOver;
            MatchManager.Instance.OnMatchPlayStart += Instance_OnMatchPlayStart;
            MatchManager.Instance.OnMatchPlayStop += Instance_OnMatchPlayStop;
            MatchManager.Instance.OnTick += Instance_OnTick;
        }

        private void Instance_OnBroadcastHalfStart(string message)
        {
            ShowInfoPanel(message);
        }

        private void Instance_OnBroadcastMatchStart(string message)
        {
            ShowInfoPanel(message);
        }

        private void Instance_OnEnterHalfTime(string message)
        {
            _halfTimePanel.infoText.text = message;
            _halfTimePanel.Root.gameObject.SetActive(true);
        }

        private void Instance_OnEnterWaitForMatchOnInstruction()
        {
            _startPanel.Root.gameObject.SetActive(true);
        }

        private void Instance_OnExitHalfTime()
        {
            _halfTimePanel.Root.gameObject.SetActive(false);
        }

        private void Instance_OnExitMatchOver()
        {
            _matchOverPanel.Root.gameObject.SetActive(false);
        }

        private void Instance_OnExitWaitForMatchOnInstruction()
        {
            _startPanel.Root.gameObject.SetActive(false);
        }

        private void _Instance_OnFinishBroadcastHalfStart()
        {
            HideInfoPanel();
        }

        private void Instance_OnFinishBroadcastMatchStart()
        {
            HideInfoPanel();
        }

        private void Instance_OnGoalScored(string message, string message1)
        {

            _matchOnPanel.hometeamScoreText.text = message;
            _matchOnPanel.awayteamScoreText.text = message1;

        }

        private void Instance_OnMatchOver(string message)
        {
            _matchOverPanel.winnerText.text = message;
            _matchOverPanel.MainGameUI.gameObject.SetActive(true);
            _matchOverPanel.Root.gameObject.SetActive(true);
        }

        private void Instance_OnMatchPlayStart()
        {
            _matchOnPanel.Root.gameObject.SetActive(true);
        }

        private void Instance_OnMatchPlayStop()
        {
            _matchOnPanel.Root.gameObject.SetActive(false);
        }

        private void Instance_OnTick(int half, int minutes, int seconds)
        {
            //declare the string
            string timeInfo = string.Empty;

            //prepare the message
            string infoHalf = half == 1 ? "" : "";

            timeInfo = string.Format("{0}{1}:{2}", 
                infoHalf, 
                minutes.ToString("00"), 
                seconds.ToString("00" + " "));

            //set the ui
            _matchOnPanel.gametimerText.text = timeInfo;
        }

        private void HideInfoPanel()
        {
            _matchInfoPanel.Root.gameObject.SetActive(false);
        }

        public void Instance_OnContinueToSecondHalf()
        {
            ActionUtility.Invoke_Action(OnContinueToSecondHalf);
        }

        public void Instance_OnMessageSwitchToMatchOn()
        {
            ActionUtility.Invoke_Action(OnMessageSwitchToMatchOn);
        }

        private void ShowInfoPanel(string message)
        {
            _matchInfoPanel.infoText.text = message;
            _matchInfoPanel.Root.gameObject.SetActive(true);
        }
    }

    [Serializable]
    public struct HalfTimePanel
    {
        public TextMeshProUGUI infoText;
        public Transform Root;
    }

    [Serializable]
    public struct StartPanel
    {
        public Transform Root;
    }

    [Serializable]
    public struct MatchInfoPanel
    {

        public TextMeshProUGUI infoText;
        public Transform Root;
    }

    [Serializable]
    public struct MatchOnPanel
    {

        public TextMeshProUGUI hometeamScoreText;
        public TextMeshProUGUI awayteamScoreText;
        public TextMeshProUGUI gametimerText;
       
        public Transform Root;
    }

    [Serializable]
    public struct MatchOverPanel
    {
        public TextMeshProUGUI winnerText;

        public Transform Root;

        public Transform MainGameUI;
    }
}
