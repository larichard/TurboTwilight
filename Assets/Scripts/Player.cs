using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public AudioClip deathClip;

    public GameManager gameMgr;
    public GameObject player_pink;
    public GameObject player_cyan;
    public Sprite sprite;

    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public TextMeshProUGUI score;

    public int lives;
    private Vector3 pink_respawn;
    private Vector3 cyan_respawn;
    public float speed = 5f;
    public GameObject wallPrefab;

    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;

    Collider2D wall;

    Vector2 lastWallEnd;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        spawnWall();
        player_cyan = GameObject.Find("player_cyan");
        player_pink = GameObject.Find("player_pink");
        pink_respawn = player_pink.transform.position;
        cyan_respawn = player_cyan.transform.position;

        canMoveUp = canMoveDown = canMoveLeft = canMoveRight = true;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rb = GetComponent<Rigidbody2D>().velocity;
        if (rb.x > 0)
        {
            canMoveLeft = false;
        }
        else if (rb.x < 0)
        {
            canMoveRight = false;
        }
        else if (rb.y > 0)
        {
            canMoveDown = false;
        }
        else if (rb.y < 0)
        {
            canMoveUp = false;
        }

        if (Input.GetKeyDown(upKey) && canMoveUp)
        {
            GetComponent<Rigidbody2D>().velocity = (Vector2.up * speed);
            spawnWall();
        }
        else if (Input.GetKeyDown(downKey) && canMoveDown)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
            spawnWall();
        }
        else if (Input.GetKeyDown(leftKey) && canMoveLeft)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
            spawnWall();
        }
        else if (Input.GetKeyDown(rightKey) && canMoveRight)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            spawnWall();
        }
        putCollider(wall, lastWallEnd, transform.position);
        canMoveUp = canMoveDown = canMoveLeft = canMoveRight = true;
    }

    void spawnWall()
    {
        lastWallEnd = transform.position;
        GameObject g = Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
    }

    void putCollider(Collider2D col, Vector2 locA, Vector2 locB)
    {
        col.transform.position = locA + (locB - locA) * 0.5f;

        float distance = Vector2.Distance(locA, locB);
        if (locA.x != locB.x)
        {
            col.transform.localScale = new Vector2(distance + 1, 1);
        }
        else
        {
            col.transform.localScale = new Vector2(1, distance + 1);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != wall)
        {
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("wall");
            foreach (GameObject obj in allObjects)
            {
                obj.SetActive(false);
            }

            //Debug.Log(lives);
            //Debug.Log("Respawn");
            player_cyan.SetActive(false);
            player_pink.SetActive(false);
            lives -= 1;
            AudioSource.PlayClipAtPoint(deathClip, transform.position);
            player_cyan.transform.position = cyan_respawn;
            player_pink.transform.position = pink_respawn;
            player_cyan.SetActive(true);
            player_pink.SetActive(true);
            //make players move upon respawn
            player_cyan.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            player_pink.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            player_cyan.GetComponent<Player>().spawnWall();
            player_pink.GetComponent<Player>().spawnWall();
        }
    }
}
