using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour
{
	public void Load(int id) => SceneManager.LoadScene(id);
}