#region Namespaces

using Entities;
using UnityEngine;

#endregion

namespace Game
{
    public class BasicTutorialController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private StarController star;

        #endregion

        #region Methods

        private void Start()
        {
        }

        private void Update()
        {
            this.transform.localScale = new Vector3(this.star.Scale, this.star.Scale, this.star.Scale);
            var attachStarPosition = this.star.gameObject.transform.position;
            attachStarPosition.y += transform.localScale.y / 2;
            attachStarPosition.x += transform.localScale.y;

            this.transform.position = attachStarPosition;
        }

        #endregion
    }
}