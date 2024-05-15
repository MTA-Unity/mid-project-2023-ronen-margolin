using UnityEngine;

public class SceneDto : MonoBehaviour
{
    public int blocksInCol{get; private set;}
    public int blocksInRow{get; private set;}

    public static SceneDto instance{get; private set;} = null;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void startGame(int blocksInCol, 
                        int blocksInRow)
    {
        this.blocksInCol = blocksInCol;
        this.blocksInRow = blocksInRow;
    }
    
}
