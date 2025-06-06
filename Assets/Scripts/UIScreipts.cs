using UnityEngine;

public class UIScreipts : MonoBehaviour
{
    public void newScene(string sceneName)
    {
        GameObject sceneManager = GameObject.Find("----SceneManager----");
        sceneManager.GetComponent<SceneManager>().LoadScene(sceneName);
    }
    public void quitGame()
    {
        GameObject sceneManager = GameObject.Find("----SceneManager----");
        sceneManager.GetComponent<SceneManager>().QuitGame();
    }
}
