using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Content.UI
{
    public class UIDamageNumberText : MonoBehaviour
    {
        [SerializeField] private Text _uiDamageText;

        public float lifetime;
        private float lifeCounter;

        public float floatSpeed = 1f;

    

        // Update is called once per frame
        void Update()
        {
            if(lifeCounter > 0)
            {
                lifeCounter -= Time.deltaTime;

                if(lifeCounter <= 0)
                {
                    Destroy(gameObject);

                    // DamageNumberController.instance.PlaceInPool(this);
                }
            }

            /* if(Input.GetKeyDown(KeyCode.U))
            {
                Setup(45);
            } */

            transform.position += Vector3.up * (floatSpeed * Time.deltaTime);
        }

        public void Setup(int damageDisplay)
        {
            lifeCounter = lifetime;

            _uiDamageText.text = damageDisplay.ToString();
        }
    }
}