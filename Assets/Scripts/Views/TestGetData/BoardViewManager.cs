using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewManager : MonoBehaviour
{
    [SerializeField]
    private GameObject board;
    public List<GameObject> boardSquares;
    [SerializeField]
    private Dictionary<string, GameObject> cloneCharacters;
    // Start is called before the first frame update
    void Start()
    {
        cloneCharacters = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RenderCharacterModelToSquare(GameObject characterPrefab, int position, string characterId)
    {
        Transform targetSquare = boardSquares[position].transform;
        cloneCharacters.Add(characterId, Instantiate(characterPrefab, targetSquare));
    }
    public void Clear(string characterId)
    {
        Destroy(cloneCharacters[characterId]);
        cloneCharacters.Remove(characterId);
    }

    public void Move(string characterId, Transform parentTransform)
    {
        cloneCharacters[characterId].GetComponent<Transform>().SetParent(parentTransform, false);
    }

    public void RotateCamera(bool isClockWise)
    {
        if (isClockWise)
        {
            board.transform.Rotate(0f, 45.0f, 0.0f, Space.World);
        }
        else
        {
            board.transform.Rotate(0f, -45.0f, 0.0f, Space.World);
        }
    }
}
