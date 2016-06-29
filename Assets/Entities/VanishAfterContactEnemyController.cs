#region Namespaces

using UnityEngine;

#endregion

namespace Entities
{
    public class VanishAfterContactEnemyController : MonoBehaviour
    {
        #region Methods

        private void Start()
        {
        }

        private void Update()
        {
        }

        private void OnCollisionEnter2D(Collision2D collider)
        {
            var colliderTag = collider.gameObject.tag.ToLower();
            if (colliderTag == "star" || colliderTag == "enemy")
            {
                return;
            }

            Destroy(this.gameObject);
        }

        #endregion
    }
}