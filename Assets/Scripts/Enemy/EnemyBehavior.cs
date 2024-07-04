using UnityEngine;
using System.Collections;

public partial class EnemyBehavior : MonoBehaviour {

    // All instances of Enemy shares this one WayPoint and EnemySystem
    static private EnemySpawnSystem sEnemySystem = null;
    static public void InitializeEnemySystem(EnemySpawnSystem s) { sEnemySystem = s; }

    private int mNumHit = 0;
    private const int kHitsToDestroy = 4;
    private const float kEnemyEnergyLost = 0.8f;

    private const float kEnemyMovingSpeed = 20f; // enemy speed

    private const float kEnemyRotatingSpeed = 0.03f; // enemy ratate speed

    public static bool mSeqencingMode = true; // seqencing from A-Z if true

    private const int mTotalTerminals = 6;

    private static Vector3[] mTerminalPositions; // positiions of terminal A-F

    private int current_destination = 0; // current terminal going to

    private void Awake() {
        // initialize terminals positions
    }

    private void Start() {
        Debug.Assert(mTerminalPositions != null);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.J)){
            mSeqencingMode = !mSeqencingMode;
        }
        MoveToNextTerminal();
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
        if(mSeqencingMode){
            current_destination += 1;
            current_destination %= mTotalTerminals;
        }else{
            current_destination = Random.Range(0, mTotalTerminals);
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
        if ((upVector.x & upVector.y) || (orientation.x & orientation.y))
        {
            return false;
        }

        float ratioX = upVector.x / orientation.x;
        float ratioY = upVector.y / orientation.y;

        return Mathf.Approximately(ratioX, ratioY);
    }

    public static void updateTerminalPosition(int index, Vector3 new_position){
        // update a terminal's position
        mTerminalPositions[index] = new_position;
    }
}
