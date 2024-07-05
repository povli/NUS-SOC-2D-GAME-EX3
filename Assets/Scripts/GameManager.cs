using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager sTheGlobalBehavior = null;

    public Text mGameStateEcho = null;  // Defined in UnityEngine.UI
    public HeroBehavior mHero = null;
    private EnemySpawnSystem mEnemySystem = null;

    private CameraSupport mMainCamera;

    public static bool mSeqencingMode = true; // seqencing from A-Z if true

    private void Start()
    {
        GameManager.sTheGlobalBehavior = this;  // Singleton pattern
        Debug.Assert(mHero != null);

        mMainCamera = Camera.main.GetComponent<CameraSupport>();
        Debug.Assert(mMainCamera != null);

        Bounds b = mMainCamera.GetWorldBound();
        mEnemySystem = new EnemySpawnSystem(b.min, b.max);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            mSeqencingMode = !mSeqencingMode;
            // Debug.Log(mSeqencingMode);
            // Debug.Log(111);
        }
        EchoGameState(); // always do this

        if (Input.GetKey(KeyCode.Q))
            Application.Quit();
    }


    #region Bound Support
    public CameraSupport.WorldBoundStatus CollideWorldBound(Bounds b) { return mMainCamera.CollideWorldBound(b); }
    #endregion 

    private void EchoGameState()
    {
        mGameStateEcho.text = mHero.GetHeroState() + "  " + mEnemySystem.GetEnemyState() + " " + EnemyBehavior.GetControlState();
    }
}
