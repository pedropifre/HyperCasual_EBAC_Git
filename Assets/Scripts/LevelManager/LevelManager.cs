using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    
    public List<LevelPieceBasedSetup> levelPieceBasedSetups;
    
    public float timeBetweenPieces = .3f;
    public GameObject player;
    public TextMeshProUGUI textLevel;
    
    
    public int _index;
    private GameObject _currentLevel;

    [SerializeField] private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBasedSetup _curSetup;
    public PlayerController playerController;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleBetweenPieces = .2f;
    public Ease ease = Ease.OutBack;
    




    private void Start()
    {
        //SpawnNextLevel();
        //StartCoroutine(CreateLevelPiecesCoroutine());
        CreateLevelPieces();
        ChangeText();
        PlayerController.Instance.ResetSpeed();
       

    }

    public void ChangeText()
    {
        var level = _index + 1;
        textLevel.text = "Level "+level.ToString()+" - 3";
    }
    
    private void SpawnNextLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            
            if (_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }
        _currentLevel = Instantiate(levels[_index], container);
        var transforms = _currentLevel.GetComponentsInChildren<Transform>();
        foreach (Transform transform in transforms)
        {
            if (transform.tag == "Respawn")
            {
                player.transform.position = transform.position;
            }
        }
        _index++;
        PlayerController.Instance.ResetSpeed();
    }

    public void NextLevel()
    {
        if (_index >= levels.Count)
        {
            //Fim de jogo
            Debug.Log("Fim de jogo");
        }
        else
        {
            Debug.Log("Pr?xima fase");
            SpawnNextLevel();
        }
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region

    public void CreateLevelPieces()
    {
        
        //aqui
        if (_index >= levels.Count)
        {
            CoinsCount.Instance.ChangeTextZerarCoins();
        }
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

        ColorManager.Instance.ChangeColorByType(_curSetup.artType);
        player.transform.position = Vector3.zero;
        ChangeText();
        StartCoroutine(ScalePiecesByTime());
        PlayerController.Instance.ScalePlayer();


    }

    IEnumerator ScalePiecesByTime()
    {
        foreach (var p in _spawnedPieces)
        {
            p.transform.localScale = Vector3.zero;
        }

        yield return null;

        for (var i = 0; i < _spawnedPieces.Count; i++)
        {
            _spawnedPieces[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleBetweenPieces);
            //Debug.Log("b");
        }
        //Debug.Log("a");
        CoinsAnimationManager.Instance.StartAnimations();
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
