#region Namespaces

using System;
using UnityEngine;
using UnityEngine.EventSystems;

#endregion

namespace UI
{
    public class LevelButtonController : MonoBehaviour, ISelectHandler
    {
        #region Methods

        [SerializeField]
        private int level;

        [SerializeField]
        private CampaignSelectionController campaignSelectionController;

        private void Start()
        {
        }

        private void Update()
        {
        }

        public void OnSelect(BaseEventData eventData)
        {
            this.campaignSelectionController.SelectLevel(this.level);
        }

        #endregion
    }
}