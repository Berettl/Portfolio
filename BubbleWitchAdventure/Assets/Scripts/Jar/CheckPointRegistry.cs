using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointRegistry : MonoBehaviour
{
    static private CheckPointRegistry m_Instance;

    private Vector3 m_spawnPosition;
    [SerializeField]
    private int m_playerWandId = 0;
    private int m_checkpointSceneIndex = -1;

    private void Awake()
    {
        if (m_Instance == null)
        {
            DontDestroyOnLoad(this);
            m_Instance = this;
        }
    }

    static public void RegisterCheckPoint(Vector3 spawnPosition, WandContainer wandContainer, int sceneBuildIndex)
    {
        if (m_Instance.m_spawnPosition.Equals(spawnPosition) && m_Instance.m_checkpointSceneIndex == sceneBuildIndex)
        {
            return;
        }

        m_Instance.m_checkpointSceneIndex = sceneBuildIndex;
        m_Instance.m_playerWandId = wandContainer.GetCurrentWandID();
        m_Instance.m_spawnPosition = spawnPosition;
    }

    static public int GetWandId()
    {
        return m_Instance.m_playerWandId;
    }

    static public void SpawnAtCheckPoint(GameObject spawnObject)
    {
        if (m_Instance.m_checkpointSceneIndex < 0)
        {
            Debug.Log("Lack of registered check points.");
            return;
        }

        spawnObject.transform.position = m_Instance.m_spawnPosition;
    }

    static public void Respawn()
    {
        if (m_Instance.m_checkpointSceneIndex < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        SceneManager.LoadScene(m_Instance.m_checkpointSceneIndex, LoadSceneMode.Single);
    }
}
