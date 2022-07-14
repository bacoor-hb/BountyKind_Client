using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using CombatProcess;
using Newtonsoft.Json;

public class TestDLLController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetData());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetData()
    {
        Debug.Log("asd");
        Startup battleStartUp = new Startup();
        CombatProcess.InputType myInput = new();
        List<CombatProcess.FormationCharacters> userCharacters = new List<CombatProcess.FormationCharacters>();
        CombatProcess.FormationCharacters character1 = new();
        character1._id = "1";
        character1.baseKey = "default_character_3";
        character1.position = 0;
        character1.hp = 100;
        character1.atk = 22;
        character1.speed = 5;
        character1.def = 23;
        character1.level = 7;
        userCharacters.Add(character1);
        CombatProcess.FormationCharacters[] userCharactersArr = userCharacters.ToArray();

        List<CombatProcess.FormationCharacters> opponentCharacters = new List<CombatProcess.FormationCharacters>();
        CombatProcess.FormationCharacters character2 = new();
        character2._id = "2";
        character2.baseKey = "default_character_3";
        character2.position = 0;
        character2.hp = 100;
        character2.atk = 22;
        character2.speed = 5;
        character2.def = 23;
        character2.level = 7;
        opponentCharacters.Add(character2);
        CombatProcess.FormationCharacters[] opponentCharactersArr = opponentCharacters.ToArray();

        myInput._userCharacters = userCharactersArr;
        myInput._opponentCharacters = opponentCharactersArr;

        Task<object> task = battleStartUp.Invoke(JsonConvert.SerializeObject(myInput)); ;
        yield return new WaitUntil(() => task.IsCompleted);
        try
        {
            object result = task.Result;
            Debug.Log(result);
            Debug.Log(result);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
        }
    }
}
