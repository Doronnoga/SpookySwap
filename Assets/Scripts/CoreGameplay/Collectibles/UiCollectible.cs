using CollectibleClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UICollectibleClass
{
    public class UiCollectible : MonoBehaviour
    {
        [Header("Collectible Connected")]
        [SerializeField]
        [Header("collectible")]
        private LevelCollectible levelCollectible;
        [SerializeField]
        GameObject collectibleIcon;


        void Start()
        {
            collectibleIcon.SetActive(false);

            if (levelCollectible != null)//on starts subscribe methods to event
            {
                levelCollectible.OnCollected += ShowCollectible;
            }
        }

        private void ShowCollectible()
        {
            collectibleIcon.SetActive(true);
        }
    } 
}
