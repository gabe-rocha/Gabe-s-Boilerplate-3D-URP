using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneReference sceneToBeLoaded;

    public void LoadSceneToBeLoaded() {
        SceneManager.LoadScene(sceneToBeLoaded);
    }
}