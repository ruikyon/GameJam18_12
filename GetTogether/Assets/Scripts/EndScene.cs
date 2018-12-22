using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScene : MonoBehaviour
{
    [SerializeField]
    private GameObject clear, over;
    [SerializeField]
    TextMeshProUGUI counter;
    [SerializeField]
    Animator animator;

	// Use this for initialization
	void Start () {
        if (GameManager.fail) over.SetActive(true);
        else
        {
            clear.SetActive(true);
            counter.gameObject.SetActive(true);
            counter.text += GameManager.trapCount.ToString();
        }
        animator.SetBool("Fail", GameManager.fail);
	}

    private void Update()
    {
        if (Input.GetButtonDown("Back"))
            SceneManager.LoadScene("StartScene");
    }
}
