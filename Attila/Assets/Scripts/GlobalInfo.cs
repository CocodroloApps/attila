using UnityEngine;
using System.Collections.Generic;

public class GlobalInfo : MonoBehaviour
{
    //General Config
    public static string configFile = "AttilaCfg";    
    public static bool gameFirstTime;   //Fecha instalación
    public static bool playFirstTime;   //Fecha primera partida
    public static int sessionsCount;    //Numero de sesiones de juego
    public static int stagesCount;      //Numero de Etapas jugadas
    public static string language;      //Lenguaje
    public static bool soundPlay = true;

    //Game
    public static int maxStageCompleted;//Máxima etapa conseguida   
    public static int maxStagesGame;    //Etapas totales del juego
    public static int score;            
    public static int water;            
    public static int food;
    public static int troops;
    public static int weapons;
    public static int gold;
    public static bool isPlaying;
    public static bool isPlayerMoving;
    public static bool isOldCellDestroyed;  //Se ha destruido ya la celda de origen?
    public static bool isEventAvaliable;
    public static bool isShowingInfo;

    //Tutorials
    public static bool showTutorial;
    public static bool showTutorial1;
    public static bool showTutorial2;
    public static bool showTutorial3;
    public static bool showTutorial4;
    public static bool showTutorial5;
    public static bool showTutorial6;
    public static bool showTutorial7;

    //Actual stage
    public static int actualStage;      //Etapa que estamos jugando actualmente
    public static int stageVersion;
    public static string stageName;
    public static int objectivesNum;
    public static int movementsNum;
    public static int finalNum;
    public static int playerPos;
    public static List<StageCell> gridStage = new List<StageCell>();

    //Editor
    public static bool isEditing;
    public static bool isPainting;
    public static bool isModifying;
    public static int paintCellType;
    public static int editingCell;
}
