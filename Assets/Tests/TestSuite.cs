using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Tests.Utils;


public class TestSuite {
    readonly private string levelName = "Game";

    private Player getPlayer() {
        return MonoBehaviour.FindAnyObjectByType<Player>();
    }

    [SetUp]
    public void Setup() {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator PlayerCanMove() {
        yield return Utils.WaitForLevelLoad(levelName);
        Player _player = getPlayer();
    
        float startPos = _player.transform.position.x;
        _player.Move(.05f);
        yield return new WaitForSeconds(2);
        float endPos = _player.transform.position.x;
        Assert.AreNotEqual(startPos, endPos);
    }

    [UnityTest]
    public IEnumerator PlayerCanJump() {
        yield return Utils.WaitForLevelLoad(levelName);
        Player _player = getPlayer();

        float startPos = _player.transform.position.y;
        _player.TryJump();
        yield return new WaitForSeconds(.5f);
        float endPos = _player.transform.position.y;
        Assert.AreNotEqual(startPos, endPos);
    }

    [UnityTest]
    public IEnumerator PlayerCanTakeKey() {
        yield return Utils.WaitForLevelLoad(levelName);
        Player _player = getPlayer();
        CollectableItem _key = MonoBehaviour.FindFirstObjectByType<CollectableItem>();

        int startCount = Managers.Inventory.GetItemCount("key");
        Assert.AreEqual(0, startCount);

        _player.transform.position = _key.transform.position;
        yield return new WaitForSeconds(1);

        int endCount = Managers.Inventory.GetItemCount("key");
        Assert.AreNotEqual(startCount, endCount);
    }

    [UnityTest]
    public IEnumerator PlayerCanOpenDoor() {
        yield return Utils.WaitForLevelLoad(levelName);
        Player _player = getPlayer();
        CollectableItem _key = MonoBehaviour.FindFirstObjectByType<CollectableItem>();
        DoorTrigger _door = MonoBehaviour.FindAnyObjectByType<DoorTrigger>();

        _player.transform.position = _key.transform.position;
        yield return new WaitForSeconds(1);

        _player.transform.position = _door.transform.position;
        yield return new WaitForSeconds(1);

        DoorTrigger newDoor = MonoBehaviour.FindAnyObjectByType<DoorTrigger>();
        Assert.IsNull(newDoor);
    }
}
