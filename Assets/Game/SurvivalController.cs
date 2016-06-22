#region Namespaces

using Entities;
using UnityEngine;

#endregion

namespace Game
{
    public class SurvivalController : MonoBehaviour
    {
        #region Methods

        private void Start()
        {
            Events.Global.OnReset += OnReset;
        }

        private void OnReset()
        {
            PlayerDataHolder.Instance.Data.Score = 0;
        }

        private void Update()
        {
        }

        #endregion
    }
}