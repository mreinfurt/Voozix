#region Namespaces

using UnityEngine;
using UnityEngine.EventSystems;

#endregion

namespace UI
{
    public class LevelButtonController : MonoBehaviour, ISelectHandler
    {
        #region Fields

        [SerializeField] private CampaignSelectionController campaignSelectionController;

        [SerializeField] private int level;

        #endregion

        #region Methods

        public void OnSelect(BaseEventData eventData)
        {
            this.campaignSelectionController.SelectLevel(this.level);
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

        #endregion
    }
}