using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawn : MonoBehaviour
{
    public GameObject turret;

    private List<Turret> turrets;

    [SerializeField]
    int startingMaxTurrets = 5;

    [SerializeField]
    int killsPerNewTurret = 3;

    private int kills = 0;

    public int Kills
    {
        get { return kills; }
    }

    void Start()
    {
        turrets = new List<Turret>();
        DoTurretUI();
    }

    void Update()
    {
        DoTurretUI();
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            Physics.Raycast(ray, out hit, 20f);

            if(hit.collider.GetComponent<TurretSpawn>() == this)
            {
                SpawnTurret(hit.point);
            }
            else if(hit.collider.tag == "Player") //Player tag used for turrets
            {
                //Remove Turret from list
                turrets.Remove(hit.collider.GetComponent<Turret>());
                //Destroy Turret
                Destroy(hit.collider.gameObject);
            }
            else if(hit.collider.tag == "Enemy")
            {//Set all turrets to target specific enemy
                foreach(Turret i in turrets)
                {
                    i.SetTarget(hit.collider.transform);
                }
            }
        }
    }

    private void SpawnTurret(Vector3 point)
    {
        //Killing more enemies increases the number of turrets you can create
        int maxTurrets = startingMaxTurrets + (kills / killsPerNewTurret);

        if (turrets.Count < maxTurrets)
        {
            GameObject t = GameObject.Instantiate(turret, point, Quaternion.identity);
            turrets.Add(t.GetComponent<Turret>());
        }
    }

    private void DoTurretUI()
    {
        int tLeft = (startingMaxTurrets + (kills / killsPerNewTurret)) - turrets.Count;
        GameObject.FindObjectOfType<UIController>().DoTurretText(tLeft);
    }

    public void IncrementKills()
    {
        kills++;
    }
}
