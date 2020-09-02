using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private string menuScene;

    public void BackToMainWorld()
    {
        SceneManager.LoadScene(menuScene, LoadSceneMode.Single);
    }
}
