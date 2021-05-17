using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceScript : MonoBehaviour
{
    /// <summary>
    /// Save Variables Between Scene
    /// </summary>

    private int indexGame = 0;

    private int indexLanguage = 0;

    private int indexLevelDifficulty = 0;

    /// <summary>
    /// Index of the chosen language
    /// </summary>
    public int LanguageIndex
    {
        get { return indexLanguage; }
        set { indexLanguage = value; }
    }


    /// <summary>
    /// Index of the chosen difficulty ; 0 - Basic , 1 - Expert
    /// </summary>
    public int DifficultyLevelIndex
    {
        get { return indexLevelDifficulty; }
        set { indexLevelDifficulty = value; }
    }

    /// <summary>
    /// Index of the Game Selected
    /// </summary>
    public int GameIndex
    {
        get { return indexGame; }
        set { indexGame = value; }
    }

}
