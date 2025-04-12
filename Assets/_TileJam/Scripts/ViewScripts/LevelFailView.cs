using System;
using System.Collections.Generic;
using _TileJam.Scripts.ManagerScripts;
using TMPro;
using UnityEngine;

namespace _TileJam.Scripts.ViewScripts
{
    public class LevelFailView : BaseView
    {
        [SerializeField] private TMP_Text levelFailText;
        private Dictionary<LevelFailType, string> levelFailTypeList = new Dictionary<LevelFailType, string>();

        private void Awake()
        {
            AddFailTypesToList();
        }

        public override void Start()
        {
            GameManager.Instance.OnLevelFail += OnOpen;
        }

        protected void OnDestroy()
        {
            GameManager.Instance.OnLevelFail -= OnOpen;
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            SetLevelFailType();
        }
        
        private void SetLevelFailType()
        {
            levelFailText.text = levelFailTypeList[GameManager.Instance.currentLevelFailType];
        }
        private void AddFailTypesToList()
        {
            levelFailTypeList.Add(LevelFailType.DebugType,"Debug Fail!");
            levelFailTypeList.Add(LevelFailType.DeadEnd,"Dead End!");
            levelFailTypeList.Add(LevelFailType.TimeOut,"Time Out!");
            levelFailTypeList.Add(LevelFailType.OutOfArea,"Out of Area");
        }
    }
}
