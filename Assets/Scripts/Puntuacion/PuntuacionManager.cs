using UnityEngine;
using UnityEngine.UI;

/// <summary>Manager for SumScore accessible from inspector</summary>
/// <remarks>
/// Attach to game object in scene. 
/// This is a singleton so only one instance can be active at a time.
/// </remarks>
public class PuntuacionManager : MonoBehaviour {

    public static PuntuacionManager instance = null;  // Static instance for singleton

    public int initialScore = 0;
    public bool storeHighScore = true, allowNegative = true;
    public Text puntuacion; // Text field displaying current score
    public Text fallos; // Text field displaying high score

    void Awake() {
        // Ensure only one instance is running
        if (instance == null)
            instance = this; // Set instance to this object
        else
            Destroy(gameObject); // Kill yo self
        // Make sure the linked references didn't go missing
        if (puntuacion == null)
            Debug.LogError("Missing reference to 'field' on <b>SumScoreManager</b> component");
        if (storeHighScore && fallos == null)
            Debug.LogError("Missing reference to 'highScoreField' on <b>SumScoreManager</b> component");
    }

    void Start() {
        Puntuacion.Reset(); // Ensure score is 0 when object loads
        if (initialScore != 0)
            Puntuacion.Add(initialScore);  // Set initial score

        Updated(); // Set initial score in UI
    }

    /// <summary>Notify this manager of a change in score</summary>
    public void Updated () {
        puntuacion.text = Puntuacion.Score.ToString("0"); // Post new score to text field
        fallos.text = Puntuacion.Fallos.ToString("0");
    }

    


}
