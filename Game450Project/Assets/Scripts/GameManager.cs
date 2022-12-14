using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameStarted;
    public static bool playerIsGrounded;
    public static bool playerCaptured;
    public static float escapeRate;
    public static float escapeGoal;
    public static float escapeTimer;
    public static int numberofCaptures;
    public static int collectibleCount;
    public static float playerSpeed;
    public static float gravityScale;
    public static bool playerRespawning;
    public static int localnumberOfFruit;
}
