using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Simple Singleton
    public static GameManager instance;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        #region Simple Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
