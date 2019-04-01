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

    //Actual stage
    public static int actualStage;      //Etapa que estamos jugando actualmente
    public static int stageVersion;
    public static List<StageCell> gridStage = new List<StageCell>();
}
