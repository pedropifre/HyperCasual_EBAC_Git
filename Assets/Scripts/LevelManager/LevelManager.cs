using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    
    public List<LevelPieceBasedSetup> levelPieceBasedSetups;
    
    public float timeBetweenPieces = .3f;
    
    
    [SerializeField] private int _index;
    private GameObject _currentLevel;

    [SerializeField] private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBasedSetup _curSetup;



    private void Awake()
    {
        //SpawnNextLevel();
        //StartCoroutine(CreateLevelPiecesCoroutine());
        CreateLevelPieces();
    }

    private void SpawnNextLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;
            if (_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }
        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region

    private void CreateLevelPieces()
    {
        
        CleanSpawnedPieces();

        if (_curSetup != null)
        {
            _index++;

            if (_index >= levelPieceBasedSetups.Count)
            {
                ResetLevelIndex();
            }
        }

        _curSetup = levelPieceBasedSetups[_index];
        
        
        
        for (int i = 0; i < _curSetup.piecesNumberStart; i++)
        {
            CreateLevelPiece(_curSetup.LevelPiecesStart);
        }
        
        for (int i = 0; i < _curSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_curSetup.LevelPieces);
        }
        
        for (int i = 0; i < _curSetup.piecesNumberEnd; i++)
        {
            CreateLevelPiece(_curSetup.LevelPiecesEnd);
        }
    }

    private void CreateLevelPiece(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if (_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }
        else
        {
            spawnedPiece.transform.position = Vector3.zero;
        }


        foreach(var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(_curSetup.artType).gameObject);
        }
        _spawnedPieces.Add(spawnedPiece);
    }

    private void CleanSpawnedPieces()
    {
        for (int i = _spawnedPieces.Count - 1; i >=0  ; i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }

        _spawnedPieces.Clear();
    }

    IEnumerator CreateLevelPiecesCoroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();

        for (int i = 0; i < _curSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_curSetup.LevelPieces);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            CreateLevelPieces();
        }
    }
}
