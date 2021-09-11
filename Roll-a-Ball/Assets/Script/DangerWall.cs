using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DangerWall : MonoBehaviour
{
	void OnCollisionEnter (Collision hit)
	{
		if (hit.gameObject.CompareTag ("Player"))
        {
			int sceneIndex = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(sceneIndex);
		}
	}
}
