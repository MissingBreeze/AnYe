using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public string sceneName;
    

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
    }
}
