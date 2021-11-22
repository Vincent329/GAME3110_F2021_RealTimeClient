using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    float durationUntilNextBalloon;
    Sprite circleTexture;


    LinkedList<GameObject> listOfBalloons;
    void Start()
    {
        listOfBalloons = new LinkedList<GameObject>();
        NetworkedClientProcessing.SetGameLogic(this);
    }
    void Update()
    {
        // we're instead taking the logic from the server
        //durationUntilNextBalloon -= Time.deltaTime;

        //if(durationUntilNextBalloon < 0)
        //{
        //    durationUntilNextBalloon = 1f;

        //    float screenPositionXPercent = Random.Range(0.0f, 1.0f);
        //    float screenPositionYPercent = Random.Range(0.0f, 1.0f);
        //    Vector2 screenPosition = new Vector2(screenPositionXPercent * (float)Screen.width, screenPositionYPercent * (float)Screen.height);
        //    SpawnNewBalloon(screenPosition);
        //}
    }
    public void SpawnNewBalloon(Vector2 screenPosition, int balloonID)
    {
        if(circleTexture == null)
            circleTexture = Resources.Load<Sprite>("Circle");

        GameObject balloon = new GameObject("Balloon");

        balloon.AddComponent<SpriteRenderer>();
        balloon.GetComponent<SpriteRenderer>().sprite = circleTexture;
        balloon.AddComponent<CircleClick>();
        balloon.AddComponent<CircleCollider2D>();

        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));
        pos.z = 0;
        balloon.transform.position = pos;

        balloon.GetComponent<CircleClick>().balloonID = balloonID;
        listOfBalloons.AddLast(balloon);
        //go.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -Camera.main.transform.position.z));
    }

    public void DestroyBalloon(int destroyBalloonID)
    {
        GameObject destroyMe = null;
        foreach(GameObject b in listOfBalloons)
        {
           if (b.GetComponent<CircleClick>().balloonID == destroyBalloonID)
            {
                destroyMe = b;
            }
        }

        if (destroyMe != null)
        {
            Destroy(destroyMe);
            listOfBalloons.Remove(destroyMe);
        }
    }
}

