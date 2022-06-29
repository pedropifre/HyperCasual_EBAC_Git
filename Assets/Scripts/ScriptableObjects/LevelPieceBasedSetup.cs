using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPieceBasedSetup : ScriptableObject
{

    public ArtManager.ArtType artType;
    [Header("Pieces")]
    public List<LevelPieceBase> LevelPiecesStart;
    public List<LevelPieceBase> LevelPieces;
    public List<LevelPieceBase> LevelPiecesEnd;

    public int piecesNumberStart = 3;
    public int piecesNumber = 5;
    public int piecesNumberEnd = 1;
}
