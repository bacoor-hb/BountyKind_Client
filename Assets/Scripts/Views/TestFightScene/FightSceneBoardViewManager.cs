using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneBoardViewManager : MonoBehaviour
{
    public List<GameObject> spawnPosPets;
    public List<GameObject> spawnPosEnemies;
    // Start is called before the first frame update
    void Start()
    {

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
            obj = Instantiate(_obj, targetSpawn.transform.position, Quaternion.identity, targetSpawn.transform);
        }
        else if (type == "enemy")
        {
            GameObject targetSpawn = spawnPosEnemies[index];
            obj = Instantiate(_obj, targetSpawn.transform.position, Quaternion.identity, targetSpawn.transform);
        }
        return obj;
    }

    public void ClearCharacterOnboard(GameObject characterObj)
    {
        Destroy(characterObj);
    }
}
