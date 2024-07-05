using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public partial class EnemyBehavior : MonoBehaviour {

    // All instances of Enemy shares this one WayPoint and EnemySystem
    static private EnemySpawnSystem sEnemySystem = null;
    static public void InitializeEnemySystem(EnemySpawnSystem s) { sEnemySystem = s; }

    private int mNumHit = 0;
    private const int kHitsToDestroy = 4;
    private const float kEnemyEnergyLost = 0.8f;

    private const float kEnemyMovingSpeed = 20f; // enemy speed

    private const float kEnemyRotatingSpeed = 0.03f; // enemy ratate speed

    private const int mTotalTerminals = 6;

    private static Vector3[] mTerminalPositions = new Vector3[6]; // positiions of terminal A-F

    private int current_destination = 0; // current terminal going to

    private const float kVeryClose = 25f;     //distance judge

    private void CheckTargetPosition()      //distance judge turn
    {
        // Access the GameManager
        float dist = Vector3.Distance(mTerminalPositions[current_destination], transform.localPosition);
        if (dist < kVeryClose)
            setDst();
    }
    private void Awake() {
        // initialize terminals positions
    }

    private void Start() {
        Debug.Assert(mTerminalPositions != null);
    }
    private void Update() {
        AfterDestroyed();
        MoveToNextTerminal();
        CheckTargetPosition();
        // Debug.Log(mTerminalPositions[current_destination]);
    }

    public static string GetControlState()
    {
        if (GameManager.mSeqencingMode)
            return "Enemy:" + "Sequence";
        else
            return "Enemy:" + "Random";
    }

    #region Trigger into chase or die
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Emeny OnTriggerEnter");
        TriggerCheck(collision.gameObject);
    }

    private void TriggerCheck(GameObject g)
    {
        if (g.name == "Hero")
        {
            ThisEnemyIsHit();

        } else if (g.name == "Egg(Clone)")
        {
            mNumHit++;
            if (mNumHit < kHitsToDestroy)
            {
                Color c = GetComponent<Renderer>().material.color;
                c.a = c.a * kEnemyEnergyLost;
                GetComponent<Renderer>().material.color = c;
            } else
            {
                ThisEnemyIsHit();
            }
        }
    }

    private void ThisEnemyIsHit()
    {
        sEnemySystem.OneEnemyDestroyed();
        Destroy(gameObject);
    }
    #endregion

    void MoveToNextTerminal(){
        // move and rotate(if needed) towards the next destination
        PointAtPosition(mTerminalPositions[current_destination], kEnemyRotatingSpeed * Time.smoothDeltaTime);
        Vector3 pos = transform.localPosition;

        pos += ((kEnemyMovingSpeed * Time.smoothDeltaTime) * transform.up);

        transform.localPosition = pos;
    }

    private void setDst(){
        // change the destination
        if(GameManager.mSeqencingMode){
            current_destination += 1;
            current_destination %= mTotalTerminals;
        }else{
            current_destination = Random.Range(0, mTotalTerminals);
        }
        // Debug.Log("set dst" + current_destination);
    }

    private void AfterDestroyed()
    {
        if (Terminbehavior.BeenDestroyed == 1 && Terminbehavior.trans == current_destination) 
        {
            Debug.Log("Turn");
            setDst();
        }
    }

    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.localPosition;
        if(AreVectorsCollinear(p, v)){
            return;
        }
        // use to turn to the direction of target gradually
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

    bool AreVectorsCollinear(Vector3 upVector, Vector3 orientation)
    {
        if ((upVector.x == 0f && upVector.y == 0f) || (orientation.x == 0f && orientation.y == 0f))
        {
            return false;
        }

        float ratioX = upVector.x / orientation.x;
        float ratioY = upVector.y / orientation.y;

        return Mathf.Approximately(ratioX, ratioY);
    }

    public static void updateTerminalPosition(int index, Vector3 new_position){
        // update a terminal's position
        // Debug.Log(mTerminalPositions);
        mTerminalPositions[index] = new_position;
        // Debug.Log("set pos");
    }
}
