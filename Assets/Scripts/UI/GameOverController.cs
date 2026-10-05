using UnityEngine;
using UnityEngine.SceneManagement;
using RunMarrakech.Core;
namespace RunMarrakech.UI
{
    public sealed class GameOverController : MonoBehaviour
    {
        public GameObject Panel;
        public void Show(){Time.timeScale=0f;if(Panel)Panel.SetActive(true);}
        public void Restart(){Time.timeScale=1f;SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);}
        public void Home(){Time.timeScale=1f;SceneManager.LoadScene(0);}
        public void Save(){GameManager.Instance?.SaveRun();}
    }
}