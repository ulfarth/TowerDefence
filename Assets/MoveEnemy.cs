using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    [HideInInspector]
    public GameObject[] waypoints;

    public float speed = 1.0f;
    public bool flipHorizontaly = false;
    public bool flipVerically = false;

    private int _currentWaypoint = 0;
    private float _lastWayPointSwitchTime;
    GameObject _sprite;
    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector2.Distance(
            gameObject.transform.position,
            waypoints[_currentWaypoint + 1].transform.position);
        for (int i = _currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPoint = waypoints[i].transform.position;
            Vector3 endPoint = waypoints[i + 1].transform.position;
            distance += Vector2.Distance(startPoint,endPoint);
        }
        return distance;
    }

    private void RotateToMoveDirection()
    {
        Vector3 newStartPoint = waypoints[_currentWaypoint].transform.position;
        Vector3 newEndPoint = waypoints[_currentWaypoint + 1].transform.position;
        Vector3 newDirection = (newEndPoint - newStartPoint);

        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y,x) * 180 / Mathf.PI;
               
        _sprite.transform.rotation = Quaternion.AngleAxis(rotationAngle,Vector3.forward);
    }

    // Start is called before the first frame update
    void Start()
    {
        _sprite = gameObject.transform.Find("Sprite").gameObject;
        if (flipHorizontaly)
        {
            Vector3 theScale = _sprite.transform.localScale;
            theScale.x *= -1;
            _sprite.transform.localScale = theScale;
           //_sprite.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (flipVerically)
        {
           // _sprite.transform.localRotation = Quaternion.Euler(180, 0, 0);
        }
        
        Debug.Log("waypoints: " + waypoints.Length);
        _lastWayPointSwitchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPoint = waypoints[_currentWaypoint].transform.position;
        Vector3 endPoint = waypoints[_currentWaypoint + 1].transform.position;

        float pathLength = Vector3.Distance(startPoint,endPoint);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - _lastWayPointSwitchTime;

        gameObject.transform.position = Vector2.Lerp(startPoint,endPoint,currentTimeOnPath / totalTimeForPath);

        if (gameObject.transform.position.Equals(endPoint))
        {
            if(_currentWaypoint < waypoints.Length - 2)
            {
                _currentWaypoint++;
                _lastWayPointSwitchTime = Time.time;
                RotateToMoveDirection();
            }
            else
            {
                Destroy(gameObject);


                //TODO PLAY SOUND, DEDUCT HEALTH FROM PLAYER
                //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                //AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                //GameManagerBehavior gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
                //gameManager.Health -= 1;
            }
        }
    }
}
