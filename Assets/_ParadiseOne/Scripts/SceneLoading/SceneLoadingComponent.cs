using UnityEngine;

public class SceneLoadingComponent : MonoBehaviour
{
    public void LoadScene(int sceneBuildIndex)
    {
        GameManager.Instance.LoadScene(sceneBuildIndex);
    }
}
