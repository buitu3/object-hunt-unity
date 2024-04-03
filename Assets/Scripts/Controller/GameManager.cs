using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace OjbectHunt.GamePlay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameManager()
        {
            Instance = this;
        }
    }

}
