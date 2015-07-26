using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
    public GameObject AButton;
    public GameObject RotatingCircle;

    protected AsyncOperation _async;
    void Update()
    {
        bool condition = InputManager.Instance.GetAButton(PlayerIndexEnum.PlayerOne) || InputManager.Instance.GetAButton(PlayerIndexEnum.PlayerTwo) || InputManager.Instance.GetAButton(PlayerIndexEnum.PlayerThree) || InputManager.Instance.GetAButton(PlayerIndexEnum.PlayerFour);
        if(condition)
        {
            AButton.SetActive(false);
            RotatingCircle.SetActive(true);
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        _async = Application.LoadLevelAsync("Main");
        _async.allowSceneActivation = false;
        yield return new WaitForSeconds(2.5f);
        _async.allowSceneActivation = true;
    }
}
