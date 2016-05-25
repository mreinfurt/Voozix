using Entities;
using UnityEngine;

namespace Game
{
    public class BasicTutorialController : MonoBehaviour
    {
        [SerializeField] private StarController star;

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
    }
}