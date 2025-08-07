using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    
    public void StartButtonPress()
    {
        SceneManager.LoadScene("Platformer Section");

    }
}
