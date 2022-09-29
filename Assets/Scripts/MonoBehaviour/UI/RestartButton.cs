using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class RestartButton : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}