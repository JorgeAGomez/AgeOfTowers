using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TowerManager : Singleton<TowerManager> {

    public TowerBtn towerBtnPressed { get; set; }
    private SpriteRenderer spriteRenderer;

    private List<Tower> TowerList = new List<Tower>();
    private List<Collider2D> BuildList = new List<Collider2D>();
    private Collider2D buildTile;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider.tag == "buildsite" )
            {
                buildTile = hit.collider;
                buildTile.tag = "buildsiteFull";
                RegisterBuildSite(buildTile); 
                placeTower(hit);
            }
            
            if (hit.collider.tag == "buildsiteFull")
            {
                replaceTower(hit);
            }            
        }
        if (spriteRenderer.enabled)
        {
            followMouse();
        }
    }

    public void RegisterBuildSite(Collider2D buildTag)
    {
        BuildList.Add(buildTag);
    }

    public void RegisterTower(Tower tower)
    {
        TowerList.Add(tower);
    }

    public void RenameTagsBuildSites()
    {
        foreach(Collider2D buildTag in BuildList)
        {
            buildTag.tag = "buildsite";
        }
        BuildList.Clear();
    }

    public void DestroyAllTowers()
    {
        foreach(Tower tower in TowerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }
    public void placeTower(RaycastHit2D hit)
    {
        if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            Tower newTower = Instantiate (towerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            buyTower(towerBtnPressed.TowerPrice);
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.TowerBuilt);
            RegisterTower(newTower);
            disableDragSprite();
        }
        
    }

    public void replaceTower(RaycastHit2D hit)
    {
        
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {            
            Tower newTower = Instantiate(towerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            buyTower(towerBtnPressed.TowerPrice);
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.TowerBuilt);
            
            Tower towerIndex = null;
            
            foreach (Tower tower in TowerList)
            {
                if (tower != null)
                {
                    if (newTower.transform.position == tower.transform.position)
                    {
                        hit.collider.tag = "buildSiteChanged";
                        Destroy(tower.gameObject);
                        Destroy(tower);
                        towerIndex = tower;
                    }
                }                                                            
            }
            TowerList.Add(newTower);
            TowerList.Remove(towerIndex);           
            disableDragSprite();


        }
    }

    public void buyTower(int price)
    {
        GameManager.Instance.subtractMoney(price);
    }

    public void selectedTower(TowerBtn towerSelected)
    {
        if (towerSelected.TowerPrice <= GameManager.Instance.TotalMoney )
        {
            towerBtnPressed = towerSelected;
            enableDragSprite(towerBtnPressed.DragSprite);
        }

    }

    public void followMouse()
    {

            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(transform.position.x, transform.position.y);
        
    }

    public void enableDragSprite(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void disableDragSprite()
    {
        spriteRenderer.enabled = false;
        towerBtnPressed = null;

    }




}
