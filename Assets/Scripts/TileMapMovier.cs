using UnityEngine;

public class TileMapMovier : MonoBehaviour
{
    [SerializeField] private Transform _tile;

    private bool _isMoving = false;
    private Vector2 _destination = new Vector2(-2.6f, 0);

    public void Move()
    {
        _isMoving = true;
    }

    private void Update()
    {
        if (_isMoving)
            while (Vector2.Distance(transform.position, _destination) > 0.1f)
            {
      
                transform.Translate(2f * Time.deltaTime * Vector3.left);
            }

    }
}
