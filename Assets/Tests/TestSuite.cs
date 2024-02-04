using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Tests.Utils;


public class TestSuite {
    readonly private string levelName = "Game";


    [SetUp]
    public void Setup() {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator PlayerCanMove() {
        yield return Utils.WaitForLevelLoad(levelName);
        Player _player = MonoBehaviour.FindObjectOfType<Player>();

        float startPos = _player.transform.position.x;
        _player.Move(.05f);
        yield return new WaitForSeconds(2);
        float endPos = _player.transform.position.x;
        Assert.AreNotEqual(startPos, endPos);
    }

    [UnityTest]
    public IEnumerator PlayerCanJump() {
        yield return Utils.WaitForLevelLoad(levelName);
        Player _player = MonoBehaviour.FindObjectOfType<Player>();

        float startPos = _player.transform.position.y;
        _player.TryJump();
        yield return new WaitForSeconds(.5f);
        float endPos = _player.transform.position.y;
        Assert.AreNotEqual(startPos, endPos);
    }


}
