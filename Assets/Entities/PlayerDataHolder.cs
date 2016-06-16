#region Namespaces

using Data;
using UnityEngine;

#endregion

namespace Entities
{
    public class PlayerDataHolder : MonoBehaviour
    {
        #region Public

        public static PlayerDataHolder Instance;

        #endregion

        #region Fields

        private PlayerData data = new PlayerData();

        #endregion

        #region Properties

        public PlayerData Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        #endregion

        #region Methods

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(this.gameObject);
                Instance = this;
                this.data = PlayerDataSaveController.Load();
            }
            else
            {
                Destroy(this.transform.gameObject);
            }
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