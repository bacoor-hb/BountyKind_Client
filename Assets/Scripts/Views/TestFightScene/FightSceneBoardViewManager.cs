using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneBoardViewManager : MonoBehaviour
{
    public List<GameObject> spawnPosPets;
    public List<GameObject> spawnPosEnemies;
    public List<GameObject> characterObjs;
    // Start is called before the first frame update
    public void Init()
    {
        characterObjs = new();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject RenderUnitToBoard(GameObject _obj, int index, string type)
    {
        GameObject obj = null;
        if (type == "pet")
        {
            GameObject targetSpawn = spawnPosPets[index];
            obj = Instantiate(_obj, targetSpawn.transform.position, targetSpawn.transform.rotation, targetSpawn.transform);
        }
        else if (type == "enemy")
        {
            GameObject targetSpawn = spawnPosEnemies[index];
            obj = Instantiate(_obj, targetSpawn.transform.position, targetSpawn.transform.rotation, targetSpawn.transform);
        }
        characterObjs.Add(obj);
        return obj;
    }

    public void ClearCharacterOnboard(GameObject characterObj)
    {
        Destroy(characterObj);
    }

    public void DestroyAllCharacterObjs()
    {
        Debug.Log("Destroy Objs");
        if (characterObjs != null && characterObjs.Count > 0)
        {
            characterObjs.ForEach(characterObj =>
            {
                Destroy(characterObj);
            });
            characterObjs = new List<GameObject>();
        }
    }
}
