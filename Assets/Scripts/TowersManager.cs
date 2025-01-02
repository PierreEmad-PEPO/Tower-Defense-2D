using UnityEngine;

public class TowersManager : Singletone<TowersManager>
{
    [SerializeField] private LayerMask buildingSiteMask;

    TowerButton pressedTower;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 _worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D _hit = Physics2D.Raycast(_worldPos, Vector2.zero, 300, buildingSiteMask);
            PlaceTower(_hit);
        }

        if (spriteRenderer.enabled)
        {
            FollowMousePosition();
        }
    }

    private void PlaceTower(RaycastHit2D _hit)
    {
        if (_hit.collider != null && pressedTower != null)
        {
            GameObject _tower = Instantiate(pressedTower.TowerObject);
            DeactivateTowerSprite();
            _tower.transform.position = _hit.transform.position;
            _hit.transform.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    private void FollowMousePosition()
    {
        Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _mousePos;
    }
    private void ActivateTowerSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = pressedTower.TowerSprite;
    }
    private void DeactivateTowerSprite()
    {
        spriteRenderer.enabled = false;
    }

    public void OnTowerButtonPressed(TowerButton _towerBtn)
    {
        pressedTower = _towerBtn;
        ActivateTowerSprite();
    }
}
