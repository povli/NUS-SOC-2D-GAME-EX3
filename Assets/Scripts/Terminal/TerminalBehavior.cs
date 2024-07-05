using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminbehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int Uid;
    public int TerminTolnum = 6;
    public int Life = 100;
    public Camera mCamera = null;
    private bool Hcheck = true;
    public  static int BeenDestroyed = 0;
    private bool Xcheck = true;
    private bool Ycheck = true;
    public static int trans;

    void Start()
    {
        EnemyBehavior.updateTerminalPosition(Uid, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Life <= 0)
        {
            trans = Uid;
            Debug.Log("die");
            BeenDestroyed = 1;
            Vector3 p = transform.localPosition;
            float tmpnumx = Random.Range(0, 10);
            float tmpnumy = Random.Range(0, 10);
            Vector3 viewportPosition = new Vector3(1f, 1f, mCamera.nearClipPlane);
            Vector3 poiont = Camera.main.ViewportToWorldPoint(viewportPosition);
            Vector3 mSpawnRegionMin, mSpawnRegionMax;
            mSpawnRegionMin.x = -poiont.x * 0.9f;
            mSpawnRegionMin.y = -poiont.y * 0.9f;
            mSpawnRegionMax.x = poiont.x * 0.9f;
            mSpawnRegionMax.y = poiont.y * 0.9f;
            if ((p.x + 15) > mSpawnRegionMax.x /*&& (p.y + 15) > mSpawnRegionMax.y*/)
            {
                p.x -= 15;
                //p.y -= 15;
                Xcheck = !Xcheck;

            }
            else if (/*(p.x + 15) > mSpawnRegionMax.x *//*&& */(p.y - 15) < mSpawnRegionMin.y)
            {
                //p.x -= 15;
                p.y += 15;
                Ycheck = !Ycheck;
            }
            else if (/*(p.x - 15) < mSpawnRegionMin.x &&*/ (p.y + 15) > mSpawnRegionMax.y)
            {
                //p.x += 15;
                p.y -= 15;
                Ycheck = !Ycheck;
            }
            else if ((p.x - 15) < mSpawnRegionMin.x /*(p.y - 15) < mSpawnRegionMin.y*/)
            {
                p.x += 15;
                Xcheck = !Xcheck;
                //p.y += 15;
            }
            else
            {
                if (tmpnumx < 5&&Xcheck)
                {
                    p.x += 15;
                }
                else if(Xcheck)
                {
                    p.x -= 15;
                }
                if (tmpnumy < 5&&Ycheck)
                {
                    p.y += 15;
                }
                else if(Ycheck)
                {
                    p.y -= 15;
                }
            }
            Xcheck = true;
            Ycheck = true;
            transform.localPosition = p;

            EnemyBehavior.updateTerminalPosition(Uid, transform.localPosition);
            Life = 100;
            Color color = GetComponent<Renderer>().material.color;
            color.a /= 0.75f;
            color.a /= 0.75f;
            color.a /= 0.75f;
            color.a /= 0.75f;
            GetComponent<Renderer>().material.color = color;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Hcheck = !Hcheck;
        }
        /*int tmpLife = Life;
        Color tmpcolor = GetComponent<Renderer>().material.color;*/
        if (!Hcheck)
        {
            /*Color color = GetComponent<Renderer>().material.color;
            color.a /= 0f;
            GetComponent<Renderer>().material.color = color;
            Life = 10000000;*/
            gameObject.SetActive(false);
        }
        else
        {
            //gameObject.active= true;
            /*Life = tmpLife;
            GetComponent<Renderer>().material.color = tmpcolor;*/
            gameObject.SetActive(Hcheck);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Respawn")
        {

            Color color = GetComponent<Renderer>().material.color;
            
            color.a *= 0.75f;
            
            GetComponent<Renderer>().material.color = color;
            
            Life -= 25;
        }

    }
}
