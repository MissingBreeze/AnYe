using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

	void Start ()
    {
        transform.GetComponent<Button>().onClick.AddListener(TryAgainBtnClick);
	}

    private void TryAgainBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(transform);
    }

    void Update ()
    {
		
	}
}
