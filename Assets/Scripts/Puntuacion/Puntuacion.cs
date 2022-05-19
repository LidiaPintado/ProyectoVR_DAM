using UnityEngine;

/// <summary>
/// Simple score manager. Requires SumScoreManager attached to game object in scene.
/// </summary>
public class Puntuacion {

    public static int Score { get; protected set; }
    public static int Fallos { get; protected set; }

    private static PuntuacionManager mgr; // Easy reference to manager instance

    // Private constructor to ensure only one copy exists
    private Puntuacion () { }
	
    /// <summary>Adds points to total score</summary>
    /// <remarks>
    /// You can also use a negative number as a shortcut to the Subtract method
    /// </remarks>
    /// <param name="pointsToAdd">Number of points to add</param>
    public static void Add (int pointsToAdd) {
        Debug.Log(pointsToAdd + " points " + ((pointsToAdd > 0) ? "added" : "removed"));
        Score += pointsToAdd; // Add points to current score
        if (MgrSet()) {
            // Make sure we don't go negative unless we're supposed to
            if (Score < 0 && !mgr.allowNegative)
                Score = 0; // Reset score to 0
            mgr.Updated(); // Let the manager know we've changed the score
        }
    }

    public static void FailAdd(int pointsToAdd)
    {
        Debug.Log(pointsToAdd + " points " + ((pointsToAdd > 0) ? "added" : "removed"));
        Fallos += pointsToAdd; // Add points to current score
        if (MgrSet())
        {
            // Make sure we don't go negative unless we're supposed to
            if (Fallos < 0 && !mgr.allowNegative)
                Fallos = 0; // Reset score to 0
            mgr.Updated(); // Let the manager know we've changed the score
        }
    }

    public static void Exito() => Add(1);
    public static void Fallo() => FailAdd(-1);

    /// <summary>Removes points from total score</summary>
    /// <param name="pointsToSubtract">Number of points to remove</param>
    public static void Subtract (int pointsToSubtract) {
        Add(-pointsToSubtract);
    }

    /// <summary>Sets Score to 0 and updates manager</summary>
    public static void Reset () {
        Debug.Log("Reset score");
        Score = 0;
        if(MgrSet()) {
            mgr.Updated();
        }
    }

    /// <summary>Checks and sets references needed for the script</summary>
    /// <returns>True if successful, false if failed</returns>
    static bool MgrSet () {
        if (mgr == null) {
            mgr = PuntuacionManager.instance; // Set instance reference
            if (mgr == null) {
                // Throw error message if we can't link
                Debug.LogError("<b>SumScoreManager.instance</b> cannot be found. Make sure object is active in inspector.");
                return false;
            }
        }
        return true;
    }

}
