using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithoutArch
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<Quests> quests;
        
        // Start is called before the first frame update
        private void Start()
        {
            
        }

        // Update is called once per frame
        private void Update()
        {
            var gameFinished = true;
            foreach (var quest in quests)
            {
                if (!quest.Completed())
                {
                    gameFinished = false;
                }
            }
            if (gameFinished)
            {
                WinGame();
            }
        }

        private void WinGame()
        {
            Debug.Log("Victory!");
        }
    }
}
